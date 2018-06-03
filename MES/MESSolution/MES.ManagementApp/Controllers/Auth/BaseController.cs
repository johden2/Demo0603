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
    [AuthorizeFilter]
    public class BaseController : Controller
    {
        public const string UserKey = "UserInfo";
        public UserModel _CurUser = null;

        protected UserModel CurUser
        {
            get {
                if (_CurUser == null)
                {
                    _CurUser = SessionManager.Instance.GetSession<UserModel>(UserKey);
                    if (_CurUser == null)
                    {
                        //Redirect("/Login/Index");
                        Response.Write("<script>window.top.location.href = '/Login/Index'</script>");
                        //throw new Exception("用户非法请求");
                        return new UserModel();//避免使用的报错
                    }
                }

                return _CurUser;
            }
            set
            {
                _CurUser = value;
            }
        }
        

    }
}
