using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Sys.Tools
{
    /// <summary>
    /// DESEncrypt���ܽ����㷨��
    /// </summary>
    public sealed class DESProvider
    {
        private DESProvider()
        {

        }

        /// <summary>
        /// ʹ��ָ����key���ַ�������DES����
        /// </summary>
        /// <param name="encryptString">Ҫ���ܵ��ַ���</param>
        /// <param name="key">DES�㷨���ܽ���ʱʹ�õ�����Կ</param>
        /// <returns>���ܺ���ַ���</returns>
        public static string DesEncrypt(string encryptString, string key)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key.Substring(0, 8));
            byte[] keyIV = keyBytes;
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, provider.CreateEncryptor(keyBytes, keyIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }
        /// <summary>
        /// ʹ��ָ����key���ַ�������DES����
        /// </summary>
        /// <param name="decryptString">Ҫ���ܵ��ַ���</param>
        /// <param name="key">DES�㷨���ܽ���ʱʹ�õ�����Կ</param>
        /// <returns>���ܺ���ַ���</returns>
        public static string DesDecrypt(string decryptString, string key)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key.Substring(0, 8));
            byte[] keyIV = keyBytes;
            byte[] inputByteArray = Convert.FromBase64String(decryptString);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, provider.CreateDecryptor(keyBytes, keyIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Encoding.UTF8.GetString(mStream.ToArray());
        }
    }
}
