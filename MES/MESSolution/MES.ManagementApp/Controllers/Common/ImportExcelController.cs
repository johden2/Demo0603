using System;
using System.Collections.Generic;
using System.Data;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using Sys.Model;
using Sys.Dao;
using Sys.Tools;

namespace MES.ManagementApp.Controllers
{
    /// <summary>
    /// 通用的导入Excel
    /// </summary>
    public partial class CommonController :BaseController
    {
        #region 通用Excel导入代码
        private const string ExcelTempFolder = "~/Document/Temp/";

        private const string ReadErrorMessage = "第{0}行出错";
        private const string ImportErrorMessage = "第{0}行导入失败";
        private string businessType = string.Empty;
        private string curUserId = string.Empty;

        [HttpGet]
        public ActionResult ImportExcel()
        {
            SetParamViewbage();
            return View();
        }

      [HttpPost]
        public ActionResult ImportExcelMain()
        {
            SetParamViewbage();

            //1.文件验证和上传
            string message = string.Empty;
            string fileName = string.Empty;
            curUserId = base.CurUser.UserId;
            bool fileCheck = CheckAndSaveFile(out message, out fileName);
            if (fileCheck == false || string.IsNullOrEmpty(fileName))
            {
                ViewBag.ErrorMessage = message;
                return View();
            }

            //读取文件，进行转换
            IList<ImportMessageModel> errorList = new List<ImportMessageModel>();
            try
            {
                DataTable dataTable = null;
                dataTable = ExcelHelper.ReadData(fileName, ref message);
                System.IO.File.Delete(fileName);
                if (dataTable == null || dataTable.Rows.Count == 0 || !string.IsNullOrEmpty(message))
                {
                    ViewBag.ErrorMessage = "转换Excel错误,错误消息为：" + (string.IsNullOrEmpty(message) ? "没有读取到需要导入的数据" : message);
                    return View("ImportExcel");
                }
                if (businessType == "TecProcessBom") //配套明细表
                {
                    errorList = this.Import_TecProcessBom(dataTable);
                }
                else if (businessType == "ProductInfo") //产品信息
                {
                    errorList = this.Import_ProductInfo(dataTable);
                }
                else if (businessType == "BarcodeInfo") //条码导入
                {
                    errorList = this.Import_BarcodeInfo(dataTable);
                }

                ViewBag.ErrorMessage = "导入成功";
            }
            catch (Exception ee)
            {
                ViewBag.ErrorMessage = "异常,错误消息为：" + ee.Message;
            }

            if (errorList == null)
            {
                errorList = new List<ImportMessageModel>();
            }
            else if (errorList.Count > 1 || (errorList.Count == 1 && errorList[0].RowData != "导入成功"))
            {
                ViewBag.ErrorMessage = "导入失败，请查看检证结果";
            }

            ViewBag.ErrorList = errorList;
            return View("ImportExcel");
        }

        /// <summary>
        /// 接收参数，并设置ViewBage值
        /// </summary>
        private void SetParamViewbage()
        {
            businessType = Request.Params["BusinessType"];
            string templateUrl = string.Empty;
            string paramKey = string.Empty;
            ViewBag.BusinessType = businessType;

            switch (businessType)
            {
                case "TecProcessBom":
                    templateUrl = "/Document/Template/TecProcessBom.xls";
                    paramKey = Request.Params["ParamKey"];
                    break;
                case "ProductInfo":
                    templateUrl = "/Document/Template/ProductInfo.xls";
                    paramKey = Request.Params["ParamKey"];
                    break;
                case "BarcodeInfo":
                    templateUrl = "/Document/Template/BarcodeInfo.xls";
                    paramKey = Request.Params["ParamKey"];
                    break;
                default:
                    break;
            }

            ViewBag.ParamKey = paramKey;
            ViewBag.TemplateUrl = templateUrl;
        }

        /// <summary>
        /// 对文件进行验证并上传到临时目录下
        /// </summary>
        /// <returns>文件是否保存成功</returns>
        private bool CheckAndSaveFile(out string message, out string fileFullName)
        {
            string fileName = string.Empty;
            return this.CheckAndSaveFile(ExcelTempFolder, out message, out fileFullName, out fileName);
        }

