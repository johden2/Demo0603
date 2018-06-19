using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.EnterpriseServices;

using System.Web.Script.Serialization;
using Sys.Model;
using Sys.Dao;


namespace MES.ManagementApp.Controllers
{
    /// <summary>
    /// 系统配置信息
    /// </summary>
    public partial class SysConfigHelper
    {
        /// <summary>
        /// 模板文件放置的文件夹
        /// </summary>
        public const string TemplateFolder = "/Document/Template/";

        /// <summary>
        /// 临时文件夹路径
        /// </summary>
        public const string TempFolder = "/Document/Temp/";

        /// <summary>
        /// 实验室上传文件路径
        /// </summary>
        public const string UploadSampleFolder = "~/Document/SampleImg/";
        /// <summary>
        /// 实验室上传文件应用路径
        /// </summary>
        public const string AppUploadSampleFolder = "/Document/SampleImg/";




      

    }
}
