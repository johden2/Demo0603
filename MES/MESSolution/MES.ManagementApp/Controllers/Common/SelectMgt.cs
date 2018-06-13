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
    /// 选择控件
    /// </summary>
    public partial class CommonController
    {
        public ActionResult SelectSupplier()
        {
            return View();
        }

        public ActionResult SelectCustomer()
        {
            return View();
        }

        public ActionResult SelectProduct()
        {
            return View();
        }

        public ActionResult SelectWorkShop()
        {
            return View();
        }

        public ActionResult SelectStock()
        {
            return View();
        }

        public ActionResult SelectSaleOrder()
        {
            return View();
        }

        public ActionResult SelectPlan()
        {
            return View();
        }
  
    }
}