       /// <summary>
        /// 上传文件
       /// </summary>
       /// <param name="fileFolder">生成的文件存放的位置</param>
       /// <param name="message">错误消息</param>
       /// <param name="fileFullName">返回文件的物理路径</param>
       /// <param name="fileName">返回文件的名称</param>
       /// <returns>是否成功</returns>
        private bool CheckAndSaveFile(string fileFolder, out string message, out string fileFullName, out string fileName)
        {
            message = string.Empty;
            fileFullName = string.Empty;
            fileName = string.Empty;

            //验证是否上传文件
            var files = Request.Files;
            if (files == null || files.Count == 0)
            {
                message = "请选择需要导入的文件";
                return false;
            }
            HttpPostedFileBase file = Request.Files[0];
            //是否有内容
            if (file.ContentLength == 0)
            {
                message = "导入的文件不包含任何内容，请选择Excel";
                return false;
            }
            ////大小是否符合配置规定
            //int excelMaxSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["excelMaxSize"].ToString());
            //if (excelMaxSize * 1024 * 1024 < file.ContentLength)
            //{
            //    message = string.Format("上传文件不能大于{0}M", excelMaxSize);
            //    return false;
            //}
            ////文件名是否符合配置规定
            //string allowExtension = System.Configuration.ConfigurationManager.AppSettings["excelExtension"].ToString();
            string extension = Path.GetExtension(file.FileName).ToLower();
            //if (allowExtension.IndexOf(extension) == -1)
            //{
            //    message = string.Format("上传文件名{0}扩展名不正确，应该是{1}中的一种", file.FileName, allowExtension);
            //    return false;
            //}

            //开始上传文件
            try
            {
                string excelTempPath = Server.MapPath(fileFolder);
                fileName = string.Format("{0:yyyyMMddHHmmssffff}{1}", DateTime.Now, extension);
                fileFullName = string.Format("{0}{1}", excelTempPath, fileName);
                file.SaveAs(fileFullName);
            }
            catch (System.Exception ex)
            {
                message = "上传文件到服务器失败:" + ex.Message;
                return false;
            }
            return true;
        }
       
        #endregion

