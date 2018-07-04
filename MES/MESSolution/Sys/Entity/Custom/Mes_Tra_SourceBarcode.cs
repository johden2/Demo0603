using System;
using Dos.ORM;
using Dos.Common;
using System.Collections.Generic;
using Sys.Tools;

namespace Sys.Model
{
    public partial class Mes_Tra_SourceBarcode
    {
        public string CreatedTimeStart { get; set; }
        public string CreatedTimeEnd { get; set; }

        public string Show_CreatedTime
        {
            get
            {
                return TConvertHelper.FormatShortDateToString(this.CreatedTime);
            }
        }

    }

}