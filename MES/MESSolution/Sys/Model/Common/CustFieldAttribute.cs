using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sys.Model
{
    public class CustFieldAttribute : Attribute
    {
        private string _description = "";    ///字段描述
        private bool _ignore = false;                                   ///

        public CustFieldAttribute(bool ignore)
        {
            this._ignore = ignore;
        }

        public CustFieldAttribute(string description)
        {
            this._description = description;
        }

        public CustFieldAttribute(bool ignore, string description)
        {
            this._description = description;
            this._ignore = ignore;
        }

        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }

        public bool Ingore
        {
            get { return this._ignore; }
            set { this._ignore = value; }
        }

    }
}