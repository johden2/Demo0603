using System;
using Dos.ORM;
using Dos.Common;
using System.Collections.Generic;
using Sys.Tools;

namespace Sys.Model
{
    public partial class Mes_Stock_ProductInStockItem
    {
        public string Show_WorkOrderType
        {
            get
            {
                return StatusHelper.GetConstStatus<OrderType>(this.WorkOrderType);
            }
        }

       
       public string Show_CreatedTime
        {
            get
            {
                return TConvertHelper.FormatShortDateToString(this.CreatedTime);
            }
        }

    }

}