using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web;

namespace Sys.Tools
{
    public class CookiesHelper
    {
        ///// <summary>
        ///// 从cookie字符串中分解出相关信息，只能提取出基本信息
        ///// </summary>
        ///// <param name="cookies"></param>
        ///// <returns></returns>
        //public static UserInfo FetchUserInfoFromCookie(HttpContextBase httpContext)
        //{
        //    FormsAuthenticationTicket ticket = GetTicket(httpContext);
        //    if (ticket == null)
        //    {
        //        return null;
        //    }
        //    string cookies = ticket.UserData;
        //    if (string.IsNullOrEmpty(cookies) || cookies.Split(',').Length < 1)
        //    {
        //        return null;
        //    }
        //    UserInfo userInfo = new UserInfo();
        //    string[] cookieArray = cookies.Split(',');
        //    userInfo.EmployeeName = cookieArray[0];
        //    userInfo.UserID = cookieArray[0];
        //    userInfo.BU = cookieArray[1];
        //    userInfo.IP = cookieArray[2];
        //    userInfo.UserLevel = cookieArray[3];

        //    IList<UserRole> roles = new List<UserRole>();
        //    roles.Add(new UserRole(){ Name = cookieArray[3]});
        //    roles[0].Name = cookies.Split(',')[3];
        //    userInfo.Roles = roles;
        //    return userInfo;
        //}

        /// <summary>
        /// 仅从cookie字符串中分解出登录用户名信息，返回空则表示未解析到
        /// </summary>
        /// <param name="cookies">已解析好的明文cookies字符串</param>
        /// <returns>null/登录用户名</returns>
        public static string FetchUserNameFromCookie(HttpContextBase httpContext)
        {
            FormsAuthenticationTicket ticket = GetTicket(httpContext);
            if (ticket == null)
            {
                return string.Empty;
            }
            string cookies = ticket.UserData;
            if (string.IsNullOrEmpty(cookies) || cookies.Split(',').Length < 1)
            {
                return string.Empty;
            }
            return cookies.Split(',')[0];
        }

        /// <summary>
        /// 获取COOKIES凭证
        /// </summary>
        /// <returns>COOKIES凭证</returns>
        private static FormsAuthenticationTicket GetTicket(HttpContextBase httpContext)
        {
            FormsAuthenticationTicket ticket;
            String encryptedTicket = GetAuthCookieValue(httpContext);

            if (encryptedTicket == null) return null;
            if (encryptedTicket.Length == 0) return null;

            try
            {
                ticket = FormsAuthentication.Decrypt(encryptedTicket);
            }
            catch
            {
                return null;
            }
            return ticket;
        }

        /// <summary>
        /// 读取COOKIES字符串
        /// </summary>
        /// <returns>COOKIES字符串</returns>
        private static String GetAuthCookieValue(HttpContextBase httpContext)
        {
            String value = string.Empty;
            if ("Shockwave Flash" == HttpContext.Current.Request.UserAgent)
            {
                value = httpContext.Request.Form[FormsAuthentication.FormsCookieName];
            }

            if (string.IsNullOrEmpty(value))
            {
                HttpCookie cookie = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (cookie != null)
                {
                    return cookie.Value;
                }
            }
            return value;
        }

        /// <summary>
        /// 清理Cookie
        /// </summary>
        /// <param name="cookieKey"></param>
        public static void ClearCookie(string cookieKey)
        {
            if (HttpContext.Current.Request.Cookies[cookieKey] == null) return;

            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieKey];
            //使Cookie过期 (注意，读取的cookie中的domain为null，需要重新增加域才能过期原来的cookie)
            cookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }


    }
}
