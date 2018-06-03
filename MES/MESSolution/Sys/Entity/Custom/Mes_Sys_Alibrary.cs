using System;
using Dos.ORM;
using Dos.Common;
using System.Collections.Generic;
using Sys.Tools;

namespace Sys.Model
{
    public partial class Mes_Sys_Alibrary 
    {

        public string Show_CreatedTime
        {
            get
            {                
                return TConvertHelper.FormatShortDateToString(this.CreatedTime);
            }
        }

        public string Show_ModifiedTime
        {
            get
            {
                if (!this.ModifiedTime.HasValue) return "";

                return TConvertHelper.FormatShortDateToString(this.ModifiedTime.Value);
            }
        }

        /// <summary>
        /// 所属仓库名称
        /// </summary>
        public string Show_StockCode
        {
            get;
            set;
        }
	}
}