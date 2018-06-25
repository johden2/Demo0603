using System;
using Dos.ORM;
using Dos.Common;
using System.Collections.Generic;
using Sys.Tools;

namespace Sys.Model
{
    public partial class Mes_Tra_InStock
    {
        public string Show_ScanTime 
        {
            get
            {
                return TConvertHelper.FormatShortDateToString(this.ScanTime.Value);
            }
        }

        public string Show_OrgID
        {
            get;
            set;
        }
        #region  
        public string ScanTimeStart
        {
            get;
            set;
        }

        public string ScanTimeEnd
        {
            get;
            set;
        }
        #endregion

    }

}