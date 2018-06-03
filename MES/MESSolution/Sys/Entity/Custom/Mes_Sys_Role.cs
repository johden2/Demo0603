using System;
using Dos.ORM;
using Dos.Common;
using System.Collections.Generic;
using Sys.Tools;

namespace Sys.Model
{
    public partial class Mes_Sys_Role
    {
       public string Show_CreatedTime
        {
            get
            {
                return TConvertHelper.FormatShortDateToString(this.CreatedTime);
            }
        }

    }

}