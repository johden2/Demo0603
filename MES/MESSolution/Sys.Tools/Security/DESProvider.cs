using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Sys.Tools
{
    /// <summary>
    /// DESEncrypt加密解密算法。
    /// </summary>
    public sealed class DESProvider
    {
        private DESProvider()
        {

        }

        /// <summary>
        /// 使用指定的key对字符串进行DES加密
        /// </summary>
        /// <param name="encryptString">要加密的字符串</param>
        /// <param name="key">DES算法加密解密时使用到的秘钥</param>
        /// <returns>加密后的字符串</returns>
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
        /// 使用指定的key对字符串进行DES解密
        /// </summary>
        /// <param name="decryptString">要解密的字符串</param>
        /// <param name="key">DES算法加密解密时使用到的秘钥</param>
        /// <returns>解密后的字符串</returns>
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
