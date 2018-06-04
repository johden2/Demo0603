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
        public ActionResult Customer()
        {
            return View();
        }

        /// <summary>
        /// 保存记录
        /// </summary>
        /// <returns></returns>
        public ActionResult Customer_Save(Mes_Sys_Customer obj)
        {
            string sMessage = string.Empty;
            //判断数据有效性
            if(string.IsNullOrEmpty(obj.CustomerCode))
            {
                sMessage = "客户编码不可为空";
                return Json(new { IsSuccess = false, Message = sMessage },JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(obj.CustomerName))
            {
                sMessage = "客户名称不可为空";
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
            bool result = MesSysCustomerDao.Instance.Save(obj);
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
        public ActionResult Customer_Delete(string Ids)
        {            
            string sMessage = string.Empty;
            bool result = MesSysCustomerDao.Instance.Delete(Ids);
            if(!result)
            {
                sMessage = "删除失败";
                return Json(new { IsSuccess = false, Message = sMessage });
            }
            return Json(new { IsSuccess = true, Message = sMessage });
        }

        public ActionResult Customer_FindByPage(Mes_Sys_Customer obj, int page, int rows)
        {
            var pager = new PagerBase() { CurrentPageIndex = page, PageSize = rows };
            var list = MesSysCustomerDao.Instance.FindByPage(obj, ref pager);
            return Json(new { total = pager.TotalItemCount, rows = list });
        }

        public ActionResult Customer_Select(int page, int rows, string q)
        {
            List<Mes_Sys_Customer> list = null;
            Mes_Sys_Customer obj = new Mes_Sys_Customer();
            if (!string.IsNullOrEmpty(q))
            {
                obj.CustomerName = q;
            }
            var pager = new PagerBase() { CurrentPageIndex = page, PageSize = rows };
            list = MesSysCustomerDao.Instance.FindByPage(obj, ref pager);
            if (list == null)
            {
                list = new List<Mes_Sys_Customer>();
            }

            return Json(new { total = pager.TotalItemCount, rows = list,JsonRequestBehavior.AllowGet });
        }


    }
}