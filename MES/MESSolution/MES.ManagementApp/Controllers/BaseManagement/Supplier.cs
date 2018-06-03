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
        public ActionResult Supplier()
        {
            return View();
        }

        /// <summary>
        /// 保存记录
        /// </summary>
        /// <returns></returns>
        public ActionResult Supplier_Save(Mes_Sys_Supplier obj)
        {
            string sMessage = string.Empty;
            //判断数据有效性
            if(string.IsNullOrEmpty(obj.SupplierCode))
            {
                sMessage = "供应商编号不可为空";
                return Json(new { IsSuccess = false, Message = sMessage },JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(obj.SupplierName))
            {
                sMessage = "供应商名称不可为空";
                return Json(new { IsSuccess = false, Message = sMessage },JsonRequestBehavior.AllowGet);
            }
            if (obj.ID > 0)
            {
                obj.Modifier = base.CurUser.ID;
                obj.ModifiedTime = DateTime.Now;
            }
            else
            {
                obj.Creater = base.CurUser.ID;
                obj.CreatedTime = DateTime.Now;
            }
            bool result = MesSysSupplierDao.Instance.Save(obj);
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
        public ActionResult Supplier_Delete(string Ids)
        {            
            string sMessage = string.Empty;
            bool result = MesSysSupplierDao.Instance.Delete(Ids);
            if(!result)
            {
                sMessage = "删除失败";
                return Json(new { IsSuccess = false, Message = sMessage });
            }
            return Json(new { IsSuccess = true, Message = sMessage });
        }

        public ActionResult Supplier_FindByPage(Mes_Sys_Supplier obj, int page, int rows)
        {
            var pager = new PagerBase() { CurrentPageIndex = page, PageSize = rows };
            var list = MesSysSupplierDao.Instance.FindByPage(obj, ref pager);
            return Json(new { total = pager.TotalItemCount, rows = list });
        }
    }
}