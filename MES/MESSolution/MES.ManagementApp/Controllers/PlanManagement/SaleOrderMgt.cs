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
    /// 订单管理
    /// </summary>
    public partial class PlanManagementController
    {
        #region 订单主信息

        public ActionResult SaleOrderMgt()
        {
            return View();
        }

        public ActionResult SaleOrderMgt_FindByPage(Mes_Plan_SaleOrder obj, int page, int rows)
        {
            var pager = new PagerBase() { CurrentPageIndex = page, PageSize = rows };
            var list = MesPlanSaleOrderDao.Instance.FindByPage(obj, ref pager);
            return Json(new { total = pager.TotalItemCount, rows = list }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaleOrderMgt_Find(int ID)
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

        public ActionResult SaleOrderMgt_Save(Mes_Plan_SaleOrder obj)
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
        public ActionResult SaleOrderMgt_Delete(int ID)
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

        #endregion 订单主信息

        #region 订单明细信息

        public ActionResult SaleOrderItemMgt()
        {
            PageModel page = new PageModel();
            page.OptType = TConvertHelper.FormatDBInt(Request.Params["OptType"]);
            page.KeyID = Request.Params["ID"];
            return View(page);
        }

        public ActionResult SaleOrderItemMgt_FindByPage(Mes_Plan_SaleOrderItem obj, int page, int rows)
        {
            var pager = new PagerBase() { CurrentPageIndex = page, PageSize = rows };
            var list = MesPlanSaleOrderDao.Instance.FindItemByPage(obj, ref pager);
            return Json(new { total = pager.TotalItemCount, rows = list }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaleOrderItemMgt_Save(Mes_Plan_SaleOrderItem obj)
        {
            if (obj.OrderID <= 0)
            {
                return Json(new { IsSuccess = false, Message = "请先选择一个订单进行操作！" });
            }
            if (string.IsNullOrEmpty(obj.MaterialProNo))
            {
                return Json(new { IsSuccess = false, Message = "【产品编码】不能为空！" });
            }
            if (string.IsNullOrEmpty(obj.Version))
            {
                return Json(new { IsSuccess = false, Message = "【产品版本】不能为空！" });
            }
            Mes_Plan_SaleOrder order = MesPlanSaleOrderDao.Instance.Find<Mes_Plan_SaleOrder, int>(obj.OrderID);
            if (order == null)
            {
                return Json(new { IsSuccess = false, Message = "选择的主订单信息有误！" });
            }

            Mes_Plan_SaleOrderItem itemObj = null;
            if (obj.ID > 0)
            {
                itemObj = MesPlanSaleOrderDao.Instance.Find<Mes_Plan_SaleOrderItem, int>(obj.ID);
            }
            else
            {
                itemObj = new Mes_Plan_SaleOrderItem();
                itemObj.OrderType = order.OrderType;
                itemObj.OrderNo = order.OrderNo;
                itemObj.Creater = base.CurUser.UserId;
                itemObj.CreatedTime = DateTime.Now;
                //生成订单行号
                itemObj.ResNum = MesPlanSaleOrderDao.Instance.GenOrderResNum(order);
            }
            if (itemObj == null)
            {
                return Json(new { IsSuccess = false, Message = "订单明细信息有误，请刷新后重试！" });
            }

            itemObj.MaterialProNo = obj.MaterialProNo;
            itemObj.MaterialCode = obj.MaterialCode;
            itemObj.Version = obj.Version;
            itemObj.Num = obj.Num;
            itemObj.ShipDate = obj.ShipDate;
            itemObj.AlNum = obj.AlNum;
            itemObj.Memo = obj.Memo;
            int id = MesPlanSaleOrderDao.Instance.Save<Mes_Plan_SaleOrderItem>(itemObj);
            return Json(new { IsSuccess = true, Message = id.ToString() });
        }

        public ActionResult SaleOrderItemMgt_Delete(int ID)
        {
            if (ID <= 0)
            {
                return Json(new { IsSuccess = false, Message = "选择的记录有误，请刷新后重试！" });
            }
            Mes_Plan_SaleOrderItem obj = MesPlanSaleOrderDao.Instance.Find<Mes_Plan_SaleOrderItem, int>(ID);
            if (obj == null)
            {
                return Json(new { IsSuccess = false, Message = "订单明细不存在，请刷新后重试！" });
            }

            MesPlanSaleOrderDao.Instance.DeleteItemExt(obj);
            return Json(new { IsSuccess = true, Message = "" });
        }

        #endregion End 订单明细信息

    }
}
