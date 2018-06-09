using System;
using Dos.ORM;
using Dos.Common;
using System.Collections.Generic;
using Sys.Tools;

namespace Sys.Model
{
    public partial class Mes_Stock_InStockItem
    {
        public string StockName { get; set; }

        public string Show_AcceptDate
        {
            get
            {
                return TConvertHelper.FormatSmallDate(this.AcceptDate);
            }
        }

        public string Show_CheckItemStatus
        {
            get
            {
                return StatusHelper.GetConstStatus<CheckStatus>(this.CheckItemStatus);
            }
        }

    }

}