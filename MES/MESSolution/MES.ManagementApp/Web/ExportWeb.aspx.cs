using MES.ManagementApp.Controllers;
using Sys.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MES.ManagementApp.Web
{
    public partial class ExportWeb : System.Web.UI.Page
    {        
        #region Begin 导出公共方法

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string businessType = Request.Params["BusinessType"];
                string keyValue = Request.Params["KeyValue"];
                string isExport = Request.Params["IsExport"];
                string fileName = Request.Params["FileName"];
                if (string.IsNullOrEmpty(businessType))
                {
                    Response.Clear();
                    Response.Write("导出失败，缺少业务类型参数！");
                    Response.End();
                    return;
                }
                //获取临时文件物理路径
                string tempPath = Server.MapPath("~" + SysConfigHelper.TempFolder);

                //LTSampleInfoExt obj = new LTSampleInfoExt();
                ////  var paramm = batch + "|" + sample + "|" + projectId + "|" + yearcom + "|";
                //obj.Batch = ConvertHelper.FormatDBString( Request.Params["Batch"]);
                //obj.SampleId =ConvertHelper.FormatDBString( Request.Params["SampleId"]);
                //obj.ProjectId =ConvertHelper.FormatDBInt( Request.Params["ProjectId"]);
                //obj.Year =Request.Params["Year"];

                ////
                //obj.userName = Request.Params["usrName"];

                //1.生成文件
                BussinessModel businessObj = new BussinessModel();
                businessObj.BusinessType = businessType;
                businessObj.TempPath = tempPath;
                //显示返回的消息
                string message = string.Empty;

                //switch (businessType)
                //{
                //    case "BatchSampleRpt": //批量导出样品信息
                //        message = this.GenBatchSampleReport(businessObj);
                //        break;
                //    case "SingleSampleRpt": //导出单个样品信息
                //        businessObj.KeyValue = keyValue;
                //        message = this.GenSingleSampleReport(businessObj);
                //        break;
                //    default:
                //        message = this.GenSingleSampleReport(businessObj);
                //        break;
                //}

                if (isExport == "1") //直接导出
                {
                    businessObj.FilePath = tempPath + fileName;
                }

                //2.输出文件
                System.IO.FileInfo file = new System.IO.FileInfo(businessObj.FilePath);
                Response.Clear();
                Response.Charset = "UTF-8";
                Response.ContentEncoding = System.Text.Encoding.Default;
                // 添加头信息，为"文件下载/另存为"对话框指定默认文件名 
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(file.Name));
                // 添加头信息，指定文件大小，让浏览器能够显示下载进度 
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/octet-stream";
                // 把文件流发送到客户端 
                Response.WriteFile(file.FullName);

                //MemoryStream stream = new MemoryStream(); // OutFileToStream(ModelDB.dbDT);
                //byte[] bytes = stream.ToArray();
                //Response.ContentType = "application/octet-stream";
                ////通知浏览器下载文件而不是打开 
                //Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode("23.xls", Encoding.UTF8));
                //Response.BinaryWrite(bytes);
            }

            Response.Flush();
            Response.End();
        }

        #endregion End 导出公共方法

    }
}