        #region Begin 配套明细表导入
        private IList<ImportMessageModel> Import_TecProcessBom(DataTable dtData)
        {
            IList<ImportMessageModel> resultList = new List<ImportMessageModel>();
            //提示用户导入消息有错，但不影响数据导入
            IList<ImportMessageModel> msgList = new List<ImportMessageModel>();
            List<Mes_Tec_ProcessBom> bomList = new List<Mes_Tec_ProcessBom>();
            List<Mes_Tec_ProcessBomItem> dataList = new List<Mes_Tec_ProcessBomItem>();
            List<Mes_Tec_Process> processList = null;
            List<Mes_Tec_ProductInfo> productList = null;
            int rowIndex = 0; //第1行是行头
            DateTime time = DateTime.Now;
            ImportMessageModel errorObj = null;

            if (dtData != null && dtData.Rows.Count > 0)
            {
                Mes_Tec_ProcessBom bomObj = null;
                Mes_Tec_ProcessBomItem bomItemObj = null;
                Mes_Tec_Process processObj = null;
                Mes_Tec_ProductInfo productObj = null;
              
                processList = MesTecProcessDao.Instance.FindAll<Mes_Tec_Process>();
                productList = MesTecProductInfoDao.Instance.FindAll<Mes_Tec_ProductInfo>();
                foreach (DataRow row in dtData.Rows)
                {
                    rowIndex++;
                    bomItemObj = new Mes_Tec_ProcessBomItem();
                    bomItemObj.CreatedTime = time;
                    bomItemObj.Creater = curUserId;
                    //存样品信息
                    errorObj = TecProcessBom_FormatRow(row, bomItemObj, rowIndex);
                    if (errorObj != null) //记录行错误信息
                    {
                        resultList.Add(errorObj);
                        continue;
                    }

                    processObj = processList.Where(p => p.Name == bomItemObj.ProcessName && p.RecordStatus == 1).FirstOrDefault();
                    if (processObj == null)
                    {
                        msgList.Add(new ImportMessageModel() { RowData = string.Format(ReadErrorMessage, rowIndex), RowMessage = string.Format("工艺【{0}】不存在", bomItemObj.ProcessName) });
                    }
                    else
                    {
                        bomItemObj.ProcessID = processObj.ID;
                    }

                    productObj = productList.Where(p => p.MaterialProNo == bomItemObj.MaterialProNo).FirstOrDefault();
                    if (productObj == null)
                    {
                        msgList.Add(new ImportMessageModel() { RowData = string.Format(ReadErrorMessage, rowIndex), RowMessage = string.Format("产品编码【{0}】不存在", bomItemObj.MaterialProNo) });
                    }
                    else
                    {
                        bomItemObj.MaterialCode = productObj.MaterialCode;
                        if (!bomList.Exists(p => p.MaterialProNo == bomItemObj.MaterialProNo && p.Version == bomItemObj.Version))
                        {
                            bomObj = new Mes_Tec_ProcessBom();
                            bomObj.MaterialProNo = bomItemObj.MaterialProNo;
                            bomObj.MaterialCode = bomItemObj.MaterialCode;
                            bomObj.Version = bomItemObj.Version;
                            bomObj.CreatedTime = time;
                            bomObj.Creater = curUserId;
                            bomList.Add(bomObj);
                        }
                    }

                    productObj = productList.Where(p => p.MaterialProNo == bomItemObj.SubMaterialProNo).First();
                    if (productObj == null)
                    {
                        msgList.Add(new ImportMessageModel() { RowData = string.Format(ReadErrorMessage, rowIndex), RowMessage = string.Format("物料编码【{0}】不存在", bomItemObj.MaterialProNo) });
                    }
                    else
                    {
                        bomItemObj.SubMaterialCode = productObj.MaterialCode;
                    }

                    dataList.Add(bomItemObj);
                }
            }
            

            //如果校验有错误，直接返回错误信息
            if (resultList.Count > 0) return resultList;
            //2.校验成功执行导入
            MesTecProcessBomDao.Instance.Import(bomList,dataList, resultList);
            if (resultList.Count == 0)
            {
                resultList.Add(new ImportMessageModel { RowData = "导入成功", RowMessage = "数据已成功导入" });
            }

            return resultList;

        }

        private ImportMessageModel TecProcessBom_FormatRow(DataRow row, Mes_Tec_ProcessBomItem itemObj, int rowIndex)
        {
            ImportMessageModel errorObj = null;

            try
            {
                //导入样品
                itemObj.MaterialProNo = TConvertHelper.FormatDBString(row["产品编码"]);
                itemObj.Version = TConvertHelper.FormatDBString(row["版本"]);
                itemObj.ProcessName = TConvertHelper.FormatDBString(row["工艺"]);
                itemObj.SubMaterialProNo = TConvertHelper.FormatDBString(row["物料编码"]);
                itemObj.Num = TConvertHelper.FormatDBInt(row["数量"]);
                itemObj.Memo = TConvertHelper.FormatDBString(row["备注"]);
                itemObj.CreatedTime = DateTime.Now;
                if (string.IsNullOrEmpty(itemObj.MaterialProNo) || string.IsNullOrEmpty(itemObj.Version))
                {
                    return new ImportMessageModel() { RowData = string.Format(ReadErrorMessage, rowIndex), RowMessage = "产品编码、版本不能为空" };
                }
                if (string.IsNullOrEmpty(itemObj.SubMaterialProNo))
                {
                    return new ImportMessageModel() { RowData = string.Format(ReadErrorMessage, rowIndex), RowMessage = "物料编码不能为空" };
                }
                if (string.IsNullOrEmpty(itemObj.ProcessName))
                {
                    return new ImportMessageModel() { RowData = string.Format(ReadErrorMessage, rowIndex), RowMessage = "工艺不能为空" };
                }
                if (itemObj.Num <=0)
                {
                    return new ImportMessageModel() { RowData = string.Format(ReadErrorMessage, rowIndex), RowMessage = "数量不能为空" };
                }
            }
            catch (Exception ex)
            {
                return new ImportMessageModel() { RowData = string.Format(ReadErrorMessage, rowIndex), RowMessage = ex.Message };
            }

            return errorObj;
        }

