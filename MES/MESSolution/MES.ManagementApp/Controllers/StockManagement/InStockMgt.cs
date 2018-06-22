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
    /// 进货单
    /// </summary>
    public partial class StockManagementController
    {

        #region 主信息

        public ActionResult InStockMgt()
        {
            return View();
        }

        public ActionResult InStockMgt_FindByPage(Mes_Stock_InStock obj, int page, int rows)
        {
            var pager = new PagerBase() { CurrentPageIndex = page, PageSize = rows };
            var list = MesStockInStockDao.Instance.FindByPage(obj, ref pager);
            return Json(new { total = pager.TotalItemCount, rows = list }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InStockMgt_Find(int ID)
        {
            if (ID <= 0)
            {
                return Json(new { IsSuccess = false, Message = "选择的记录有误，请刷新后重试！" });
            }
            Mes_Stock_InStock obj = MesStockInStockDao.Instance.Find<Mes_Stock_InStock, int>(ID);
            if (obj == null)
            {
                return Json(new { IsSuccess = false, Message = "信息不存在，请刷新后重试！" });
            }

            return Json(new { IsSuccess = true, Message = JsonHelper.SerializeObject(obj) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InStockMgt_Save(Mes_Stock_InStock obj)
        {
            if (string.IsNullOrEmpty(obj.BillType))
            {
                return Json(new { IsSuccess = false, Message = "请选择进货单单别！" });
            }
            if (obj.SupplierID <= 0)
            {
                return Json(new { IsSuccess = false, Message = "请选择供应商！" });
            }
            Mes_Stock_InStock dataObj = null;
            if (obj.ID > 0)
            {
                dataObj = MesStockInStockDao.Instance.Find<Mes_Stock_InStock, int>(obj.ID);
            }
            else
            {
                dataObj = new Mes_Stock_InStock();
                dataObj.AuditStatus = AuditEnum.No;
                dataObj.Creater = base.CurUser.UserId;
                dataObj.CreatedTime = DateTime.Now;
                //生成单号
                dataObj.BillNo =  MesStockInStockDao.Instance.GenBillNo();
            }
            if (dataObj == null)
            {
                return Json(new { IsSuccess = false, Message = "进货单信息有误，请刷新后重试！" });
            }
            dataObj.BillType = obj.BillType;
            dataObj.InStockDate = obj.InStockDate;
            dataObj.SupplierName = obj.SupplierName;
            dataObj.SupplierID = obj.SupplierID;
            dataObj.SupBillNo = obj.SupBillNo;
            dataObj.BillDate = obj.BillDate;
            dataObj.TotalInStockNum = obj.TotalInStockNum;
            dataObj.TotalAcceptNum = obj.TotalAcceptNum;
            dataObj.SourceBillType = obj.SourceBillType;
            dataObj.SourceBillNo = obj.SourceBillNo;
            dataObj.CheckStatus = obj.CheckStatus;
            dataObj.Factory = obj.Factory;
            int id = MesStockInStockDao.Instance.Save(dataObj);
            return Json(new { IsSuccess = true, Message = id.ToString() });
        }

        /// <summary>
        /// 进货单审批
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public ActionResult InStockMgt_Audit(Mes_Stock_InStockApproval obj)
        {
            if (obj.InStockID <= 0)
            {
                return Json(new { IsSuccess = false, Message = "请先选择一个进货单！" });
            }
            Mes_Stock_InStock dataObj = MesStockInStockDao.Instance.Find<Mes_Stock_InStock, int>(obj.InStockID);
            if (dataObj == null)
            {
                return Json(new { IsSuccess = false, Message = "选择的进货单不存在，请刷新后重试！" });
            }
            if (dataObj.AuditStatus == AuditEnum.Yes)
            {
                return Json(new { IsSuccess = false, Message = "选择的进货单已审批，不允许重复审批！" });
            }

            obj.ApproverID = base.CurUser.ID;
            obj.ApproverName = base.CurUser.UserName;
            obj.BillNo = dataObj.BillNo;
            obj.BillType = dataObj.BillType;
            obj.Creater = base.CurUser.UserId;
            obj.CreatedTime = DateTime.Now;

            string message = MesStockInStockDao.Instance.DoAudit(obj);
            if (!string.IsNullOrEmpty(message))
            {
                return Json(new { IsSuccess = false, Message = message });
            }

            return Json(new { IsSuccess = true, Message =""});
        }

        public ActionResult InStockMgt_Delete(int ID)
        {
            if (ID <= 0)
            {
                return Json(new { IsSuccess = false, Message = "选择的记录有误，请刷新后重试！" });
            }
            Mes_Stock_InStock obj = MesStockInStockDao.Instance.Find<Mes_Stock_InStock, int>(ID);
            if (obj == null)
            {
                return Json(new { IsSuccess = false, Message = "进货单不存在，请刷新后重试！" });
            }
            if (obj.AuditStatus != AuditEnum.No)
            {
                return Json(new { IsSuccess = false, Message = "只能删除未审核的进货单！" });
            }

            MesStockInStockDao.Instance.DeleteExt(ID);
            return Json(new { IsSuccess = true, Message = "" });
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public ActionResult InStockMgt_Export(Mes_Stock_InStock obj)
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

        public ActionResult InStockItemMgt()
        {
            PageModel page = new PageModel();
            page.OptType = TConvertHelper.FormatDBInt(Request.Params["OptType"]);
            page.KeyID = Request.Params["ID"];
            return View(page);
        }

        public ActionResult InStockItemMgt_FindByPage(Mes_Stock_InStockItem obj, int page, int rows)
        {
            var pager = new PagerBase() { CurrentPageIndex = page, PageSize = rows };
            var list = MesStockInStockDao.Instance.FindItemByPage(obj, ref pager);
            return Json(new { total = pager.TotalItemCount, rows = list }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InStockItemMgt_Save(Mes_Stock_InStockItem obj)
        {
            if (obj.InStockID <= 0)
            {
                return Json(new { IsSuccess = false, Message = "请先选择一个进货单进行操作！" });
            }
            if (string.IsNullOrEmpty(obj.MaterialProNo))
            {
                return Json(new { IsSuccess = false, Message = "【物料编码】不能为空！" });
            }
            Mes_Stock_InStock main = MesStockInStockDao.Instance.Find<Mes_Stock_InStock, int>(obj.InStockID.Value);
            if (main == null)
            {
                return Json(new { IsSuccess = false, Message = "选择的进货单信息有误！" });
            }

            Mes_Stock_InStockItem itemObj = null;
            if (obj.ID > 0)
            {
                itemObj = MesStockInStockDao.Instance.Find<Mes_Stock_InStockItem, int>(obj.ID);
                itemObj.CheckItemStatus = obj.CheckItemStatus;
            }
            else
            {
                itemObj = new Mes_Stock_InStockItem();
                itemObj.AcceptNum = 0;
                itemObj.BackNum = 0;
                itemObj.AcceptStatus = YesNoType.No;
                itemObj.CheckItemStatus = CheckStatus.No;
                //生成订单行号
                itemObj.ResNum = MesStockInStockDao.Instance.GenResNum(main);
            }
            if (itemObj == null)
            {
                return Json(new { IsSuccess = false, Message = "订单明细信息有误，请刷新后重试！" });
            }

            itemObj.InStockID = obj.InStockID;
            itemObj.BillType = main.BillType;
            itemObj.BillNo = main.BillNo;
            itemObj.MaterialProNo = obj.MaterialProNo;
            itemObj.MaterialCode = obj.MaterialCode;
            itemObj.Version = obj.Version;
            itemObj.MaterialSize = obj.MaterialSize;
            itemObj.Unit = obj.Unit;
            itemObj.BatchNo = obj.BatchNo;
            itemObj.AcceptNum = obj.AcceptNum;
            itemObj.InStockNum = obj.InStockNum;
            itemObj.AcceptDate = obj.AcceptDate;
            itemObj.BackNum = obj.BackNum;
            itemObj.StockID = obj.StockID;
            int id = MesStockInStockDao.Instance.Save<Mes_Stock_InStockItem>(itemObj);
            return Json(new { IsSuccess = true, Message = id.ToString() });
        }

        public ActionResult InStockItemMgt_Delete(int ID)
        {
            if (ID <= 0)
            {
                return Json(new { IsSuccess = false, Message = "选择的记录有误，请刷新后重试！" });
            }
            Mes_Stock_InStockItem obj = MesStockInStockDao.Instance.Find<Mes_Stock_InStockItem, int>(ID);
            if (obj == null)
            {
                return Json(new { IsSuccess = false, Message = "进货单明细不存在，请刷新后重试！" });
            }

            MesStockInStockDao.Instance.DeleteItemExt(obj);
            return Json(new { IsSuccess = true, Message = "" });
        }

        #endregion 明细信息

        #region 审核信息
        public ActionResult InStockApproval_FindByPage(Mes_Stock_InStockApproval obj)
        {
            var list = MesStockInStockApprovalDao.Instance.FindByCond(obj);
            int count = (list != null) ? list.Count : 0;
            return Json(new { total = count, rows = list }, JsonRequestBehavior.AllowGet);
        }

        #endregion 审核信息

    }
}
