using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.ComponentModel;
using Sys.Model;
using Sys.Tools;
using Sys.Dao;

namespace MES.ManagementApp.Controllers
{
    /// <summary>
    /// 权限拦截
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizeFilterAttribute : ActionFilterAttribute
    {
        private const string UserKey = "UserInfo";
        //public UserModel _CurUser = null;

        /// <summary>
        /// 验证用户登录信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool ValidUser(HttpContextBase context)
        {
            UserModel _CurUser = null; //移到内部了

            //测试时使用
            this.Test();
            return true;
            
            //1.校验Session
            if (_CurUser != null && _CurUser.ID > 0) return true;

            _CurUser = SessionManager.Instance.GetSession<UserModel>(UserKey);
            if (_CurUser == null)
            {
                //2.校验Cookie
                HttpCookie cookie = context.Request.Cookies[UserKey];
                if (cookie == null || string.IsNullOrEmpty(cookie.Value))
                {
                    return false;
                }

                string sUserInfo = cookie.Value;
                sUserInfo = EncryptUtil.DesDecrypt(sUserInfo);
                if (sUserInfo.IndexOf("|") < 0)
                {
                    return false;
                }
                string loginName = sUserInfo.Split('|')[0];
                string password = sUserInfo.Split('|')[1];
                if (string.IsNullOrEmpty(loginName) || string.IsNullOrEmpty(password))
                {
                    return false;
                }

                //3.校验用户名、密码
                string oldPassword = password;
                //password = EncryptUtil.MD5Password(password).ToLower(); //md5加密
                Mes_Sys_User obj = new Mes_Sys_User() { UserID = loginName, Pass = password };
                Mes_Sys_User user = MesSysUserDao.Instance.GetUser(obj);
                if (user == null || user.ID <= 0) return false;

                //4.校验Cookie成功，写入账号
                _CurUser = new UserModel();
                _CurUser.ID = user.ID;
                _CurUser.UserId = user.UserID;
                _CurUser.Password = password;
                _CurUser.UserName = user.UserName;
                _CurUser.IsAdmin = (user.IsAdmin == "Y");
                _CurUser.OrgID = user.OrgID;

                SessionManager.Instance.AddSession(UserKey, _CurUser);
                return true;
            }
            return true;
        }

        public void Test()
        {
            UserModel _CurUser = null;
            _CurUser = SessionManager.Instance.GetSession<UserModel>(UserKey);
            if (_CurUser == null)
            {
                _CurUser = new UserModel();
                _CurUser.ID = 1;
                _CurUser.UserId = "admin";
                _CurUser.Password = "";
                _CurUser.UserName = "admin";
                _CurUser.IsAdmin = true;
                _CurUser.OrgID = 1;
            }
            SessionManager.Instance.AddSession(UserKey, _CurUser);

        }

        /// <summary>
        /// 动作执行前,对授权情况进行验证
        /// </summary>
        /// <param name="filterContext">上下文参数</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //校验用户信息是否合法
            if (!this.ValidUser(filterContext.HttpContext))
            {
                //添加头部信息 前端会有ajax回调 判断
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.HttpContext.Response.AddHeader("sessionstatus", "timeout");
                    return;
                }
                else
                {
                    //正常的请求
                    filterContext.Result = new RedirectResult("/Login/Index");
                    HttpContext.Current.Response.Write("<script>window.top.location.href = '/Login/Index'</script>");
                }
            }
        }

        ///// <summary>
        ///// 动作执行后
        ///// </summary>
        ///// <param name="filterContext"></param>
        //public override void OnActionExecuted(ActionExecutedContext filterContext)
        //{
           
        //}
    }
}