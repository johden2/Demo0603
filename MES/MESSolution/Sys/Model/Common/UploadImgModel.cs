using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Sys.Model
{
    /// <summary>
    /// 数据分析(采集)模块中的数据导入模型
    /// </summary>
    public class UploadImgModel
    {
        /// <summary>
        /// 业务类型
        /// </summary>
        public string BusinessType { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public string KeyValue { get; set; }
    }

    /// <summary>
    /// 上传结果模型
    /// </summary>
    public class UploadResultModel
    {
        /// <summary>
        /// 返回是否成功标识
        /// </summary>
        public bool IsSucc { get; set; }

        /// <summary>
        /// 上传的结果消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 旧文件名(即上传的文件原始名称)
        /// </summary>
        public string OldFileName { get; set; }

        /// <summary>
        /// 新文件名
        /// </summary>
        public string NewFileName { get; set; }

        /// <summary>
        /// 新文件的物理全路径
        /// </summary>
        public string NewFullPath { get; set; }
    }

}