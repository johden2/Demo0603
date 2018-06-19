using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.EnterpriseServices;
using System.Web.Script.Serialization;
using Sys.Model;
using Sys.Dao;
using CellDLL = Aspose.Cells;
using Sys.Tools;
using Aspose.Words;
using Aspose.Cells.Drawing;
using Sys.Model;
using System.Data;
using Aspose.Words.Drawing;
using System.Reflection;

namespace MES.ManagementApp.Controllers
{
    /// <summary>
    /// 系统导出功能
    /// </summary>
    public partial class SysExportHelper
    {
        #region Begin 通用导出

        /// <summary>
        /// 通用导出
        /// </summary>
        /// <param name="businessObj"></param>
        public static string Export<T>(ref BussinessModel businessObj, List<T> list) where T : class
        {
            string message = string.Empty;
            try
            {
                //1.生成文件(由模板生成新的文件)
                string templateName = "ExportTemplate.xls";
                string templatePath = HttpContext.Current.Server.MapPath("~" + SysConfigHelper.TemplateFolder) + templateName;
                string tempPath = HttpContext.Current.Server.MapPath("~" + SysConfigHelper.TempFolder) ;
                string fileName = string.Format("{0}_{1}.xls", businessObj.BusinessType, DateTime.Now.ToString("yyMMddHHmmssffff"));
                businessObj.FilePath = tempPath + fileName;
                businessObj.FileName = fileName;
                message = FileHelper.CopyFile(templatePath, tempPath.Trim('/'), fileName);
                if (!string.IsNullOrEmpty(message)) return message;

                //2.填写文件内容
                CellDLL.Workbook workbook = new CellDLL.Workbook(businessObj.FilePath);
                CellDLL.Cells cells = workbook.Worksheets[0].Cells;//单元格 
                int index = 0;
                int colNum = 0;
                object value = null;
                IDictionary<string,PropertyInfo> colProperties = new Dictionary<string,PropertyInfo>();
                PropertyInfo property = null;
                Type type = typeof(T);
                if (businessObj.ColList != null && businessObj.ColList.Count > 0)
                {
                    //(1)先写列头
                    businessObj.ColList.ForEach(item =>
                    {
                        cells[index, colNum++].Value = item.Value;
                        if (!colProperties.ContainsKey(item.Key))
                        {
                            property = type.GetProperty(item.Key);
                            colProperties.Add(item.Key, property);
                        }
                    });

                    //(2)写内容
                    if (list != null && list.Count > 0)
                    {
                        index = 1;
                        colNum = 0;
                        foreach (var item in list)
                        {
                            foreach (KeyModel keyObj in businessObj.ColList)
                            {
                                property = colProperties[keyObj.Key];
                                if (property != null)
                                {
                                    value = property.GetValue(item, null);
                                    cells[index, colNum].Value = TConvertHelper.FormatDBString(value);
                                }

                                colNum++;
                            }
                            colNum = 0;
                            index++;
                        }
                    }

                }/*businessObj.ColList*/

                //3.保存文件
                workbook.Save(businessObj.FilePath);
            }
            catch (Exception ex)
            {
                return "导出文件时出错,文件可能正被打开！";
            }
            finally
            {
                //xlApp.Quit();
                GC.Collect();//强行销毁  
            }

            return string.Empty;
        }
    
        #endregion

    }
}
