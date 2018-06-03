using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Sys.Tools
{
    /// <summary>
    /// 系统操作日志
    /// </summary>
    public  class LogHelper
    {
        public static string LogPath = System.Configuration.ConfigurationSettings.AppSettings["LogPath"];

       /// <summary>
       /// 写入日志
       /// </summary>
       /// <param name="sMessage"></param>
        public static void Log(string sMessage)
        {
            try
            {
                string sSystemLogPath = LogPath;
                if (string.IsNullOrEmpty(sSystemLogPath))
                {
                    sSystemLogPath = "c:\\Log\\";
                }
                string sDateTag = System.DateTime.Now.ToString("yyyyMM");
                //按天生成日志文件
                string sFileName = System.DateTime.Now.ToString("yyyyMMdd") + ".config";

                string dir = sSystemLogPath + sDateTag;
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                using (StreamWriter sw = new StreamWriter(Path.Combine(dir, sFileName), true))
                {
                    sw.WriteLine(System.DateTime.Now.ToString());
                    sw.WriteLine(sMessage);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="sMessage"></param>
        public static void Log(string sMessage,string logPath)
        {
            try
            {
                //按月生成目录
                string sSystemLogPath = logPath;
                string sDateTag = System.DateTime.Now.ToString("yyyyMM");
                //按天生成日志文件
                string sFileName = System.DateTime.Now.ToString("yyyyMMdd") + ".config";

                string dir = sSystemLogPath + sDateTag;
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                using (StreamWriter sw = new StreamWriter(Path.Combine(dir, sFileName), true))
                {
                    sw.WriteLine(System.DateTime.Now.ToString());
                    sw.WriteLine(sMessage);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch
            {

            }
        }


    }
}
