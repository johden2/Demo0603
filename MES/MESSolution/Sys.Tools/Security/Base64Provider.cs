using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Sys.Tools
{
    /// <summary>
    /// Base64加密解密算法实现。
    /// </summary>
    public sealed class Base64Provider
    {
        private Base64Provider()
        {

        }

       #region Base64加密解密算法实现
          /// <summary>
          /// Base64加密
          /// </summary>
          /// <param name="Message"></param>
          /// <returns></returns>
          public static string Base64Code(string Message)
          {
              byte[] encData_byte = new byte[Message.Length];
             encData_byte = System.Text.Encoding.UTF8.GetBytes(Message);
             string encodedData = Convert.ToBase64String(encData_byte);
             return encodedData;
         }
         /// <summary>
         /// Base64解密
         /// </summary>
         /// <param name="Message"></param>
         /// <returns></returns>
          public static string Base64Decode(string Message)
         {
             try
             {
                 string result = DESNewProvider.DecryptDES(Message, "123456");//解密
                 return result;

                 //System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                 //System.Text.Decoder utf8Decode = encoder.GetDecoder();
                 //byte[] todecode_byte = Convert.FromBase64String(Message);
                 //int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                 //char[] decoded_char = new char[charCount];
                 //utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                 //string result = new String(decoded_char);
                 //return result;
             }
             catch (Exception e)
             {
                 throw new Exception("Error in base64Decode" + e.Message);
             }
         }
         #endregion
    }
}
