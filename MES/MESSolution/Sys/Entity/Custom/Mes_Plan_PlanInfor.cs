using System;
using Dos.ORM;
using Dos.Common;
using System.Collections.Generic;
using Sys.Tools;

namespace Sys.Model
{
    public partial class Mes_Plan_PlanInfor
    {
        public string CreatedTimeStart { get; set; }
        public string CreatedTimeEnd { get; set; }

        public string WorkShopName { get; set; }

        public string Show_OrderType
        {
            get
            {
                return StatusHelper.GetConstStatus<OrderType>(this.OrderType);
            }
        }

        public string Show_PlanStatus
        {
            get
            {
                return StatusHelper.GetConstStatus<PlanStatus>(this.PlanStatus);
            }
        }

        public string Show_PlanType
        {
            get
            {
                return StatusHelper.GetConstStatus<PlanType>(this.PlanType);
            }
        }


       public string Show_CreatedTime
        {
            get
            {
                return TConvertHelper.FormatShortDateToString(this.CreatedTime);
            }
        }

       public string Show_BeginTime
       {
           get
           {
               return TConvertHelper.FormatSmallDate(this.BeginTime);
           }
       }

       public string Show_EndTime
       {
           get
           {
               return TConvertHelper.FormatSmallDate(this.EndTime);
           }
       }

    }

}