        #endregion End 配套明细表导入

        #region Begin 产品信息导入
        private IList<ImportMessageModel> Import_ProductInfo(DataTable dtData)
        {
            IList<ImportMessageModel> resultList = new List<ImportMessageModel>();
            //提示用户导入消息有错，但不影响数据导入
            IList<ImportMessageModel> msgList = new List<ImportMessageModel>();
            List<Mes_Tec_ProductInfo> productAllList = null;
            List<Mes_Tec_ProductInfo> dataList = new List<Mes_Tec_ProductInfo>();
            int rowIndex = 0; //第1行是行头
            DateTime time = DateTime.Now;
            ImportMessageModel errorObj = null;

            if (dtData != null && dtData.Rows.Count > 0)
            {
                productAllList = MesTecProductInfoDao.Instance.FindAll<Mes_Tec_ProductInfo>();
                Mes_Tec_ProductInfo productObj = null;
                foreach (DataRow row in dtData.Rows)
                {
                    rowIndex++;
                    productObj = new Mes_Tec_ProductInfo();
                    productObj.CreatedTime = time;
                    productObj.Creater = curUserId;
                    //存样品信息
                    errorObj = ProductInfo_FormatRow(row, productObj, rowIndex);
                    if (errorObj != null) //记录行错误信息
                    {
                        resultList.Add(errorObj);
                        continue;
                    }

                    //检测物料编码是否重复，不允许重复
                    if(productAllList.Exists(p => p.MaterialProNo == productObj.MaterialProNo))
                    {
                        resultList.Add(new ImportMessageModel() { RowData = string.Format(ReadErrorMessage, rowIndex), RowMessage = string.Format("物料编码【{0}】已经存在", productObj.MaterialProNo) });
                    }
                    if (dataList.Exists(p => p.MaterialProNo == productObj.MaterialProNo))
                    {
                        resultList.Add(new ImportMessageModel() { RowData = string.Format(ReadErrorMessage, rowIndex), RowMessage = string.Format("物料编码【{0}】重复导入", productObj.MaterialProNo) });
                    }

                    dataList.Add(productObj);
                }
            }


            //如果校验有错误，直接返回错误信息
            if (resultList.Count > 0) return resultList;
            //2.校验成功执行导入
            MesTecProductInfoDao.Instance.Import(dataList, resultList);
            if (resultList.Count == 0)
            {
                resultList.Add(new ImportMessageModel { RowData = "导入成功", RowMessage = "数据已成功导入" });
            }

            return resultList;

        }

        private ImportMessageModel ProductInfo_FormatRow(DataRow row, Mes_Tec_ProductInfo itemObj, int rowIndex)
        {
            ImportMessageModel errorObj = null;

            try
            {
                //导入样品
                string sTemp = "";
                itemObj.MaterialProNo = TConvertHelper.FormatDBString(row["物料编码"]);
                itemObj.MaterialCode = TConvertHelper.FormatDBString(row["物料名称"]);
                sTemp = TConvertHelper.FormatDBString(row["物料分类"]);
                if (!string.IsNullOrEmpty(sTemp))
                {
                    itemObj.MaterialClass = StatusHelper.GetStatusByDescription<MaterialClassStatus>(sTemp);
                }
                sTemp = TConvertHelper.FormatDBString(row["批次属性"]);
                if (!string.IsNullOrEmpty(sTemp))
                {
                    itemObj.TraceProperty = TConvertHelper.FormatDBInt(StatusHelper.GetStatusByDescription<MaterialClassStatus>(sTemp));
                }
                itemObj.MaterialSize = TConvertHelper.FormatDBString(row["规格尺寸"]);
                itemObj.Unit = TConvertHelper.FormatDBString(row["单位"]);
                itemObj.PackNumber = TConvertHelper.FormatDBInt(row["每箱数量"]);
                itemObj.Memo = TConvertHelper.FormatDBString(row["备注"]);
                //itemObj.CreatedTime = DateTime.Now;
                if (string.IsNullOrEmpty(itemObj.MaterialProNo) || string.IsNullOrEmpty(itemObj.MaterialCode))
                {
                    return new ImportMessageModel() { RowData = string.Format(ReadErrorMessage, rowIndex), RowMessage = "物料编码、物料名称不能为空" };
                }
            }
            catch (Exception ex)
            {
                return new ImportMessageModel() { RowData = string.Format(ReadErrorMessage, rowIndex), RowMessage = ex.Message };
            }

            return errorObj;
        }

