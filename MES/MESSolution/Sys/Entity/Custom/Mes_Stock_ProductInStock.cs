using System;
using Dos.ORM;
using Dos.Common;
using System.Collections.Generic;
using Sys.Tools;

namespace Sys.Model
{
    public partial class Mes_Stock_ProductInStock
    {
        public string InStockDateStart { get; set; }
        public string InStockDateEnd { get; set; }
        public string CreatedTimeStart { get; set; }
        public string CreatedTimeEnd { get; set; }

        public string Show_BillType
        {
            get
            {
                return StatusHelper.GetConstStatus<PStockBillType>(this.BillType);
            }
        }

        public string Show_AuditStatus
        {
            get
            {
                return StatusHelper.GetConstStatus<AuditEnum>(this.AuditStatus);
            }
        }

        public string Show_CreatedTime
        {
            get
            {
                return TConvertHelper.FormatShortDateToString(this.CreatedTime);
            }
        }

        public string Show_InStockDate
        {
            get
            {
                return TConvertHelper.FormatSmallDate(this.InStockDate);
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