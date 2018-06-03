using System;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Sys.Tools
{
    /// <summary>
    /// SQL ע�����
    /// </summary>
    public sealed class SQLInjectProvider
    {
        private SQLInjectProvider()
        {

        }

        /// <summary>
        /// �޳��Ƿ��ַ�
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string FilterParameter(string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;

            string strOld = s;

            s = s.Trim().ToLower();
            //s = ClearScript(s);
            //s = s.Replace("=", ""); //Base64������Щ���=��
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
            //    return "��������������������ַ���";
            //}

            return s;
        }

        /// <summary>
        /// �޳��Ƿ��ַ�
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string IsValidParameter(string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;

            string strOld = s;

            s = s.Trim().ToLower();
            //s = ClearScript(s);
            //s = s.Replace("=", "");  //base64λ����������=�ţ���Ҫ�ų�
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
                return "��������������������ַ�(--,=,;,or,',or, %��)��";
            }

            return string.Empty;
        }

    }
}
