using System;
using Dos.ORM;
using Dos.Common;
using System.Collections.Generic;
using Sys.Tools;

namespace Sys.Model
{
    public partial class Mes_Tec_ProcessBom
    {
        public int ItemID { get; set; }

        public string SubMaterialProNo { get; set; }

        public string SubMaterialCode { get; set; }

        public int Num { get; set; }

        public string Unit { get; set; }

        public string ProcessCode { get; set; }

        public string ProcessName { get; set; }


    }

}