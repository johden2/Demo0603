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
                if (businessType == "TecProcessBom") //配套明细表
                {
                     dataTable = ExcelHelper.ReadData(fileName, ref message);
                    System.IO.File.Delete(fileName);
                    if (dataTable == null || dataTable.Rows.Count == 0 || !string.IsNullOrEmpty(message))
                    {
                        ViewBag.ErrorMessage = "转换Excel错误,错误消息为：" + (string.IsNullOrEmpty(message) ? "没有读取到需要导入的数据" : message);
                        return View("ImportExcel");
                    }
                    errorList = this.Import_TecProcessBom(dataTable);
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
                    templateUrl = "/Document/TecProcessBom.xls";
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
            //List<Rc_SampleInfo> dataList = new List<Rc_SampleInfo>();

            //List<Rc_SampleRFIDRefer> RFIDReferList = new List<Rc_SampleRFIDRefer>();//RFID编号信息

            //IList<ImportMessageModel> resultList = new List<ImportMessageModel>();
            ////提示用户导入消息有错，但不影响数据导入
            //IList<ImportMessageModel> msgList = new List<ImportMessageModel>();
            ////List<TTaskNotice> TTaskNoticeList = new List<TTaskNotice>();
            //List<Sys_HistoryLog> HistoryLogList = new List<Sys_HistoryLog>();
            //List<Rc_SampleInfoExt> sampleExtList = new List<Rc_SampleInfoExt>();
            //int rowIndex = 0; //第1行是行头
            //Rc_SampleInfo SampleInfo = null;

            //ImportMessageModel errorObj = null;
            //DateTime time = DateTime.Now;
            //string BatchNo = Sys.Bussiness.SysNumberUtil.Instance.GenBatchNo();
            //int RFIDNo = Rc_SampleRFIDReferDao.Instance.GetMaxId<Rc_SampleRFIDRefer>();
            //List<Sys_Region> regions = Sys_RegionDao.Instance.FindAll();
            ////获取样品属性
            //List<Sys_PropertyConfig> propertys = Sys_PropertyConfigDao.Instance.FindAll<Sys_PropertyConfig>();
            ////获取样品编号最大值
            ////新编码规则采样年份（2位）+县域代码（6位）+随机码（1位：国控、省控、详查、课题、其他）+采样层位（1位：表层、中层、深层）+顺序码（4位顺序码）。
            //List<Rc_SampleInfo> sampleIds = Rc_SampleInfoDao.Instance.getMaxSampleIdByRule("YYXXXXXXAB");
            //foreach (DataRow row in dtData.Rows)
            //{
            //    rowIndex++;
            //    SampleInfo = new Rc_SampleInfo();
            //    Rc_SampleRFIDRefer RFIDRefer = new Rc_SampleRFIDRefer();//RFID编号信息
            //    //TTaskNotice Notice = new TTaskNotice();
            //    Sys_HistoryLog HistoryLog = new Sys_HistoryLog();
            //    int YRFIDNo = RFIDNo++;
            //    //存样品信息
            //    errorObj = SampleInfo_FormatRow(row, SampleInfo, rowIndex, BatchNo, HistoryLog);
            //    if (errorObj != null) //记录行错误信息
            //    {
            //        resultList.Add(errorObj);
            //        continue;
            //    }
            //    //判断该样品编号的是否已经存在
            //    //注：处理同一批样品导入样品编码相同问题
            //    //if (dataList.Exists(p => p.SampleId == SampleInfo.SampleId))
            //    //{
            //    //    string v = dataList.Where(p=>p.SampleId==SampleInfo.SampleId).Max(x => SampleInfo.SampleId);
            //    //        //样品编号+1
            //    //        if (v.IndexOf("_") > -1)
            //    //        {
            //    //            SampleInfo.SampleId = SampleInfo.SampleId.Split('_')[0] +'_'+ (ConvertHelper.FormatDBInt(v.Split('_')[1]) + 1);
            //    //        }
            //    //        else
            //    //        {
            //    //            SampleInfo.SampleId = SampleInfo.SampleId + "_1";
            //    //        }
            //    //    //resultList.Add(new ImportMessageModel() { RowData = string.Format(ReadErrorMessage, rowIndex), RowMessage = string.Format("导入的样品编号{0}记录有重复", SampleInfo.SampleId) });
            //    //    //continue;
            //    //}
            //    SampleInfo.KeyWord = string.Format("{0} {1} {2} {3}", SampleInfo.SampleId, SampleInfo.BatchNo, SampleInfo.SampleUser, row["样品属性"]);
            //    if (dtData.Columns.Contains("所属项目"))
            //    {
            //        SampleInfo.KeyWord += " " + row["所属项目"].ToString();
            //    }
            //    Sys_Region region = regions.Where(p => p.RegionName == SampleInfo.RegionName).FirstOrDefault();
            //    string regionNum = "";
            //    //判断该行政区域的是否存在
            //    if (region == null)
            //    {

            //        msgList.Add(new ImportMessageModel() { RowData = string.Format(ReadErrorMessage, rowIndex), RowMessage = string.Format("导入的行政区域{0}记录与系统数据不一致", SampleInfo.RegionName) });
            //        //continue;
            //    }
            //    else
            //    {
            //        SampleInfo.RegionCode = region.RegionCode;
            //        SampleInfo.KeyWord += " " + region.RegionName;
            //        regionNum = region.RegionNum;
            //    }
            //    region = regions.Where(p => p.RegionName == SampleInfo.CountyName).FirstOrDefault();
            //    //判断该县级区域的是否存在
            //    if (region == null)
            //    {

            //        msgList.Add(new ImportMessageModel() { RowData = string.Format(ReadErrorMessage, rowIndex), RowMessage = string.Format("导入的县级区域{0}记录与系统数据不一致", SampleInfo.CountyName) });
            //        //continue;
            //    }
            //    else
            //    {
            //        SampleInfo.RegionCode = region.RegionCode;
            //        SampleInfo.KeyWord += " " + region.RegionName;
            //        regionNum = region.RegionNum;
            //    }

            //    //生成新样品编码
            //    errorObj = SampleInfo_Create(SampleInfo, rowIndex, regionNum, sampleIds, RFIDRefer, YRFIDNo);

            //    if (errorObj != null) //记录生成错误信息
            //    {
            //        resultList.Add(errorObj);
            //        continue;
            //    }
            //    //存样品扩展信息
            //    errorObj = SampleInfo_FormatRowExt(row, SampleInfo, rowIndex, propertys, sampleExtList);
            //    if (errorObj != null) //记录行错误信息
            //    {
            //        msgList.Add(errorObj);
            //    }
            //    dataList.Add(SampleInfo);
            //    RFIDReferList.Add(RFIDRefer);

            //    HistoryLogList.Add(HistoryLog);
            //}

            ////如果校验有错误，直接返回错误信息
            //if (resultList.Count > 0) return resultList;
            ////2.校验成功执行导入
            //Rc_SampleInfoDao.Instance.ImportSample(dataList, resultList);
            //if (resultList.Count == 0)
            //{
            //    resultList.Add(new ImportMessageModel { RowData = "导入成功", RowMessage = "数据已成功导入" });
            //    //保存样品扩展属性
            //    Rc_SampleInfoExtDao.Instance.ImportSampleExt(sampleExtList, resultList);
            //    //保存RFID关系
            //    Rc_SampleRFIDReferDao.Instance.SaveList(RFIDReferList);
            //    //保存日志
            //    Sys_HistoryLogDao.Instance.SaveList(HistoryLogList);
            //}

            //return resultList;

            return null;

        }

        //private ImportMessageModel SampleInfo_FormatRow(DataRow row, Rc_SampleInfo SampleInfo, int rowIndex, string BatchNo, Sys_HistoryLog HistoryLog)
        //{
        //    ImportMessageModel errorObj = null;
          
        //    try
        //    {
        //        //导入样品
        //        SampleInfo.SampleId = ConvertHelper.FormatDBString(row["原样品编号"]);
        //        SampleInfo.SampleNumber = ConvertHelper.FormatDBString(row["点位编号"]);
        //        SampleInfo.SampleVolume = ConvertHelper.FormatDBFloat(row["样品存量(g)"]);
        //        SampleInfo.StoreVolume = SampleInfo.SampleVolume;
        //        SampleInfo.BatchNo = BatchNo;
        //        SampleInfo.Status = 1;
        //        SampleInfo.CreateTime = DateTime.Now;
        //        SampleInfo.SampleUser = ConvertHelper.FormatDBString(row["采样人"]);
        //        SampleInfo.SampleTime = ConvertHelper.FormatDBDateTime((row["采样时间"]));
        //        SampleInfo.SampleCategory = ConvertHelper.FormatDBInt(StatusHelper.GetStatusByDescription<SampleCategory>(row["样品属性"].ToString()));
        //        SampleInfo.RelSampleId = SampleInfo.SampleId;
        //        if (string.IsNullOrEmpty(SampleInfo.SampleId))
        //        {
        //            return new ImportMessageModel() { RowData = string.Format(ReadErrorMessage, rowIndex), RowMessage = "样品编号不能为空" };
        //        }
        //        //Rc_SampleInfo obj = Rc_SampleInfoDao.Instance.getMaxSampleId(SampleInfo.SampleId);

        //        //if (obj!=null)
        //        //{

        //        //    //样品编号+1
        //        //    if (obj.SampleId.IndexOf("_")>-1)
        //        //    {
        //        //        SampleInfo.SampleId = SampleInfo.SampleId +'_'+ (ConvertHelper.FormatDBInt(obj.SampleId.Split('_')[1])+1);
        //        //    }
        //        //    else
        //        //    {
        //        //        SampleInfo.SampleId =  SampleInfo.SampleId+"_1";
        //        //    }
                   
                    
        //        //    //return new ImportMessageModel() { RowData = string.Format(ReadErrorMessage, rowIndex), RowMessage = "样品编号已存在" };
        //        //}
        //        SampleInfo.RegionName = ConvertHelper.FormatDBString(row["行政区域"]);
        //        SampleInfo.CountyName = ConvertHelper.FormatDBString(row["县级区域"]);
        //        //SampleInfo.BottleCode = ConvertHelper.FormatDBString(row["箱号"]);
        //        SampleInfo.PointType = StatusHelper.GetStatusByDescription<PointType>(row["项目类型"].ToString());
        //        SampleInfo.DepthType = StatusHelper.GetStatusByDescription<DepthType>(row["深度类型"].ToString());
        //        SampleInfo.Longitude = ConvertHelper.FormatDBString(row["东经"]);
        //        SampleInfo.Latitude = ConvertHelper.FormatDBString(row["北纬"]);
               

        //        HistoryLog.KeyId = SampleInfo.RelSampleId;
        //        HistoryLog.BusinessType = StoreBusinessType.YanPin;
        //        HistoryLog.Type = 1;
        //        HistoryLog.Remark = SampleInfo.SampleId;
        //        string Content = "{0}在{1}导入了一个样品";
        //        HistoryLog.Content = string.Format(Content, base.CurUser.UserName, ConvertHelper.FormatDateToStringNew(DateTime.Now));
        //        HistoryLog.CreateBy = base.CurUser.UserName;
        //        HistoryLog.CreateTime = DateTime.Now;
        //    }
        //    catch (Exception ex)
        //    {
                
        //       return new ImportMessageModel() { RowData = string.Format(ReadErrorMessage, rowIndex), RowMessage = ex.Message };
        //    }


        //    return errorObj;
        //}

        #endregion End 配套明细表导入

    }
}
