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
    /// 计划管理
    /// </summary>
    public partial class PlanManagementController
    {
        public ActionResult WorkOrder()
        {
            return View();
        }

        public ActionResult WorkOrder_FindByPage(Mes_Plan_WorkOrder obj, int page, int rows)
        {
            var pager = new PagerBase() { CurrentPageIndex = page, PageSize = rows };
            var list = MesPlanWorkOrderDao.Instance.FindByPage(obj, ref pager);
            return Json(new { total = pager.TotalItemCount, rows = list }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult WorkOrder_Find(Mes_Plan_WorkOrder obj)
        {
            Mes_Plan_WorkOrder entity = MesPlanWorkOrderDao.Instance.Find(obj);
            if (entity == null)
            {
                return Json(new { IsSuccess = false, Message = "计划信息不存在，请刷新后重试！" });
            }

            return Json(new { IsSuccess = true, Message = JsonHelper.SerializeObject(entity) }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 工单保存
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public ActionResult WorkOrder_Save(Mes_Plan_WorkOrder obj)
        {
            string sMessage = string.Empty;

            if (string.IsNullOrEmpty(obj.WorkOrderNumber))
            {
                return Json(new { IsSuccess = false, Message = "工单单号信息不可谓空" });
            }
            if (string.IsNullOrEmpty(obj.PlanCode))
            {
                return Json(new { IsSuccess = false, Message = "工单对应的计划批号不能为空！" });
            }
            if (string.IsNullOrEmpty(obj.MaterialProNo) || string.IsNullOrEmpty(obj.Version))
            {
                return Json(new { IsSuccess = false, Message = "工单对应的产品信息及版本不能为空！" });
            }
                       
            if (obj.ID > 0)
            {
               
            }
            else
            {               
                obj.Creater = base.CurUser.UserId;
                obj.CreatedTime = DateTime.Now;
                //生成计划批号
                obj.WorkOrderNumber = MesPlanWorkOrderDao.Instance.GetWorkOrderNo();
            }
            bool result = MesPlanWorkOrderDao.Instance.Save(obj);
            if(!result)
            {
                sMessage = "保存失败";
                return Json(new { IsSuccess = false, Message = sMessage }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { IsSuccess = true, Message = sMessage },JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除工单
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public ActionResult WorkOrder_Delete(string ids)
        {
            string sMessage = string.Empty;
            bool result = MesPlanWorkOrderDao.Instance.Delete(ids);
            if (!result)
            {
                sMessage = "删除失败";
                return Json(new { IsSuccess = false, Message = sMessage });
            }
            return Json(new { IsSuccess = true, Message = sMessage });
        }

        /// <summary>
        /// 工单下达
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public ActionResult WorkOrder_Generate(Mes_Plan_WorkOrder obj)
        {
            string sMessage = string.Empty;
            bool result = MesPlanWorkOrderDao.Instance.GenerateWorkOrder(obj);
            if(!result)
            {
                sMessage = "工单下达失败";
                return Json(new {IsSuccess=false,Message=sMessage }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { IsSuccess = true, Message = sMessage }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 强制关闭工单
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public ActionResult WorkOrder_QzClose(Mes_Plan_WorkOrder obj)
        {
            string sMessage = string.Empty;
            bool result = MesPlanWorkOrderDao.Instance.QzCloseWorkOrder(obj);
            if(!result)
            {
                sMessage = "强制关闭失败";
                return Json(new { IsSuccess = false, Message = sMessage }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { IsSuccess = true, Message = sMessage }, JsonRequestBehavior.AllowGet);
        }

    }
}
