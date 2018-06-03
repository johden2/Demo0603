using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.EnterpriseServices;
using System.Web.Script.Serialization;
using Sys.Model;
using System.Text;

namespace MES.ManagementApp.Controllers
{
    /// <summary>
    /// 系统编号工具类
    /// </summary>
    public class SysNumberUtil
    {
        public static readonly SysNumberUtil Instance = new SysNumberUtil();
        private object lockobj = new object();

        /// <summary>
        /// 生成订单编号
        /// </summary>
        /// <returns></returns>
        public string GenOrderCode()
        {
            lock (lockobj)
            {
                return "O" + DateTime.Now.ToString("yyMMddHHmmssffff");
            }
        }

    }
}
