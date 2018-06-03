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
    /// 员工管理
    /// </summary>
    public partial class PersonnelManagementController
    {
        public ActionResult EmployeeMgt()
        {
            return View();
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult EmployeeMgt_FindByPage(Mes_Sys_Employee obj, int page, int rows)
        {
            var pager = new PagerBase() { CurrentPageIndex = page, PageSize = rows };
            var list = MesSysEmployeeDao.Instance.FindByPage(obj, ref pager);
            return Json(new { total = pager.TotalItemCount, rows = list }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public ActionResult EmployeeMgt_Save(Mes_Sys_Employee obj)
        {
            if (string.IsNullOrEmpty(obj.EmpNo))
            {
                return Json(new { IsSuccess = false, Message = "工号不能为空！" });
            }
            if (string.IsNullOrEmpty(obj.EmpName))
            {
                return Json(new { IsSuccess = false, Message = "用户名不能为空！" });
            }
            if (obj.ID <= 0)
            {
                obj.RecordStatus = YesNoType.Yes;
                obj.Creater = base.CurUser.UserId;
                obj.CreatedTime = DateTime.Now;
            }

            MesSysEmployeeDao.Instance.Save(obj);
            return Json(new { IsSuccess = true, Message = "操作成功！" });
        }
        public ActionResult EmployeeMgt_Delete(int ID)
        {
            if (ID <= 0)
            {
                return Json(new { IsSuccess = false, Message = "选择的记录有误，请刷新后重试！" });
            }
            Mes_Sys_Employee obj = MesSysEmployeeDao.Instance.Find<Mes_Sys_Employee, int>(ID);
            if (obj == null)
            {
                return Json(new { IsSuccess = false, Message = "员工信息有误！" });
            }

            MesSysEmployeeDao.Instance.Delete<Mes_Sys_Employee>(obj);
            return Json(new { IsSuccess = true, Message = "" });
        }
    }
}
