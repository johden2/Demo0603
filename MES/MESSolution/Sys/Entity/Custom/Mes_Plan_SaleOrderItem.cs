using System;
using Dos.ORM;
using Dos.Common;
using System.Collections.Generic;
using Sys.Tools;

namespace Sys.Model
{
    public partial class Mes_Plan_SaleOrderItem
    {
        public int OrderID { get; set; }

        public string Show_OrderType
        {
            get
            {
                return StatusHelper.GetConstStatus<OrderType>(this.OrderType);
            }
        }

        public string Show_ShipDate
        {
            get
            {
                if (!this.ShipDate.HasValue) return string.Empty;
                return TConvertHelper.FormatSmallDate(this.ShipDate.Value);
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