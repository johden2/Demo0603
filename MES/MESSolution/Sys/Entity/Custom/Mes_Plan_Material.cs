using System;
using Dos.ORM;
using Dos.Common;
using System.Collections.Generic;
using Sys.Tools;

namespace Sys.Model
{
    public partial class Mes_Plan_Material
    {
        public string Show_CreatedTime
        {
            get
            {
                return TConvertHelper.FormatShortDateToString(this.CreatedTime);
            }
        }

        public string Show_ModifiedTime
        {
            get
            {
                if (!this.ModifiedTime.HasValue) return "";

                return TConvertHelper.FormatShortDateToString(this.ModifiedTime.Value);
            }
        }

        public string Show_ActionStatus
        {
            get
            {
                return StatusHelper.GetConstStatus<ActionStatus>(this.ActionStatus);
            }
        }

        public string Show_ProcessCode
        {
            get;
            set;
        }
    }

}