using System;
using Dos.ORM;
using Dos.Common;
using System.Collections.Generic;
using Sys.Tools;

namespace Sys.Model
{
    public partial class Mes_Tra_DiscardRecord
    {
        public string Show_CreatedTime 
        {
            get
            {
                return TConvertHelper.FormatShortDateToString(this.CreatedTime.Value);
            }
        }

        public string Show_OrgID
        {
            get;
            set;
        }
        #region  
        public string CreatedTimeStart
        {
            get;
            set;
        }

        public string CreatedTimeEnd
        {
            get;
            set;
        }
        #endregion

    }

}