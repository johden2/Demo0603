using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Sys.Model
{
    /// <summary>
    /// 业务模型
    /// </summary>
    public class BussinessModel
    {
        /// <summary>
        /// 业务类型
        /// </summary>
        public string BusinessType { get; set; }

        /// <summary>
        /// 关键参数值
        /// </summary>
        public string KeyValue { get; set; }

        /// <summary>
        /// 临时文件路径
        /// </summary>
        public string TempPath { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }

        public string FileName { get; set; }

        /// <summary>
        /// 用于导出时控制需要显示的列
        /// </summary>
        public List<string> ColList { get; set; }

    }
}