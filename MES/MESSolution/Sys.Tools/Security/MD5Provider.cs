using System;
using System.Text;
using System.Security.Cryptography;

namespace Sys.Tools
{
    /// <summary>
    /// 实现MD5散列（加密），算法的哈希值大小为 128 位。
    /// </summary>
    public sealed class MD5Provider
    {
        private static MD5 md5 = MD5.Create();
        private MD5Provider()
        {
        }
        /// <summary>
        /// 采用UTF8编码对字符串散列
        /// </summary>
        /// <param name="sourceString">要散列的字符串</param>
        /// <returns>散列后的字符串</returns>
        public static string HashString(string sourceString)
        {
            return HashString(Encoding.UTF8, sourceString);
        }
        /// <summary>
        /// 采用指定的编码对字符串散列
        /// </summary>
        /// <param name="encoding">Encoding的实例</param>
        /// <param name="sourceString">要散列的字符串</param>
        /// <returns>散列后的字符串</returns>
        public static string HashString(Encoding encoding, string sourceString)
        {
            if (string.IsNullOrEmpty(sourceString))
            {
                throw new ArgumentNullException("sourceString", "不能为空");
            }
            byte[] source = md5.ComputeHash(encoding.GetBytes(sourceString));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < source.Length; i++)
            {
                sBuilder.Append(source[i].ToString("x"));
            }
            return sBuilder.ToString();
        }
    }
}
