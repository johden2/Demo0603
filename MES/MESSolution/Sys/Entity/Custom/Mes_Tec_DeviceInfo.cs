using System;
using Dos.ORM;
using Dos.Common;
using System.Collections.Generic;
using Sys.Tools;

namespace Sys.Model
{
    public partial class Mes_Tec_DeviceInfo 
    {

        public string Show_CreatedTime
        {
            get
            {
                if (!this.CreatedTime.HasValue) return "";

                return TConvertHelper.FormatShortDateToString(this.CreatedTime.Value);
            }
        }

        public string Show_AcceptTime
        {
            get
            {
                if (!this.AcceptTime.HasValue) return "";

                return TConvertHelper.FormatShortDateToString(this.AcceptTime.Value);
            }
        }

        public string Show_ValidBeginTime
        {
            get
            {
                if (!this.ValidBeginTime.HasValue) return "";

                return TConvertHelper.FormatShortDateToString(this.ValidBeginTime.Value);
            }
        }

        public string Show_ValidEndTime
        {
            get
            {
                if (!this.ValidEndTime.HasValue) return "";

                return TConvertHelper.FormatShortDateToString(this.ValidEndTime.Value);
            }
        }

        public string Show_DeviceType
        {
            get;
            set;
        }
        
        public string Show_DeviceProperty
        {
            get
            {
                return StatusHelper.GetConstStatus<DeviceProperty>(this.DeviceProperty);
            }
        }

        public string Show_DeviceStatus
        {
            get
            {
                return StatusHelper.GetConstStatus<DeviceStatus>(this.DeviceStatus);
            }
        }
	}
}