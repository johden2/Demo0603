using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Sys.Dao;
using Sys.Model;
using Sys.Tools;


namespace MES.ManagementApp.Controllers
{
    public partial class BaseManagementController
    {
        /// <summary>
        /// 流程配置
        /// </summary>
        /// <returns></returns>
        public ActionResult FlowConfig()
        {
            return View();
        }

        public ActionResult FlowConfig_FindByPage(Mes_Sys_FlowConfig obj, int page, int rows)
        {
            var pager = new PagerBase() { CurrentPageIndex = page, PageSize = rows };
            var list = MesSysFlowConfigDao.Instance.FindByPage(obj, ref pager);
            return Json(new { total = pager.TotalItemCount, rows = list });
        }

        /// <summary>
        /// 保存记录
        /// </summary>
        /// <returns></returns>
        public ActionResult FlowConfig_Save(Mes_Sys_FlowConfig obj)
        {
            //判断数据有效性
            if(string.IsNullOrEmpty(obj.OptUserName))
            {
                return Json(new { IsSuccess = false, Message =  "操作人员不可为空" },JsonRequestBehavior.AllowGet);
            }
            if (obj.ID <=0)
            {
                obj.Creater = base.CurUser.UserId;
                obj.CreatedTime = DateTime.Now;
            }
            int result = MesSysFlowConfigDao.Instance.Save(obj);
            if (result <=0 )
            {
                return Json(new { IsSuccess = false, Message ="保存失败" },JsonRequestBehavior.AllowGet);
            }
            return Json(new { IsSuccess = true, Message = "" },JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <returns></returns>
        public ActionResult FlowConfig_Delete(string Ids)
        {            
            bool result = MesSysFlowConfigDao.Instance.DeleteExt(Ids);
            if(!result)
            {
                return Json(new { IsSuccess = false, Message = "删除失败" });
            }

            return Json(new { IsSuccess = true, Message = "" });
        }

    }
}