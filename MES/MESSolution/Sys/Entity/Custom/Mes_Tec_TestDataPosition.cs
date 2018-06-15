using System;
using Dos.ORM;
using Dos.Common;
using System.Collections.Generic;
using Sys.Tools;

namespace Sys.Model
{
    public partial class Mes_Tec_TestDataPosition 
    {
        public string ProcessName { get; set; }

        public string Show_CreatedTime
        {
            get
            {
                if (!this.CreatedTime.HasValue) return "";

                return TConvertHelper.FormatShortDateToString(this.CreatedTime.Value);
            }
        }
        public string Show_TestProperty
       {
           get
           {
               return StatusHelper.GetConstStatus<TestPropertyStatus>(this.TestProperty);
           }
       }

        //public string Show_ModifiedTime
        //{
        //    get
        //    {
        //        if (!this.ModifiedTime.HasValue) return "";

        //        return TConvertHelper.FormatShortDateToString(this.ModifiedTime.Value);
        //    }
        //}		
	}
}