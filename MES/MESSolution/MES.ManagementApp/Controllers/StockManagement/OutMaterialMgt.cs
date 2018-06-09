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

        public ActionResult OutMaterialMgt_FindByPage(Mes_Plan_SaleOrder obj, int page, int rows)
        {
            var pager = new PagerBase() { CurrentPageIndex = page, PageSize = rows };
            var list = MesPlanSaleOrderDao.Instance.FindByPage(obj, ref pager);
            return Json(new { total = pager.TotalItemCount, rows = list }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OutMaterialMgt_Find(int ID)
        {
            if (ID <= 0)
            {
                return Json(new { IsSuccess = false, Message = "选择的记录有误，请刷新后重试！" });
            }
            Mes_Plan_SaleOrder obj = MesPlanSaleOrderDao.Instance.Find<Mes_Plan_SaleOrder, int>(ID);
            if (obj == null)
            {
                return Json(new { IsSuccess = false, Message = "订单信息不存在，请刷新后重试！" });
            }

            return Json(new { IsSuccess = true, Message = JsonHelper.SerializeObject(obj) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OutMaterialMgt_Save(Mes_Plan_SaleOrder obj)
        {
            if (string.IsNullOrEmpty(obj.OrderType))
            {
                return Json(new { IsSuccess = false, Message = "【订单单别】不能为空！" });
            }
            if (obj.OrderDate <= TConvertHelper.SYS_MINDATE)
            {
                return Json(new { IsSuccess = false, Message = "【下单日期】不能为空！" });
            }
            if (obj.CustomerID <=0)
            {
                return Json(new { IsSuccess = false, Message = "【下单客户】不能为空！" });
            }

            Mes_Plan_SaleOrder order = null;
            if (obj.ID > 0)
            {
                order = MesPlanSaleOrderDao.Instance.Find<Mes_Plan_SaleOrder, int>(obj.ID);
            }
            else
            {
                order = new Mes_Plan_SaleOrder();
                order.OrderStatus = OrderStatus.YXD;
                order.Creater = base.CurUser.UserId;
                order.CreatedTime = DateTime.Now;
                //生成订单号
                order.OrderNo = MesPlanSaleOrderDao.Instance.GenOrderNo();
            }
            if (order == null)
            {
                return Json(new { IsSuccess = false, Message = "订单信息有误，请刷新后重试！" });
            }

            order.OrderType = obj.OrderType;
            order.OrderDate = obj.OrderDate;
            order.OrderAmount = obj.OrderAmount;
            order.CustomerID = obj.CustomerID;
            order.PaymentType = obj.PaymentType;
            order.Memo = obj.Memo;
            int id = MesPlanSaleOrderDao.Instance.Save(order);
            return Json(new { IsSuccess = true, Message = id.ToString() });
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public ActionResult OutMaterialMgt_Delete(int ID)
        {
            if (ID <= 0)
            {
                return Json(new { IsSuccess = false, Message = "选择的记录有误，请刷新后重试！" });
            }
            Mes_Plan_SaleOrder obj = MesPlanSaleOrderDao.Instance.Find<Mes_Plan_SaleOrder, int>(ID);
            if (obj == null)
            {
                return Json(new { IsSuccess = false, Message = "订单信息不存在，请刷新后重试！" });
            }
            if (obj.OrderStatus != OrderStatus.YXD)
            {
                return Json(new { IsSuccess = false, Message = "订单已在流程中，不允许作废！" });
            }

            MesPlanSaleOrderDao.Instance.DeleteExt(obj);
            return Json(new { IsSuccess = true, Message = "" });
        }

        #endregion 主信息

    }
}
