using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sys.Model
{
    public class FormatCommon
    {
        /// <summary>
        /// 格式化日期
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string FormatDateTime(DateTime time)
        {
            //return string.Format("{0:yyyy-MM-dd HH:mm:ss}", time);
            return string.Format("{0:yyyy-MM-dd HH:mm}", time);
        }

        public static string FormatDateTime(DateTime? time)
        {
            if (!time.HasValue) return string.Empty;

            //return string.Format("{0:yyyy-MM-dd HH:mm:ss}", time.Value);
            return string.Format("{0:yyyy-MM-dd HH:mm}", time.Value);
        }

        public static string FormatDate(DateTime? time)
        {
            if (!time.HasValue) return string.Empty;

            return string.Format("{0:yyyy-MM-dd}", time);
        }

        public static string FormatDate(DateTime time)
        {
            return string.Format("{0:yyyy-MM-dd}", time);
        }

        public static int FormatInt(object obj,int defaultValue = -1)
        {
            if (obj == null || obj == DBNull.Value||obj.ToString().Length==0) return defaultValue;

            return Convert.ToInt32(obj);
        }

        public static string FormatString(object obj)
        {
            if (obj == null || obj == DBNull.Value) return string.Empty;

            return obj.ToString();
        }

    }


}
