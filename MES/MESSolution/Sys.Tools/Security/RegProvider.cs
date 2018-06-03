using System;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Sys.Tools
{
    /// <summary>
    /// ������֤
    /// </summary>
    public sealed class RegProvider
    {
        private RegProvider()
        {

        }

        /// <summary>
        /// ��֤�ַ�
        /// </summary>
        /// <param name="field"></param>
        /// <param name="sData"></param>
        /// <returns></returns>
        public static string IsEngName(string field,string sData)
        {
            var reg = new Regex(@"^[A-Za-z0-9_@]+$");
            if(!reg.IsMatch(sData)){
                return field + "ֻ�ܰ������֡���ĸ���»��ߵ�";
            }

            return string.Empty;
        }
       
        /// <summary>
        /// ��֤�ַ�
        /// </summary>
        /// <param name="field"></param>
        /// <param name="sData"></param>
        /// <returns></returns>
        public static string IsCNName(string field, string sData)
        {
            var reg = new Regex(@"^[A-Za-z0-9_\u4E00-\u9FA5@]+$");
            if(!reg.IsMatch(sData)){
                return field + "ֻ�ܰ������ġ����֡���ĸ���»��ߵ�";
            }

            return string.Empty;
        }

        public static string IsPassword(string field, string sData)
        {
            var reg = new Regex(@"^[A-Za-z0-9_@]{6,20}$");
            if (!reg.IsMatch(sData))
            {
                return field + "ֻ�ܰ���6~20λ�����֡���ĸ���»��ߵ�";
            }

            return string.Empty;
        }

    }
}
