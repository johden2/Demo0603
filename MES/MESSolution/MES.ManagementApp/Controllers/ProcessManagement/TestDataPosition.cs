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
    public partial class ProcessManagementController
    {
        public ActionResult TestDataPosition()
        {
            return View();
        }

        /// <summary>
        /// 保存记录
        /// </summary>
        /// <returns></returns>
        public ActionResult TestDataPosition_Save(Mes_Tec_TestDataPosition obj)
        {
            string sMessage = string.Empty;
            //判断数据有效性
            if(string.IsNullOrEmpty(obj.ProcessCode))
            {
                sMessage = "工序编号不可为空";
                return Json(new { IsSuccess = false, Message = sMessage },JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(obj.DataTableName))
            {
                sMessage = "测试数据表名称不可为空";
                return Json(new { IsSuccess = false, Message = sMessage },JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(obj.DataProcessName))
            {
                sMessage = "测试工序不可为空";
                return Json(new { IsSuccess = false, Message = sMessage }, JsonRequestBehavior.AllowGet);
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
            bool result = MesTecTestDataPositionDao.Instance.Save(obj);
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
        public ActionResult TestDataPosition_Delete(string Ids)
        {            
            string sMessage = string.Empty;
            bool result = MesTecTestDataPositionDao.Instance.Delete(Ids);
            if(!result)
            {
                sMessage = "删除失败";
                return Json(new { IsSuccess = false, Message = sMessage });
            }
            return Json(new { IsSuccess = true, Message = sMessage });
        }

        public ActionResult TestDataPosition_FindByPage(Mes_Tec_TestDataPosition obj, int page, int rows)
        {
            var pager = new PagerBase() { CurrentPageIndex = page, PageSize = rows };
            var list = MesTecTestDataPositionDao.Instance.FindByPage(obj, ref pager);
            return Json(new { total = pager.TotalItemCount, rows = list });
        }
    }
}