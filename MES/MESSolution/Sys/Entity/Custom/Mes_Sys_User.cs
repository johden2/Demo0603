using System;
using Dos.ORM;
using Dos.Common;
using System.Collections.Generic;
using Sys.Tools;

namespace Sys.Model
{
    public partial class Mes_Sys_User
    {
        public string RoleID { get; set; }
        public string RoleName { get; set; }

        public string OrgName { get; set; }

       public string Show_CreatedTime
        {
            get
            {
                if (!this.CreatedTime.HasValue) return "";

                return TConvertHelper.FormatShortDateToString(this.CreatedTime.Value);
            }
        }

       public string Show_IsAdmin
       {

           get
           {
               return StatusHelper.GetConstStatus<YesNoEnum>(this.IsAdmin);
           }
       }
      

    }

}