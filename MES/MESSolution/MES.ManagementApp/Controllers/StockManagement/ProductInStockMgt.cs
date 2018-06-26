using Sys.Dao;
using Sys.Model;
using Sys.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MES.ManagementApp.Controllers
{
    /// <summary>
    /// 生产入库单
    /// </summary>
    public partial class StockManagementController
    {

        #region 主信息

        public ActionResult ProductInStockMgt()
        {
            return View();
        }

        public ActionResult ProductInStockMgt_FindByPage(Mes_Stock_ProductInStock obj, int page, int rows)
        {
            var pager = new PagerBase() { CurrentPageIndex = page, PageSize = rows };
            var list = StockProductInStockDao.Instance.FindByPage(obj, ref pager);
            return Json(new { total = pager.TotalItemCount, rows = list }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProductInStockMgt_Find(int ID)
        {
            if (ID <= 0)
            {
                return Json(new { IsSuccess = false, Message = "选择的记录有误，请刷新后重试！" });
            }
            Mes_Stock_ProductInStock obj = StockProductInStockDao.Instance.Find<Mes_Stock_ProductInStock, int>(ID);
            if (obj == null)
            {
                return Json(new { IsSuccess = false, Message = "信息不存在，请刷新后重试！" });
            }

            return Json(new { IsSuccess = true, Message = JsonHelper.SerializeObject(obj) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProductInStockMgt_Save(Mes_Stock_ProductInStock obj)
        {
            if (string.IsNullOrEmpty(obj.BillType))
            {
                return Json(new { IsSuccess = false, Message = "请选择入库单单别！" });
            }
            Mes_Stock_ProductInStock dataObj = null;
            if (obj.ID > 0)
            {
                dataObj = StockProductInStockDao.Instance.Find<Mes_Stock_ProductInStock, int>(obj.ID);
            }
            else
            {
                dataObj = new Mes_Stock_ProductInStock();
                dataObj.AuditStatus = AuditEnum.No;
                dataObj.Creater = base.CurUser.UserId;
                dataObj.CreatedTime = DateTime.Now;
                //生成单号
                dataObj.BillNo = StockProductInStockDao.Instance.GenBillNo();
            }
            if (dataObj == null)
            {
                return Json(new { IsSuccess = false, Message = "入库单信息有误，请刷新后重试！" });
            }
            dataObj.BillType = obj.BillType;
            dataObj.InStockDate = obj.InStockDate;
            dataObj.BillDate = obj.BillDate;
            dataObj.TotalNum = obj.TotalNum;
            dataObj.Factory = obj.Factory;
            dataObj.Memo = obj.Memo;
            int id = StockProductInStockDao.Instance.Save(dataObj);
            return Json(new { IsSuccess = true, Message = id.ToString() });
        }

        /// <summary>
        /// 入库单审批
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public ActionResult ProductInStockMgt_Audit(Mes_Stock_ProductInStock obj)
        {
            if (obj.ID <= 0)
            {
                return Json(new { IsSuccess = false, Message = "请先选择一个入库单！" });
            }
            Mes_Stock_ProductInStock dataObj = StockProductInStockDao.Instance.Find<Mes_Stock_ProductInStock, int>(obj.ID);
            if (dataObj == null)
            {
                return Json(new { IsSuccess = false, Message = "选择的入库单不存在，请刷新后重试！" });
            }
            if (dataObj.AuditStatus == AuditEnum.Yes)
            {
                return Json(new { IsSuccess = false, Message = "选择的入库单已审批，不允许重复审批！" });
            }

            //obj.ApproverID = base.CurUser.ID;
            //obj.ApproverName = base.CurUser.UserName;
            //obj.BillNo = dataObj.BillNo;
            //obj.BillType = dataObj.BillType;
            //obj.Creater = base.CurUser.UserId;
            //obj.CreatedTime = DateTime.Now;

            //string message = StockProductInStockDao.Instance.DoAudit(obj);
            //if (!string.IsNullOrEmpty(message))
            //{
            //    return Json(new { IsSuccess = false, Message = message });
            //}

            return Json(new { IsSuccess = true, Message =""});
        }

        public ActionResult ProductInStockMgt_Delete(int ID)
        {
            if (ID <= 0)
            {
                return Json(new { IsSuccess = false, Message = "选择的记录有误，请刷新后重试！" });
            }
            Mes_Stock_ProductInStock obj = StockProductInStockDao.Instance.Find<Mes_Stock_ProductInStock, int>(ID);
            if (obj == null)
            {
                return Json(new { IsSuccess = false, Message = "入库单不存在，请刷新后重试！" });
            }
            if (obj.AuditStatus != AuditEnum.No)
            {
                return Json(new { IsSuccess = false, Message = "只能删除未审核的入库单！" });
            }

            StockProductInStockDao.Instance.DeleteExt(ID);
            return Json(new { IsSuccess = true, Message = "" });
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public ActionResult ProductInStockMgt_Export(Mes_Stock_InStock obj)
        {
            var list = MesStockInStockDao.Instance.FindByCond(obj);
            BussinessModel bussinessObj = new BussinessModel();
            bussinessObj.BusinessType = "InStockMgt";
            KeyModel keyObj = null;
            List<KeyModel> colList = new List<KeyModel>();
            keyObj = new KeyModel("单据状态","Show_AuditStatus");
            colList.Add(keyObj);
            keyObj = new KeyModel("检验状态", "Show_CheckStatus");
            colList.Add(keyObj);
            keyObj = new KeyModel("进货单别", "Show_BillType");
            colList.Add(keyObj);
             keyObj = new KeyModel("进货单号","BillNo");
            colList.Add(keyObj);
            keyObj = new KeyModel("进货日期", "Show_InStockDate");
            colList.Add(keyObj);
            keyObj = new KeyModel("总进货数量", "TotalInStockNum");
            colList.Add(keyObj);
            keyObj = new KeyModel("总验收数量", "TotalAcceptNum");
            colList.Add(keyObj);
            keyObj = new KeyModel("供应商", "SupplierName");
            colList.Add(keyObj);
            keyObj = new KeyModel("供应商单号", "SupBillNo");
            colList.Add(keyObj);
            keyObj = new KeyModel("销售单别", "Show_SourceBillType");
            colList.Add(keyObj);
            keyObj = new KeyModel("销售单号", "SourceBillNo");
            colList.Add(keyObj);
            keyObj = new KeyModel("创建人", "Creater");
            colList.Add(keyObj);
            keyObj = new KeyModel("创建时间", "Show_CreatedTime");
            colList.Add(keyObj);
            bussinessObj.ColList = colList;
            string message = SysExportHelper.Export<Mes_Stock_InStock>(ref bussinessObj, list);
            if (!string.IsNullOrEmpty(message))
            {
                return Json(new { IsSuccess = false, Message = message });
            }
            return Json(new { IsSuccess = true, Message = bussinessObj.FileName });
        }


        #endregion 主信息

        #region 明细信息

        public ActionResult ProductInStockItemMgt()
        {
            PageModel page = new PageModel();
            page.OptType = TConvertHelper.FormatDBInt(Request.Params["OptType"]);
            page.KeyID = Request.Params["ID"];
            return View(page);
        }

        public ActionResult ProductInStockItemMgt_FindByPage(Mes_Stock_ProductInStockItem obj, int page, int rows)
        {
            var pager = new PagerBase() { CurrentPageIndex = page, PageSize = rows };
            var list = StockProductInStockDao.Instance.FindItemByPage(obj, ref pager);
            return Json(new { total = pager.TotalItemCount, rows = list }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProductInStockItemMgt_Save(Mes_Stock_ProductInStockItem obj)
        {
            if (obj.ProductInStockID <= 0)
            {
                return Json(new { IsSuccess = false, Message = "请先选择一个入库单进行操作！" });
            }
            if (string.IsNullOrEmpty(obj.MaterialProNo))
            {
                return Json(new { IsSuccess = false, Message = "【物料编码】不能为空！" });
            }
            Mes_Stock_ProductInStock main = StockProductInStockDao.Instance.Find<Mes_Stock_ProductInStock, int>(obj.ProductInStockID.Value);
            if (main == null)
            {
                return Json(new { IsSuccess = false, Message = "选择的入库单信息有误！" });
            }

            Mes_Stock_ProductInStockItem itemObj = null;
            if (obj.ID > 0)
            {
                itemObj = StockProductInStockDao.Instance.Find<Mes_Stock_ProductInStockItem, int>(obj.ID);
            }
            else
            {
                itemObj = new Mes_Stock_ProductInStockItem();
                //生成订单行号
                itemObj.ResNum = StockProductInStockDao.Instance.GenResNum(main);
            }
            if (itemObj == null)
            {
                return Json(new { IsSuccess = false, Message = "入库单明细信息有误，请刷新后重试！" });
            }

            itemObj.ProductInStockID = obj.ProductInStockID;
            itemObj.BillType = main.BillType;
            itemObj.BillNo = main.BillNo;
            itemObj.MaterialProNo = obj.MaterialProNo;
            itemObj.MaterialCode = obj.MaterialCode;
            itemObj.Version = obj.Version;
            itemObj.MaterialSize = obj.MaterialSize;
            itemObj.Unit = obj.Unit;
            itemObj.Num = obj.Num;
            itemObj.WorkOrderType = obj.WorkOrderType;
            itemObj.WorkOrderNumber = obj.WorkOrderNumber;
            itemObj.Memo = obj.Memo;
            itemObj.StockID = obj.StockID;
            itemObj.AlibraryID = obj.AlibraryID;
            int id = StockProductInStockDao.Instance.Save<Mes_Stock_ProductInStockItem>(itemObj);
            return Json(new { IsSuccess = true, Message = id.ToString() });
        }

        public ActionResult ProductInStockItemMgt_Delete(int ID)
        {
            if (ID <= 0)
            {
                return Json(new { IsSuccess = false, Message = "选择的记录有误，请刷新后重试！" });
            }
            Mes_Stock_ProductInStockItem obj = StockProductInStockDao.Instance.Find<Mes_Stock_ProductInStockItem, int>(ID);
            if (obj == null)
            {
                return Json(new { IsSuccess = false, Message = "入库单明细不存在，请刷新后重试！" });
            }

            StockProductInStockDao.Instance.DeleteItemExt(obj);
            return Json(new { IsSuccess = true, Message = "" });
        }

        #endregion 明细信息

    }
}
