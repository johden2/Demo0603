using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Sys.Model
{
    /// <summary>
    /// Key, Value键值对
    /// </summary>
    public class ItemModel
    {
        public string Value { get; set; }

        //文本
        public string Text { get; set; }


        public ItemModel() { }

        public ItemModel(string sValue, string sText)
        {
            this.Value = sValue;
            this.Text = sText;
        }


    }
}