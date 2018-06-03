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
        public ActionResult PlanMgt()
        {
            return View();
        }

        public ActionResult PlanMgt_FindByPage(Mes_Plan_PlanInfor obj, int page, int rows)
        {
            var pager = new PagerBase() { CurrentPageIndex = page, PageSize = rows };
            var list = MesPlanPlanInforDao.Instance.FindByPage(obj, ref pager);
            return Json(new { total = pager.TotalItemCount, rows = list }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PlanMgt_Find(int ID)
        {
            if (ID <= 0)
            {
                return Json(new { IsSuccess = false, Message = "选择的记录有误，请刷新后重试！" });
            }
            Mes_Plan_PlanInfor obj = MesPlanPlanInforDao.Instance.Find<Mes_Plan_PlanInfor, int>(ID);
            if (obj == null)
            {
                return Json(new { IsSuccess = false, Message = "计划信息不存在，请刷新后重试！" });
            }

            return Json(new { IsSuccess = true, Message = JsonHelper.SerializeObject(obj) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PlanMgt_Save(Mes_Plan_PlanInfor obj)
        {
            if (string.IsNullOrEmpty(obj.OrderType) || string.IsNullOrEmpty(obj.OrderNo))
            {
                return Json(new { IsSuccess = false, Message = "请选择计划对应的工单！" });
            }
            if (string.IsNullOrEmpty(obj.MaterialProNo) || string.IsNullOrEmpty(obj.Version))
            {
                return Json(new { IsSuccess = false, Message = "工单对应的产品信息不能为空！" });
            }
            Mes_Plan_PlanInfor plan = null;
            if (obj.ID > 0)
            {
                plan = MesPlanPlanInforDao.Instance.Find<Mes_Plan_PlanInfor, int>(obj.ID);
            }
            else
            {
                plan = new Mes_Plan_PlanInfor();
                plan.PlanStatus = PlanStatus.A;
                plan.Creater = base.CurUser.UserId;
                plan.CreatedTime = DateTime.Now;
                //生成计划批号
                plan.PlanCode = MesPlanPlanInforDao.Instance.GenPlanNo();
            }
            if (plan == null)
            {
                return Json(new { IsSuccess = false, Message = "计划信息有误，请刷新后重试！" });
            }

            plan.PlanType = obj.PlanType;
            plan.OrderType = obj.OrderType;
            plan.OrderNo = obj.OrderNo;
            plan.MaterialProNo = obj.MaterialProNo;
            plan.MaterialCode = obj.MaterialCode;
            plan.Version = obj.Version;
            plan.PlanNum = obj.PlanNum;
            plan.CompleteNum = obj.CompleteNum;
            plan.Unit = obj.Unit;
            plan.WorkShopID = obj.WorkShopID;
            plan.BeginTime = obj.BeginTime;
            plan.EndTime = obj.EndTime;
            int id = MesPlanPlanInforDao.Instance.Save(plan);
            return Json(new { IsSuccess = true, Message = id.ToString() });
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public ActionResult PlanMgt_Delete(int ID)
        {
            if (ID <= 0)
            {
                return Json(new { IsSuccess = false, Message = "选择的记录有误，请刷新后重试！" });
            }
            Mes_Plan_PlanInfor obj = MesPlanPlanInforDao.Instance.Find<Mes_Plan_PlanInfor, int>(ID);
            if (obj == null)
            {
                return Json(new { IsSuccess = false, Message = "计划信息不存在，请刷新后重试！" });
            }
            if (obj.PlanStatus != PlanStatus.A)
            {
                return Json(new { IsSuccess = false, Message = "计划已在流程中，不允许作废！" });
            }

            MesPlanPlanInforDao.Instance.Delete(obj);
            return Json(new { IsSuccess = true, Message = "" });
        }
    }
}
