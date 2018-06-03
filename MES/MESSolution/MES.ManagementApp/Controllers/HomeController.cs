using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Sys.Model;
using Sys.Dao;
using System.Web.Script.Serialization;

namespace MES.ManagementApp.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View(base.CurUser);
        }

        public ActionResult Main()
        {
            return View();
        }

    }
}
