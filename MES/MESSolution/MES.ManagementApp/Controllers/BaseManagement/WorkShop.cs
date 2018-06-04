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
        public ActionResult WorkShop()
        {
            return View();
        }

        /// <summary>
        /// 保存记录
        /// </summary>
        /// <returns></returns>
        public ActionResult WorkShop_Save(Mes_Sys_WorkShop obj)
        {
            string sMessage = string.Empty;
            //判断数据有效性
            if(string.IsNullOrEmpty(obj.WorkShopCode))
            {
                sMessage = "车间编码不可为空";
                return Json(new { IsSuccess = false, Message = sMessage });
            }
            if (string.IsNullOrEmpty(obj.WorkShopName))
            {
                sMessage = "车间名称不可为空";
                return Json(new { IsSuccess = false, Message = sMessage });
            }
            obj.Creater = base.CurUser.ID;
            obj.CreatedTime = DateTime.Now;
            bool result = MesSysWorkShopDao.Instance.Save(obj);
            if(!result)
            {
                sMessage = "保存失败";
                return Json(new { IsSuccess = false, Message =sMessage });
            }
            return Json(new { IsSuccess = true, Message = sMessage });
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <returns></returns>
        public ActionResult WorkShop_Delete(string Ids)
        {            
            string sMessage = string.Empty;
            bool result = MesSysWorkShopDao.Instance.Delete(Ids);
            if(!result)
            {
                sMessage = "删除失败";
                return Json(new { IsSuccess = false, Message = sMessage });
            }
            return Json(new { IsSuccess = true, Message = sMessage });
        }

        public ActionResult WorkShop_FindByPage(Mes_Sys_WorkShop obj,int page,int rows)
        {
            var pager = new PagerBase() { CurrentPageIndex = page, PageSize = rows };
            var list = MesSysWorkShopDao.Instance.FindByPage(obj, ref pager);
            return Json(new { total = pager.TotalItemCount, rows = list });

        }

        public ActionResult WorkShop_Select(int page, int rows, string q)
        {
            List<Mes_Sys_WorkShop> list = null;
            Mes_Sys_WorkShop obj = new Mes_Sys_WorkShop();
            if (!string.IsNullOrEmpty(q))
            {
                obj.WorkShopName = q;
            }
            var pager = new PagerBase() { CurrentPageIndex = page, PageSize = rows };
            list = MesSysWorkShopDao.Instance.FindByPage(obj, ref pager);
            if (list == null)
            {
                list = new List<Mes_Sys_WorkShop>();
            }

            return Json(new { total = pager.TotalItemCount, rows = list, JsonRequestBehavior.AllowGet });
        }

    }
}