        #endregion End 产品信息导入

        #region Begin 条码信息导入
        private IList<ImportMessageModel> Import_BarcodeInfo(DataTable dtData)
        {
            IList<ImportMessageModel> resultList = new List<ImportMessageModel>();
            //提示用户导入消息有错，但不影响数据导入
            IList<ImportMessageModel> msgList = new List<ImportMessageModel>();
            List<Mes_Tra_SourceBarcode> dataList = new List<Mes_Tra_SourceBarcode>();
            int rowIndex = 0; //第1行是行头
            DateTime time = DateTime.Now;
            ImportMessageModel errorObj = null;
            string batchNo = DateTime.Now.ToString("yyMMddHHmmss");
            if (dtData != null && dtData.Rows.Count > 0)
            {
                Mes_Tra_SourceBarcode itemObj = null;
                foreach (DataRow row in dtData.Rows)
                {
                    rowIndex++;
                    itemObj = new Mes_Tra_SourceBarcode();
                    itemObj.CreatedTime = time;
                    itemObj.Creater = curUserId;
                    itemObj.PackSN = batchNo;
                    //存样品信息
                    errorObj = BarcodeInfo_FormatRow(row, itemObj, rowIndex);
                    if (errorObj != null) //记录行错误信息
                    {
                        resultList.Add(errorObj);
                        continue;
                    }

                    //检测条码是否重复
                    if (SourceBarcodeDao.Instance.IsExist(itemObj.Barcode))
                    {
                        resultList.Add(new ImportMessageModel() { RowData = string.Format(ReadErrorMessage, rowIndex), RowMessage = string.Format("条码【{0}】已经存在", itemObj.Barcode) });
                    }
                    if (dataList.Exists(p => p.Barcode == itemObj.Barcode))
                    {
                        resultList.Add(new ImportMessageModel() { RowData = string.Format(ReadErrorMessage, rowIndex), RowMessage = string.Format("条码【{0}】重复导入", itemObj.Barcode) });
                    }

                    dataList.Add(itemObj);
                }
            }


            //如果校验有错误，直接返回错误信息
            if (resultList.Count > 0) return resultList;
            //2.校验成功执行导入
            SourceBarcodeDao.Instance.Import(dataList, resultList);
            if (resultList.Count == 0)
            {
                resultList.Add(new ImportMessageModel { RowData = "导入成功", RowMessage = "数据已成功导入" });
            }

            return resultList;

        }

        private ImportMessageModel BarcodeInfo_FormatRow(DataRow row, Mes_Tra_SourceBarcode itemObj, int rowIndex)
        {
            ImportMessageModel errorObj = null;

            try
            {
                itemObj.Barcode = TConvertHelper.FormatDBString(row["条码"]);
                if (string.IsNullOrEmpty(itemObj.Barcode))
                {
                    return new ImportMessageModel() { RowData = string.Format(ReadErrorMessage, rowIndex), RowMessage = "条码不能为空" };
                }
            }
            catch (Exception ex)
            {
                return new ImportMessageModel() { RowData = string.Format(ReadErrorMessage, rowIndex), RowMessage = ex.Message };
            }

            return errorObj;
        }

        #endregion End 条码信息导入
    }
}
