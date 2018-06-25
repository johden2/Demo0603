using System;
using Dos.ORM;
using Dos.Common;
using System.Collections.Generic;
using Sys.Tools;

namespace Sys.Model
{
    public partial class Mes_Tec_DeviceOutRecord 
    {

        public string Show_CreatedTime
        {
            get
            {
                if (!this.CreatedTime.HasValue) return "";

                return TConvertHelper.FormatShortDateToString(this.CreatedTime.Value);
            }
        }

        public string Show_OutDate
        {
            get
            {
                if (!this.OutDate.HasValue) return "";

                return TConvertHelper.FormatShortDateToString(this.OutDate.Value);
            }
        }

        public DateTime OutDateStart
        { get; set; }

        public DateTime OutDateEnd
        {
            get;
            set;
        }

        public string DeviceName { get; set; }
        public string DeviceSize { get; set; }
        public string DeviceType { get; set; }

        public string Show_DeviceType { get; set; }

        public string Show_OrgID
        {
            get;
            set;
        }        
	}
}