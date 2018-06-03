using System;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Sys.Tools
{
    /// <summary>
    /// 正在验证
    /// </summary>
    public sealed class RegProvider
    {
        private RegProvider()
        {

        }

        /// <summary>
        /// 验证字符
        /// </summary>
        /// <param name="field"></param>
        /// <param name="sData"></param>
        /// <returns></returns>
        public static string IsEngName(string field,string sData)
        {
            var reg = new Regex(@"^[A-Za-z0-9_@]+$");
            if(!reg.IsMatch(sData)){
                return field + "只能包含数字、字母或下划线等";
            }

            return string.Empty;
        }
       
        /// <summary>
        /// 验证字符
        /// </summary>
        /// <param name="field"></param>
        /// <param name="sData"></param>
        /// <returns></returns>
        public static string IsCNName(string field, string sData)
        {
            var reg = new Regex(@"^[A-Za-z0-9_\u4E00-\u9FA5@]+$");
            if(!reg.IsMatch(sData)){
                return field + "只能包含中文、数字、字母或下划线等";
            }

            return string.Empty;
        }

        public static string IsPassword(string field, string sData)
        {
            var reg = new Regex(@"^[A-Za-z0-9_@]{6,20}$");
            if (!reg.IsMatch(sData))
            {
                return field + "只能包含6~20位的数字、字母或下划线等";
            }

            return string.Empty;
        }

    }
}
