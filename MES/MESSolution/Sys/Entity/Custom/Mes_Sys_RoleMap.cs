using System;
using Dos.ORM;
using Dos.Common;
using System.Collections.Generic;
using Sys.Tools;

namespace Sys.Model
{
    public partial class Mes_Sys_RoleMap
    {
        public string ModuleCode { get; set; }

        public string MenuName { get; set; }

        public string RoleName { get; set; }

        public int ParentID { get; set; }

        public int  UseType { get; set; }

        public int Level { get; set; }

        public int IsParent { get; set; }

    }

}