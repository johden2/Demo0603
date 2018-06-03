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
        public ActionResult DataCollectPoint()
        {
            return View();
        }

        /// <summary>
        /// 保存记录
        /// </summary>
        /// <returns></returns>
        public ActionResult DataCollectPoint_Save(Mes_Sys_DataCollectPoint obj)
        {
            string sMessage = string.Empty;
            //判断数据有效性
            if(string.IsNullOrEmpty(obj.CollectCode))
            {
                sMessage = "采集点编号不可为空";
                return Json(new { IsSuccess = false, Message = sMessage },JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(obj.CollectName))
            {
                sMessage = "采集点名称不可为空";
                return Json(new { IsSuccess = false, Message = sMessage },JsonRequestBehavior.AllowGet);
            }
            if (obj.ID > 0)
            {
                obj.Modifier = base.CurUser.UserId;
                obj.ModifiedTime = DateTime.Now;
            }
            else
            {
                obj.Creater = base.CurUser.UserId;
                obj.CreatedTime = DateTime.Now;
            }
            bool result = MesSysDataCollectPointDao.Instance.Save(obj);
            if(!result)
            {
                sMessage = "保存失败";
                return Json(new { IsSuccess = false, Message =sMessage },JsonRequestBehavior.AllowGet);
            }
            return Json(new { IsSuccess = true, Message = sMessage },JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <returns></returns>
        public ActionResult DataCollectPoint_Delete(string Ids)
        {            
            string sMessage = string.Empty;
            bool result = MesSysDataCollectPointDao.Instance.Delete(Ids);
            if(!result)
            {
                sMessage = "删除失败";
                return Json(new { IsSuccess = false, Message = sMessage });
            }
            return Json(new { IsSuccess = true, Message = sMessage });
        }

        public ActionResult DataCollectPoint_FindByPage(Mes_Sys_DataCollectPoint obj, int page, int rows)
        {
            var pager = new PagerBase() { CurrentPageIndex = page, PageSize = rows };
            var list = MesSysDataCollectPointDao.Instance.FindByPage(obj, ref pager);
            return Json(new { total = pager.TotalItemCount, rows = list });
        }
    }
}