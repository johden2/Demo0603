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
    public class KeyModel
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public KeyModel() { }

        public KeyModel(string sValue, string sKey)
        {
            this.Value = sValue;
            this.Key = sKey;
        }


    }
}