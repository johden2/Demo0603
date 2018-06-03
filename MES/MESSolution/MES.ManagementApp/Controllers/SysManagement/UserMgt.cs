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
    /// 系统管理
    /// </summary>
    public partial class SysManagementController
    {
        public ActionResult UserMgt()
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
        public ActionResult UserMgt_FindByPage(Mes_Sys_User obj, int page, int rows)
        {
            var pager = new PagerBase() { CurrentPageIndex = page, PageSize = rows };
            var list = MesSysUserDao.Instance.FindByPage(obj, ref pager);
            return Json(new { total = pager.TotalItemCount, rows = list }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 用户保存
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public ActionResult UserMgt_Save(Mes_Sys_User obj)
        {
            if (string.IsNullOrEmpty(obj.UserID))
            {
                return Json(new { IsSuccess = false, Message = "工号不能为空！" });
            }
            if (string.IsNullOrEmpty(obj.UserName))
            {
                return Json(new { IsSuccess = false, Message = "用户名不能为空！" });
            }
            if (obj.ID <= 0)
            {
                obj.Pass = "123456";
                obj.RecordStatus = YesNoType.Yes;
                obj.Creater = base.CurUser.UserId;
                obj.CreatedTime = DateTime.Now;
            }

            MesSysUserDao.Instance.SaveExt(obj);
            return Json(new { IsSuccess = true, Message = "操作成功！" });
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult UserMgt_Delete(string ids)
        {
            string message = string.Empty;
            if (string.IsNullOrEmpty(ids))
            {
                return Json(new { IsSuccess = false, Message = "请选择需要操作的记录！" });
            }

            MesSysUserDao.Instance.DeleteList(ids);
            return Json(new { IsSuccess = true, Message = message });
        }

        /// <summary>
        /// 密码重置
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult UserMgt_ResetPassword(int ID)
        {
            string message = string.Empty;
            if (ID <= 0)
            {
                return Json(new { IsSuccess = false, Message = "选择的记录有误，请刷新后重试！" });
            }
            Mes_Sys_User obj = MesSysUserDao.Instance.Find<Mes_Sys_User, int>(ID);
            if (obj == null)
            {
                return Json(new { IsSuccess = false, Message = "用户信息有误！" });
            }

            obj.Pass = "123456";
            MesSysUserDao.Instance.Save<Mes_Sys_User>(obj);
            return Json(new { IsSuccess = true, Message = message });
        }

    }
}
