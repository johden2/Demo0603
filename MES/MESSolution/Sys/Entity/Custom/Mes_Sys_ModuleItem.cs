using System;
using Dos.ORM;
using Dos.Common;
using System.Collections.Generic;

namespace Sys.Model
{
    public partial class Mes_Sys_ModuleItem
    {
        public int ParentID { get; set; }

        public int IsParent { get; set; }

        public int ButtonRightFlag { get; set; }

    }

}