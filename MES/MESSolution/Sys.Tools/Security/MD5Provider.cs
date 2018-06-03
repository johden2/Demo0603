using System;
using System.Text;
using System.Security.Cryptography;

namespace Sys.Tools
{
    /// <summary>
    /// ʵ��MD5ɢ�У����ܣ����㷨�Ĺ�ϣֵ��СΪ 128 λ��
    /// </summary>
    public sealed class MD5Provider
    {
        private static MD5 md5 = MD5.Create();
        private MD5Provider()
        {
        }
        /// <summary>
        /// ����UTF8������ַ���ɢ��
        /// </summary>
        /// <param name="sourceString">Ҫɢ�е��ַ���</param>
        /// <returns>ɢ�к���ַ���</returns>
        public static string HashString(string sourceString)
        {
            return HashString(Encoding.UTF8, sourceString);
        }
        /// <summary>
        /// ����ָ���ı�����ַ���ɢ��
        /// </summary>
        /// <param name="encoding">Encoding��ʵ��</param>
        /// <param name="sourceString">Ҫɢ�е��ַ���</param>
        /// <returns>ɢ�к���ַ���</returns>
        public static string HashString(Encoding encoding, string sourceString)
        {
            if (string.IsNullOrEmpty(sourceString))
            {
                throw new ArgumentNullException("sourceString", "����Ϊ��");
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
