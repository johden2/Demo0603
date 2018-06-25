using System;
using Dos.ORM;
using Dos.Common;
using System.Collections.Generic;
using Sys.Tools;

namespace Sys.Model
{
    public partial class Mes_Plan_WorkOrder
    {
        public string CreatedTimeStart { get; set; }
        public string CreatedTimeEnd { get; set; }

        public string WorkShopName { get; set; }

        public string Show_WorkOrderStatus
        {
            get
            {
                return StatusHelper.GetConstStatus<WorkOrderStatus>(this.WorkOrderStatus);
            }
        }

        public string Show_WorkOrderType
        {
            get
            {
                return StatusHelper.GetConstStatus<WorkOrderType>(this.WorkOrderType);
            }
        }
       public string Show_CreatedTime
        {
            get
            {
                return TConvertHelper.FormatShortDateToString(this.CreatedTime);
            }
        }

       public string Show_PlanBeginTime
       {
           get
           {
               return TConvertHelper.FormatSmallDate(this.PlanBeginTime);
           }
       }

       public string Show_PlanEndTime
       {
           get
           {
               return TConvertHelper.FormatSmallDate(this.PlanEndTime);
           }
       }

       public string Show_ActuralBeginTime
       {
           get
           {
               return TConvertHelper.FormatSmallDate(this.ActuralBeginTime);
           }
       }

       public string Show_ActuralEndTime
       {
           get
           {
               return TConvertHelper.FormatSmallDate(this.ActuralEndTime);
           }
       }

    }

}