using System;
using Dos.ORM;
using Dos.Common;
using System.Collections.Generic;
using Sys.Tools;

namespace Sys.Model
{
    public partial class Mes_Plan_SaleOrder
    {
        public string OrderDateStart { get; set; }
        public string OrderDateEnd { get; set; }
        public string CreatedTimeStart { get; set; }
        public string CreatedTimeEnd { get; set; }

        public string CustomerName { get; set; }

        public string Show_OrderType
        {
            get
            {
                return StatusHelper.GetConstStatus<OrderType>(this.OrderType);
            }
        }
        public string Show_OrderStatus
        {
            get
            {
                return StatusHelper.GetConstStatus<OrderStatus>(this.OrderStatus);
            }
        }

        public string Show_OrderDate
        {
            get
            {
                return TConvertHelper.FormatSmallDate(this.OrderDate);
            }
        }

       public string Show_CreatedTime
        {
            get
            {
                return TConvertHelper.FormatShortDateToString(this.CreatedTime);
            }
        }

       public string Show_CloseTime
       {
           get
           {
               return TConvertHelper.FormatShortDateToString(this.CloseTime);
           }
       }

    }

}