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
    /// 工艺管理
    /// </summary>
    public partial class StockManagementController
    {
        /// <summary>
        /// 返回视图
        /// </summary>
        /// <returns></returns>
        public ActionResult TraInStock()
        {
            return View();
        }
                      
        public ActionResult TraInStock_FindByPage(Mes_Tra_InStock obj, int page, int rows)
        {
            var pager = new PagerBase() { CurrentPageIndex = page, PageSize = rows };
            var list = MesTraInStockDao.Instance.FindByPage(obj, ref pager);
            return Json(new { total = pager.TotalItemCount, rows = list });
        }
    }
}
