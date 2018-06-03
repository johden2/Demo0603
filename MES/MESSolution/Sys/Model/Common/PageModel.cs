using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Sys.Model
{
    /// <summary>
    /// 页面操作模型
    /// </summary>
    public class PageModel
    {
        /// <summary>
        /// 操作类型：1-新增 2-修改 3-查看
        /// </summary>
        public int OptType { get; set; }

        /// <summary>
        /// 主键ID
        /// </summary>
        public string KeyID { get; set; }

    }
}