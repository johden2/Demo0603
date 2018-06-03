using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Sys.Tools
{
    public class Log4Helper
    {
        //log4net日志专用
        private static readonly log4net.ILog Instance = log4net.LogManager.GetLogger("loginfo");

        public static void SetConfig()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public static void SetConfig(string filePath)
        {
            FileInfo configFile = new FileInfo(filePath);
            log4net.Config.XmlConfigurator.Configure(configFile);
        }

        public static void SetConfig(FileInfo configFile)
        {
            log4net.Config.XmlConfigurator.Configure(configFile);
        }

        /// <summary>
        /// 记录普通文件记录
        /// </summary>
        /// <param name="info"></param>
        public static void Info(string info)
        {
            if (Instance.IsInfoEnabled)
            {
                Instance.Info(info);
            }
        }

        /// <summary>
        ///记录调试信息
        /// </summary>
        /// <param name="info"></param>
        /// <param name="se"></param>
        public static void Debug(string info)
        {
            if (Instance.IsErrorEnabled)
            {
                Instance.Debug(info);
            }
        }

        /// <summary>
        ///记录警告信息
        /// </summary>
        /// <param name="info"></param>
        /// <param name="se"></param>
        public static void Warn(string info)
        {
            if (Instance.IsWarnEnabled)
            {
                Instance.Warn(info);
            }
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="info"></param>
        /// <param name="se"></param>
        public static void Error(string info)
        {
            Error(info, new Exception(info));
        }

        public static void Error(string info, Exception se)
        {
            if (Instance.IsErrorEnabled)
            {
                Instance.Error(info, se);
            }
        }

        /// <summary>
        /// 记录严重错误
        /// </summary>
        /// <param name="info"></param>
        public static void Fatal(string info)
        {
            Fatal(info, new Exception(info));
        }

        /// <summary>
        /// 记录严重错误
        /// </summary>
        /// <param name="info"></param>
        /// <param name="se"></param>
        public static void Fatal(string info, Exception se)
        {
            if (Instance.IsFatalEnabled)
            {
                Instance.Fatal(info, se);
            }
        }
    }
}
