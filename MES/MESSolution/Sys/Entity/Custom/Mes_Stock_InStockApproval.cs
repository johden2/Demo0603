using System;
using Dos.ORM;
using Dos.Common;
using System.Collections.Generic;
using Sys.Tools;

namespace Sys.Model
{
    public partial class Mes_Stock_InStockApproval
    {
        public string CreatedTimeStart { get; set; }
        public string CreatedTimeEnd { get; set; }

        public int InStockID { get; set; }

       public string Show_CreatedTime
        {
            get
            {
                return TConvertHelper.FormatShortDateToString(this.CreatedTime);
            }
        }

       public string Show_ApprovalStatus
       {
           get
           {
               return StatusHelper.GetConstStatus<AuditEnum>(this.ApprovalStatus);
           }
       }
    }

}