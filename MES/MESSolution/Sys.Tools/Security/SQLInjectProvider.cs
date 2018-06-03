using System;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Sys.Tools
{
    /// <summary>
    /// SQL 注入控制
    /// </summary>
    public sealed class SQLInjectProvider
    {
        private SQLInjectProvider()
        {

        }

        /// <summary>
        /// 剔除非法字符
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string FilterParameter(string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;

            string strOld = s;

            s = s.Trim().ToLower();
            //s = ClearScript(s);
            //s = s.Replace("=", ""); //Base64加密有些会带=号
            s = s.Replace("'", "");
            s = s.Replace(";", "");
            s = s.Replace(" or ", "");
            s = s.Replace("select", "");
            s = s.Replace("update", "");
            s = s.Replace("insert", "");
            s = s.Replace("delete", "");
            s = s.Replace("declare", "");
            s = s.Replace("exec", "");
            s = s.Replace("drop", "");
            s = s.Replace("create", "");
            s = s.Replace("%", "");
            s = s.Replace("--", "");

            //if (strOld.Length > s.Length)
            //{
            //    return "参数不允许包含有特殊字符！";
            //}

            return s;
        }

        /// <summary>
        /// 剔除非法字符
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string IsValidParameter(string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;

            string strOld = s;

            s = s.Trim().ToLower();
            //s = ClearScript(s);
            //s = s.Replace("=", "");  //base64位加密密码有=号，需要排除
            s = s.Replace("'", "");
            s = s.Replace(";", "");
            s = s.Replace(" or ", "");
            s = s.Replace("select", "");
            s = s.Replace("update", "");
            s = s.Replace("insert", "");
            s = s.Replace("delete", "");
            s = s.Replace("declare", "");
            s = s.Replace("exec", "");
            s = s.Replace("drop", "");
            s = s.Replace("create", "");
            s = s.Replace("%", "");
            s = s.Replace("--", "");

            if (strOld.Length > s.Length)
            {
                return "参数不允许包含有特殊字符(--,=,;,or,',or, %等)！";
            }

            return string.Empty;
        }

    }
}
