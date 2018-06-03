using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sys.Model
{
    /// <summary>
    /// 导入结果消息
    /// </summary>
    [Serializable]
    public class ImportMessageModel
    {
        public string RowData{ get; set; }

        public string RowMessage { get; set; }

    }
}
