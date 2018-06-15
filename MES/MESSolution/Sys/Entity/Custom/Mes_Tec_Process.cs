using System;
using Dos.ORM;
using Dos.Common;
using System.Collections.Generic;
using Sys.Tools;

namespace Sys.Model
{
    public partial class Mes_Tec_Process
    {
        public int IsCheck { get; set; }

        public string ParentName { get; set; }

        public string Show_State
        {
            get
            {
                return StatusHelper.GetConstStatus<ProcessState>(this.State);
            }
        }
        public string Show_IsRepairStatus
        {
            get
            {
                return StatusHelper.GetConstStatus<RepairStatus>(this.IsRepairStatus);
            }
        }

    }

}