using System;
using Dos.ORM;
using Dos.Common;
using System.Collections.Generic;
using Sys.Tools;

namespace Sys.Model
{
    public partial class Mes_Stock_OutMaterial
    {
        public string OutStockDateStart { get; set; }
        public string OutStockDateEnd { get; set; }
        public string CreatedTimeStart { get; set; }
        public string CreatedTimeEnd { get; set; }

       public string Show_CreatedTime
        {
            get
            {
                return TConvertHelper.FormatShortDateToString(this.CreatedTime);
            }
        }

       public string Show_BillType
       {
           get
           {
               return StatusHelper.GetConstStatus<LLBillType>(this.BillType);
           }
       }
       public string Show_AuditStatus
       {
           get
           {
               return StatusHelper.GetConstStatus<AuditEnum>(this.AuditStatus);
           }
       }

       public string Show_OutStockDate
       {
           get
           {
               return TConvertHelper.FormatSmallDate(this.OutStockDate);
           }
       }

       public string Show_BillDate
       {
           get
           {
               return TConvertHelper.FormatSmallDate(this.BillDate);
           }
       }

    }

}