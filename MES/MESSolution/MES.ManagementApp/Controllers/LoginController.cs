using Sys.Dao;
using Sys.Model;
using Sys.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace MES.ManagementApp.Controllers
{
    public class LoginController : Controller
    {
        public const string UserKey = "UserInfo";

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 校验登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CheckLogin(Mes_Sys_User obj)
        {
            if (string.IsNullOrEmpty(obj.UserID))
            {
                return Json(new { IsSuccess = false, Message = "请输入用户账号！" });
            }
            if (string.IsNullOrEmpty(obj.Pass))
            {
                return Json(new { IsSuccess = false, Message = "请输入密码！" });
            }

            try
            {
                string password = obj.Pass;
                obj.Pass = password; //EncryptUtil.MD5Password(obj.Password).ToLower(); //md5加密
                Mes_Sys_User user = MesSysUserDao.Instance.GetUser(obj);
                if (user == null || user.ID <= 0)
                {
                    return Json(new { IsSuccess = false, Message = "账号或密码有误！" });
                }

                UserModel model = new UserModel();
                model.ID = user.ID;
                model.UserId = user.UserID;
                model.Password = password;
                model.UserName = user.UserName;
                model.IsAdmin = (user.IsAdmin == "Y");
                model.OrgID = user.OrgID;

                //写入Cookie和Session
                string sUserInfo = model.UserId + "|" + password;
                sUserInfo = EncryptUtil.DesEncrypt(sUserInfo);
                //FormsAuthentication.SetAuthCookie(model.LoginName, true, "name");//加入from验证票据
                //FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,model.LoginName, DateTime.Now, DateTime.Now.AddDays(7), true, sUserInfo);
                //FormsIdentity identity = new FormsIdentity(ticket);
                //HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                //Response.Cookies.Remove(cookie.Name);
                HttpCookie cookie = new HttpCookie(UserKey, sUserInfo);
                cookie.Expires = DateTime.Now.AddDays(1); //Cookie设置为1天内过期
                Response.Cookies.Add(cookie);

                //SessionManager.Instance.AddSession(UserKey, model);

                //string LoginIp = AppHelper.GetClientIP();
                //string userName = user.LoginName;
                //Sys_LoginLogDao.Instance.SaveLoginLog(userName, LoginIp);

                return Json(new { IsSuccess = true, Message = "登录成功" });
            }
            catch (System.Exception ex)
            {
                return Json(new { IsSuccess = false, Message = "登录失败，请确认账号和密码正确" });
            }
        }


        public ActionResult LoginOut()
        {
            CookiesHelper.ClearCookie(UserKey);
            SessionManager.Instance.RemoveSession(UserKey);
            return Json(new { IsSuccess = true, Message = "" });
        }

    }
}
