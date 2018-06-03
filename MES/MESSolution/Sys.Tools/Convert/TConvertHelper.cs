using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Text.RegularExpressions;


namespace Sys.Tools
{
    public  class TConvertHelper
    {

        #region 定义常量

        public static DateTime SYS_MINDATE = new DateTime(1900, 1, 1);


        #endregion 定义常量

        #region 公共方法


        public static string FormatDBString(object data)
        {
            if (data != null && data != DBNull.Value && !string.IsNullOrEmpty(data.ToString()))
                return data.ToString().Trim();

            return "";
        }

        /// <summary>
        /// 验证是否是整数,包含0在内也算整数
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static bool IsNumber(object data)
        {
            if (data != null && data != DBNull.Value && !string.IsNullOrEmpty(data.ToString()))
            {
                Match match = (new Regex(@"^[0-9]\d*$")).Match(data.ToString());
                return match.Success;
            }
            return false;
        }

        /// <summary>
        /// 验证是否是日期
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        static public bool IsDateTime(object data)
        {
            if (data != null && data != DBNull.Value && !string.IsNullOrEmpty(data.ToString()))
            {
                Match match = (new Regex(@"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$")).Match(data.ToString().Replace('/','-'));
                return match.Success;
            }
            return false;
        }

        public static int FormatDBInt(object data)
        {
            if (data != null && data != DBNull.Value && !string.IsNullOrEmpty(data.ToString()))
                return Convert.ToInt32(data);

            return 0;
        }

        public static int? FormatDBNullInt(object data)
        {
            if (data != null && data != DBNull.Value && !string.IsNullOrEmpty(data.ToString()))
                return Convert.ToInt32(data);

            return null;
        }

        public static decimal FormatDBDeciaml(object data)
        {
            if (data != null && data != DBNull.Value && !string.IsNullOrEmpty(data.ToString()))
                return Convert.ToDecimal(data);

            return 0;
        }

        public static decimal? FormatDBNullDeciaml(object data)
        {
            if (data != null && data != DBNull.Value && !string.IsNullOrEmpty(data.ToString()))
                return Convert.ToDecimal(data);

            return null;
        }

        public static string FormatDBNullDecimal(object data, int num,int bsNum)
        {
            if (data != null && data != DBNull.Value && !string.IsNullOrEmpty(data.ToString()))
                return Math.Round(decimal.Parse(data.ToString()) * bsNum, num).ToString();

            return null;
        }

        /// <summary>
        /// 格式化double
        /// </summary>
        /// <param name="data"></param>
        /// <param name="num">小数位数</param>
        /// <returns></returns>
        public static double FormatDoubleNum(object data,int num)
        {
            if (data != null && data != DBNull.Value && !string.IsNullOrEmpty(data.ToString()))
                return Math.Round(double.Parse(data.ToString()), num);

            return 0;
        }

        public static double FormatDouble(object data)
        {
            return FormatDoubleNum(data, 0);
        }

        public static float FormatFloatNum(object data, int num)
        {
            if (data != null && data != DBNull.Value && !string.IsNullOrEmpty(data.ToString()))
                return (float)Math.Round(decimal.Parse(data.ToString()), num);

            return 0;
        }

        public static float? FormatFloatNullNum(object data, int num)
        {
            if (data == null || data == DBNull.Value || string.IsNullOrEmpty(data.ToString())) return null;

           return (float)Math.Round(decimal.Parse(data.ToString()), num);
        }
        /// <summary>
        /// 判断是否为合法的值
        /// </summary>
        /// <returns></returns>
        public static bool IsValidValue(decimal? data)
        {
            if (!data.HasValue || data.Value == 0) return false;

            return true;
        }

        public static string FormatFloatNullString(object data, int num)
        {
            if (data == null || data == DBNull.Value || string.IsNullOrEmpty(data.ToString())) return "";

            if (num > 0)
            {
                string format = "#0.";
                for (var i = 0; i < num;i++ ) format += "0";
                return Math.Round(decimal.Parse(data.ToString()), num).ToString(format);
            }

            return Math.Round(decimal.Parse(data.ToString()), num).ToString() ;
        }

        public static long FormatDBBigInt(object data)
        {
            if (data != null && data != DBNull.Value && !string.IsNullOrEmpty(data.ToString()))
                return Convert.ToInt64(data);

            return 0;
        }

        public static bool FormatDBBool(object data)
        {
            if (data != null && data != DBNull.Value && !string.IsNullOrEmpty(data.ToString()))
                return Convert.ToBoolean(data.ToString());

            return false;
        }

        public static DateTime FormatDBDate(object data)
        {
            try
            {
                if (data != null && data != DBNull.Value && !string.IsNullOrEmpty(data.ToString()))
                    return Convert.ToDateTime(data);
            }
            catch
            {
            }

            return DateTime.MinValue;
        }

        public static string FormatSmallDate(DateTime? time)
        {
            if (!time.HasValue) return string.Empty;

            return time.Value.ToString("yyyy-MM-dd");
        }

        public static string FormatSmallDate(DateTime time)
        {
            try
            {
                return time.ToString("yyyy-MM-dd");
            }
            catch
            {
            }

            return string.Empty;
        }

        public static string FormatShortDateToString(DateTime time)
        {
            return time.ToString("yyyy-MM-dd HH:mm");
        }

        public static string FormatShortDateToString(DateTime? time)
        {
            if (!time.HasValue) return string.Empty;

            return time.Value.ToString("yyyy-MM-dd HH:mm");
        }

        public static string FormatDateToString(DateTime time)
        {
            try
            {
                return time.ToString("yyyy-MM-dd HH:mm:ss"); 
            }
            catch
            {
            }

            return string.Empty;
        }

        public static string FormatDateToString(DateTime? time)
        {
            try
            {
                if (time == null || !time.HasValue) return string.Empty;

                return time.Value.ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch
            {
            }

            return string.Empty;
        }

        /// <summary>
        /// 格式化显示小时
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string FormatDateHour(DateTime time)
        {
            return time.ToString("yyyy-MM-dd HH");
        }

        public static string FormatHour(DateTime time)
        {
            return time.ToString("HH:00");
        }

        public static string FormatHourNum(DateTime time)
        {
            return time.ToString("HH");
        }

        public static DateTime? FormatDBDateTime(object data)
        {
            try
            {
                if (data != null && data != DBNull.Value && !string.IsNullOrEmpty(data.ToString()))
                    return Convert.ToDateTime(data);
            }
            catch
            {
            }

            return null;
        }

        public static DateTime? FormatDateTime(string data)
        {
            try
            {
                if (string.IsNullOrEmpty(data)) return null;

                return Convert.ToDateTime(data);
            }
            catch
            {
            }

            return null;
        }

        public static bool ValidateDate(string data)
        {
            try
            {
                if (string.IsNullOrEmpty(data)) return false;

                  DateTime time =  Convert.ToDateTime(data);
                  return true;
            }
            catch
            {
                return false;
            }
        }


        #endregion 公共方法


    }
}
