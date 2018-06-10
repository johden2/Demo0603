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
    /// 领料单
    /// </summary>
    public partial class StockManagementController
    {

        #region 主信息

        public ActionResult OutMaterialMgt()
        {
            return View();
        }

        public ActionResult OutMaterialMgt_FindByPage(Mes_Stock_OutMaterial obj, int page, int rows)
        {
            var pager = new PagerBase() { CurrentPageIndex = page, PageSize = rows };
            var list = MesStockOutMaterialDao.Instance.FindByPage(obj, ref pager);
            return Json(new { total = pager.TotalItemCount, rows = list }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OutMaterialMgt_Find(int ID)
        {
            if (ID <= 0)
            {
                return Json(new { IsSuccess = false, Message = "选择的记录有误，请刷新后重试！" });
            }
            Mes_Stock_OutMaterial obj = MesStockOutMaterialDao.Instance.Find<Mes_Stock_OutMaterial, int>(ID);
            if (obj == null)
            {
                return Json(new { IsSuccess = false, Message = "信息不存在，请刷新后重试！" });
            }

            return Json(new { IsSuccess = true, Message = JsonHelper.SerializeObject(obj) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OutMaterialMgt_Save(Mes_Stock_OutMaterial obj)
        {
            if (string.IsNullOrEmpty(obj.BillType))
            {
                return Json(new { IsSuccess = false, Message = "请选择进货单单别！" });
            }
            if (obj.OrgID <= 0)
            {
                return Json(new { IsSuccess = false, Message = "请选择班组！" });
            }
            Mes_Stock_OutMaterial dataObj = null;
            if (obj.ID > 0)
            {
                dataObj = MesStockOutMaterialDao.Instance.Find<Mes_Stock_OutMaterial, int>(obj.ID);
            }
            else
            {
                dataObj = new Mes_Stock_OutMaterial();
                dataObj.AuditStatus = AuditEnum.No;
                dataObj.Creater = base.CurUser.UserId;
                dataObj.CreatedTime = DateTime.Now;
                //生成单号
                dataObj.BillNo = MesStockOutMaterialDao.Instance.GenBillNo();
            }
            if (dataObj == null)
            {
                return Json(new { IsSuccess = false, Message = "进货单信息有误，请刷新后重试！" });
            }
            dataObj.BillType = obj.BillType;
            dataObj.OutStockDate = obj.OutStockDate;
            dataObj.OptPerson = obj.OptPerson;
            dataObj.OrgID = obj.OrgID;
            dataObj.OrgName = obj.OrgName;
            dataObj.BillDate = obj.BillDate;
            dataObj.TotalNum = obj.TotalNum;
            dataObj.Memo = obj.Memo;
            dataObj.Factory = obj.Factory;
            int id = MesStockOutMaterialDao.Instance.Save(dataObj);
            return Json(new { IsSuccess = true, Message = id.ToString() });
        }

        public ActionResult OutMaterialMgt_Delete(int ID)
        {
            if (ID <= 0)
            {
                return Json(new { IsSuccess = false, Message = "选择的记录有误，请刷新后重试！" });
            }
            Mes_Stock_OutMaterial obj = MesStockOutMaterialDao.Instance.Find<Mes_Stock_OutMaterial, int>(ID);
            if (obj == null)
            {
                return Json(new { IsSuccess = false, Message = "进货单不存在，请刷新后重试！" });
            }
            if (obj.AuditStatus != AuditEnum.No)
            {
                return Json(new { IsSuccess = false, Message = "只能删除未审核的进货单！" });
            }

            MesStockOutMaterialDao.Instance.DeleteExt(ID);
            return Json(new { IsSuccess = true, Message = "" });
        }


        #endregion 主信息

        #region 明细信息

        public ActionResult OutMaterialItemMgt()
        {
            PageModel page = new PageModel();
            page.OptType = TConvertHelper.FormatDBInt(Request.Params["OptType"]);
            page.KeyID = Request.Params["ID"];
            return View(page);
        }

        public ActionResult OutMaterialItemMgt_FindByPage(Mes_Stock_OutMaterialItem obj, int page, int rows)
        {
            var pager = new PagerBase() { CurrentPageIndex = page, PageSize = rows };
            var list = MesStockOutMaterialDao.Instance.FindItemByPage(obj, ref pager);
            return Json(new { total = pager.TotalItemCount, rows = list }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OutMaterialItemMgt_Save(Mes_Stock_OutMaterialItem obj)
        {
            if (obj.OutMaterialID <= 0)
            {
                return Json(new { IsSuccess = false, Message = "请先选择一个领料单进行操作！" });
            }
            if (string.IsNullOrEmpty(obj.MaterialProNo))
            {
                return Json(new { IsSuccess = false, Message = "【物料编码】不能为空！" });
            }
            Mes_Stock_OutMaterial main = MesStockOutMaterialDao.Instance.Find<Mes_Stock_OutMaterial, int>(obj.OutMaterialID.Value);
            if (main == null)
            {
                return Json(new { IsSuccess = false, Message = "选择的进货单信息有误！" });
            }

            Mes_Stock_OutMaterialItem itemObj = null;
            if (obj.ID > 0)
            {
                itemObj = MesStockOutMaterialDao.Instance.Find<Mes_Stock_OutMaterialItem, int>(obj.ID);
            }
            else
            {
                itemObj = new Mes_Stock_OutMaterialItem();
                itemObj.WorkOrderType = "";
                //生成订单行号
                itemObj.ResNum = MesStockOutMaterialDao.Instance.GenResNum(main);
            }
            if (itemObj == null)
            {
                return Json(new { IsSuccess = false, Message = "订单明细信息有误，请刷新后重试！" });
            }

            itemObj.OutMaterialID = obj.OutMaterialID;
            itemObj.BillType = main.BillType;
            itemObj.BillNo = main.BillNo;
            itemObj.MaterialProNo = obj.MaterialProNo;
            itemObj.MaterialCode = obj.MaterialCode;
            itemObj.Version = obj.Version;
            itemObj.MaterialSize = obj.MaterialSize;
            itemObj.Unit = obj.Unit;
            itemObj.BatchNo = obj.BatchNo;
            itemObj.Num = obj.Num;
            itemObj.PlanNo = obj.PlanNo;
            itemObj.Memo = obj.Memo;
            itemObj.StockID = obj.StockID;
            itemObj.StockName = obj.StockName;
            itemObj.AlibraryID = obj.AlibraryID;
            itemObj.AlibraryName = obj.AlibraryName;
            itemObj.ProcessID = obj.ProcessID;
            itemObj.ProcessName = obj.ProcessName;
            int id = MesStockOutMaterialDao.Instance.Save<Mes_Stock_OutMaterialItem>(itemObj);
            return Json(new { IsSuccess = true, Message = id.ToString() });
        }

        public ActionResult OutMaterialItemMgt_Delete(int ID)
        {
            if (ID <= 0)
            {
                return Json(new { IsSuccess = false, Message = "选择的记录有误，请刷新后重试！" });
            }
            Mes_Stock_OutMaterialItem obj = MesStockOutMaterialDao.Instance.Find<Mes_Stock_OutMaterialItem, int>(ID);
            if (obj == null)
            {
                return Json(new { IsSuccess = false, Message = "进货单明细不存在，请刷新后重试！" });
            }

            MesStockOutMaterialDao.Instance.DeleteItemExt(obj);
            return Json(new { IsSuccess = true, Message = "" });
        }

        #endregion 明细信息

    }
}
