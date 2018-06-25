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
    /// 工单物管理
    /// </summary>
    public partial class PlanManagementController
    {
        public ActionResult WorkOrderMaterial()
        {
            return View();
        }

        public ActionResult WorkOrderMaterial_FindByPage(Mes_Plan_Material obj, int page, int rows)
        {
            var pager = new PagerBase() { CurrentPageIndex = page, PageSize = rows };
            var list = MesPlanWorkOrderMaterialDao.Instance.FindByPage(obj, ref pager);
            return Json(new { total = pager.TotalItemCount, rows = list }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult WorkOrderMaterial_Find(Mes_Plan_Material obj)
        {
            Mes_Plan_Material entity = MesPlanWorkOrderMaterialDao.Instance.Find(obj);
            if (entity == null)
            {
                return Json(new { IsSuccess = false, Message = "工单物料信息不存在，请刷新后重试！" });
            }

            return Json(new { IsSuccess = true, Message = JsonHelper.SerializeObject(entity) }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 工单用料保存
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public ActionResult WorkOrderMaterial_Save(Mes_Plan_Material obj)
        {
            string sMessage = string.Empty;

            if (string.IsNullOrEmpty(obj.WorkOrderNumber))
            {
                return Json(new { IsSuccess = false, Message = "工单单号信息不可谓空" });
            }
            if (string.IsNullOrEmpty(obj.MaterialProNo) || string.IsNullOrEmpty(obj.MaterialCode) || string.IsNullOrEmpty(obj.Version))
            {
                return Json(new { IsSuccess = false, Message = "产品编码、名称、版本信息不能为空！" });
            }
            if (string.IsNullOrEmpty(obj.ProcessCode))
            {
                return Json(new { IsSuccess = false, Message = "工单对应的工艺不能为空！" });
            }
            if (string.IsNullOrEmpty(obj.SubMaterialProNo) || string.IsNullOrEmpty(obj.SubMaterialCode))
            {
                return Json(new { IsSuccess = false, Message = "物料编码或名称不可为空" }, JsonRequestBehavior.AllowGet);
            }
            if(obj.Num==0)
            {
                return Json(new { IsSuccess = false, Message = "物料用料数量必须填写" }, JsonRequestBehavior.AllowGet);
            }
            if (obj.ID > 0)
            {
               
            }
            else
            {               
                obj.Creater = base.CurUser.UserId;
                obj.CreatedTime = DateTime.Now;
                obj.ActionStatus = 1;
            }
            bool result = MesPlanWorkOrderMaterialDao.Instance.Save(obj);
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
        public ActionResult WorkOrderMaterial_Delete(string ids)
        {
            string sMessage = string.Empty;
            bool result = MesPlanWorkOrderMaterialDao.Instance.Delete(ids);
            if (!result)
            {
                sMessage = "删除失败";
                return Json(new { IsSuccess = false, Message = sMessage });
            }
            return Json(new { IsSuccess = true, Message = sMessage });
        }

        
    }
}
