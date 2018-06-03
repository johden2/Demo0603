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
        public ActionResult SysMoudle()
        {
            return View();
        }

        public ActionResult SysMoudle_GetList(Mes_Sys_ModuleItem entity)
        {
            string userName = base.CurUser.UserId;
            bool isAdmin = base.CurUser.IsAdmin;
            List<Mes_Sys_ModuleItem> list = MesSysModuleItemDao.Instance.FindByCondExt(entity, isAdmin, userName);
            string jsonStr = JsonHelper.SerializeObject(list);
            return Json(new { IsSuccess = true, Message = jsonStr });
        }


        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string SysUser_ChangePwd(string oldpwd, string pwd, string npwd)
        {
            if (string.IsNullOrEmpty(oldpwd)) return "原密码不能为空！";

            if (string.IsNullOrEmpty(pwd) || string.IsNullOrEmpty(npwd))
            {
                return "新密码和重复密码不能为空！";
            }

            string message = RegProvider.IsPassword("新密码和重复密码", pwd);
            if (!string.IsNullOrEmpty(message)) return message;

            if (pwd != npwd) return "新密码和重复密码不一致！";

            Mes_Sys_User obj = new Mes_Sys_User();
            obj.UserID = base.CurUser.UserId;
            obj.Pass = oldpwd;
            Mes_Sys_User u = MesSysUserDao.Instance.GetUser(obj);
            if (u == null || u.ID < 0) return "原密码有误！";

            u.Pass = pwd;
            MesSysUserDao.Instance.Save<Mes_Sys_User>(u);
            return "OK";
        }

    }
}
