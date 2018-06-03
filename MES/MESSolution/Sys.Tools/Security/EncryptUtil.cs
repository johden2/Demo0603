using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Sys.Tools
{
    /// <summary>
    /// 定义的工具类
    /// </summary>
    public class EncryptUtil
    {
        public static DateTime SysMinDate = new DateTime(1900, 1, 1);

        //md5: e1adc3949ba59abbe56e057f2f883e
        private const string USER_PASSWORD = "123456";
        //对称加密秘钥
        private const string DES_KEY = "Rate_KEY";

        //字符串加密,账号关键参数
        private const string DB_USER_ID = "[USER_ID]";
        //字符串加密,密码关键参数
        private const string DB_USER_PASS = "[USER_PASS]";
        //字符串加密,账号、密码分隔符
        private const string DB_USER_Split = "@@@@";
        //特殊字符定义
        public const string SpecialChars = "~～!！@＠＃$＄%％^……&＆*＊,，、。?？;；:∶…¨‘’“”〃‘′〃↑↓←→↖↗↙↘㊣◎○●⊕⊙○●△▲☆★◇◆□■▽▼§￥〒￠￡※♀♂";

        #region Begin 数据校验

        /// <summary>
        /// 员工工号检测
        /// </summary>
        /// <param name="sEmployeeNo"></param>
        /// <returns></returns>
        public static string CheckEmployeeNo(string sEmployeeNo)
        {
            bool isSucc = new Regex(@"^\d{4,10}$").IsMatch(sEmployeeNo);
            if (!isSucc)
            {
                return "员工工号只能为数字(4~10位)！";
            }

            return null;
        }

        /// <summary>
        /// 密码检测
        /// </summary>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        public static string CheckPassword(string sPassword)
        {
            bool isSucc = new Regex(@"[a-zA-Z0-9_]{6,20}").IsMatch(sPassword);
            if (!isSucc)
            {
                return "密码只能为数字、字母或下划线(6~20位)！";
            }

            return null;
        }

        /// <summary>
        /// 校验录入的字符是否为特殊字符
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static bool isSpecialChar(char ch)
        {
            return (SpecialChars.IndexOf(ch) > -1);
        }


        #endregion End 数据校验


        #region Begin 数据库账号加密处理

        /// <summary>
        /// 解析数据库连接串
        /// </summary>
        /// <param name="sConnString"></param>
        /// <returns></returns>
        public static string DecodeDBConnString(string sConnString,string sAccount)
        {
            //解析加密的用户名、密码串(格式为: 账号@@@@密码 )
            List<string> list = SplitDBUser(sAccount);
            if (list == null || list.Count < 2)
            {
                throw new Exception("数据库连接串信息配置有误，请检查！");
            }

            //替换连接处中的变量
            return sConnString.Replace(DB_USER_ID, list[0].Trim()).Replace(DB_USER_PASS, list[1].Trim());
        }

        /// <summary>
        /// 解析加密的用户名、密码串
        /// </summary>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        public static List<string> SplitDBUser(string sAccount)
        {
            //先解密字符串
            string sResult = DesDecrypt(sAccount);
            if (sResult.IndexOf(DB_USER_Split) < 0) return null;

            return Regex.Split(sResult, DB_USER_Split, RegexOptions.IgnoreCase).ToList();
        }

        #endregion End 数据库账号加密处理

        #region Begin 密码处理

        /// <summary>
        /// 获取默认的用户密码
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultPassword()
        {
            return MD5Provider.HashString(USER_PASSWORD);
        }

       /// <summary>
       /// MD5加密传入的字符串
       /// </summary>
       /// <param name="sPassword"></param>
       /// <returns></returns>
        public static string MD5Password(string sPassword)
        {
            //return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile("123", "md5");

            return MD5Provider.HashString(sPassword);
        }


        #endregion End 密码处理


        #region Begin 对称加密

        /// <summary>
        /// 对称加密
        /// </summary>
        /// <param name="encryptString"></param>
        /// <returns></returns>
        public static string DesEncrypt(string encryptString)
        {
            return DESProvider.DesEncrypt(encryptString, DES_KEY);
        }

        /// <summary>
        /// 对称解密
        /// </summary>
        /// <param name="decryptString"></param>
        /// <returns></returns>
        public static string DesDecrypt(string decryptString)
        {
            return DESProvider.DesDecrypt(decryptString, DES_KEY);
        }

        #endregion End 对称加密



       



    }
}
