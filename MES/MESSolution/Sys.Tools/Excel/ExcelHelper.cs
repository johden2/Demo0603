using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CellDLL = Aspose.Cells;
using System.Data;
using System.IO;
using Aspose.Cells;


namespace Sys.Tools
{
    public class ExcelHelper
    {
        public static DataTable ReadGatherData(string strFileName, ref string message)
        {
            int headerIndex = 1;
            int paramColIndex = 6;
            int dataIndex = 5;

            DataTable dt = new DataTable();

            try
            {
                CellDLL.Workbook workbook = new CellDLL.Workbook(strFileName); 
                CellDLL.Cells cellList = null;
                CellDLL.Worksheet sheet = workbook.Worksheets[0];
                
                cellList = sheet.Cells;

                int startIndex = cellList.MinColumn;
                int lastIndex = cellList.MaxColumn;

                //获取列头
                for (int i = startIndex; i <= lastIndex; i++)
                {
                    if (i < paramColIndex)
                    {
                        if (cellList[headerIndex, i].Value == null) continue;

                        dt.Columns.Add(cellList[headerIndex, i].StringValue);
                    }else
                    {
                        if (cellList[headerIndex + 2, i].Value == null)
                        {
                            lastIndex = i - 1;
                            break;
                        }
                        dt.Columns.Add(cellList[headerIndex + 2, i].StringValue);
                    }                   
                }

                //获取数据
                DataRow dataRow = null;
                for (int i = dataIndex; i <= cellList.MaxDataRow; i++)
                {
                    if (cellList[i, startIndex] == null || cellList[i, startIndex].Value == DBNull.Value) break;

                    dataRow = dt.NewRow();
                    for (int j = startIndex; j <= lastIndex; j++)
                    {
                        if (cellList[i, j] == null) continue;

                        dataRow[j] = cellList[i, j].StringValue;
                    }

                    dt.Rows.Add(dataRow);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }

        public static DataTable ReadData(string strFileName,  ref string message)
        {
            DataTable dt = new DataTable();


            try
            {

                // CellDLL.Workbook workbook = new CellDLL.Workbook(strFileName, new CellDLL.LoadOptions(CellDLL.LoadFormat.Excel97To2003)); //(strFileName, FileFormatType.Default);
                CellDLL.Workbook workbook = new CellDLL.Workbook(strFileName);
                CellDLL.Cells cellList = null;
                CellDLL.Worksheet sheet = workbook.Worksheets[0];

                cellList = sheet.Cells;

                int startIndex = cellList.MinColumn;
                int lastIndex = cellList.MaxColumn;

                //获取列头
                for (int i = startIndex; i <= lastIndex; i++)
                {
                    if (cellList[0, i] == null) continue;

                    dt.Columns.Add(cellList[0, i].StringValue);
                }

                //获取数据
                DataRow dataRow = null;
                for (int i = cellList.MinDataRow + 1; i <= cellList.MaxDataRow; i++)
                {
                    dataRow = dt.NewRow();
                    for (int j = startIndex; j <= lastIndex; j++)
                    {
                        if (cellList[i, j] == null) continue;

                        dataRow[j] = cellList[i, j].StringValue;
                    }

                    dt.Rows.Add(dataRow);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }


        public static void GenFile(string strFileName)
        {
            try
            {
                //CellDLL.Workbook workbook = new CellDLL.Workbook(strFileName);
                int index = strFileName.LastIndexOf("\\");
                string sFilePath = strFileName.Substring(0,index + 1);
                string newFileName = strFileName.Substring(index + 1);
                index = newFileName.LastIndexOf(".");
                string sTempName = newFileName.Substring(0,index);
                newFileName = sFilePath + sTempName + "_New" + newFileName.Substring(index);
                CellDLL.Workbook workbook = new CellDLL.Workbook(newFileName);
                workbook.Save(newFileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public static string GetRandomFileName(string ext)
        {
            Random r = new Random();
            return GetDateTimeStr() + r.Next(10000, 99999).ToString() + "." + ext;
        }
        public static string GetDateTimeStr()
        {
            return GetDateTimeStr(DateTime.Now);
        }
        public static string GetDateTimeStr(DateTime t)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(t.Year.ToString());
            sb.Append(t.Month.ToString().PadLeft(2, '0'));
            sb.Append(t.Day.ToString().PadLeft(2, '0'));
            sb.Append(t.Hour.ToString().PadLeft(2, '0'));
            sb.Append(t.Minute.ToString().PadLeft(2, '0'));
            sb.Append(t.Second.ToString().PadLeft(2, '0'));
            return sb.ToString();
        }

 
        /// <summary> 
        /// 导出数据到本地 
        /// </summary> 
        /// <param name="dt">要导出的数据</param> 
        /// <param name="tableName">表格标题</param> 
        /// <param name="path">保存路径</param> 
       
        public static void OutFileToDisk(DataTable dt, string modelFilePath, string exportpath,string[] Cols)
        {

            string OutFilePath = exportpath + "admin" + "_" + GetRandomFileName("xls");
            Workbook workbook = new Workbook(); //工作簿 
            Worksheet sheet = workbook.Worksheets[0]; //工作表 
            Cells cells = sheet.Cells;//单元格 

            //为标题设置样式     
            Style styleTitle = workbook.Styles[workbook.Styles.Add()];//新增样式 
            styleTitle.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
            styleTitle.Font.Name = "宋体";//文字字体 
            styleTitle.Font.Size = 18;//文字大小 
            styleTitle.Font.IsBold = true;//粗体 

            //样式2 
            Style style2 = workbook.Styles[workbook.Styles.Add()];//新增样式 
            style2.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
            style2.Font.Name = "宋体";//文字字体 
            style2.Font.Size = 14;//文字大小 
            style2.Font.IsBold = true;//粗体 
            style2.IsTextWrapped = true;//单元格内容自动换行 
            style2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            style2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

            //样式3 
            Style style3 = workbook.Styles[workbook.Styles.Add()];//新增样式 
            style3.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
            style3.Font.Name = "宋体";//文字字体 
            style3.Font.Size = 12;//文字大小 
            style3.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style3.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            style3.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style3.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

            int Colnum = dt.Columns.Count;//表格列数 
            int Rownum = dt.Rows.Count;//表格行数 

            //生成行1 标题行    
        //    cells.Merge(0, 0, 1, Colnum);//合并单元格 
          //  cells[0, 0].PutValue(tableName);//填写内容 
            
            cells[0, 0].SetStyle(styleTitle);
            cells.SetRowHeight(0, 38);
             
            //生成行2 列名行 
            for (int i = 0; i < Cols.Length; i++)
            {
                cells[0, i].PutValue(Cols[i]);
                cells[0, i].SetStyle(style3);
                cells.SetRowHeight(0, 25);
            }

            //生成数据行 
            for (int i = 0; i < Rownum; i++)
            {
                for (int k = 0; k < Colnum; k++)
                {
                    cells[1 + i, k].PutValue(dt.Rows[i][k].ToString());
                    cells[1 + i, k].SetStyle(style3);
                }
                cells.SetRowHeight(1 + i, 24);
               
            }
            workbook.Save(OutFilePath);
            System.Web.HttpContext curContext = System.Web.HttpContext.Current;
            System.IO.FileInfo file = new System.IO.FileInfo(OutFilePath);
            curContext.Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            curContext.Response.ContentType = "application/ms-excel";// 指定返回的是一个不能被客户端读取的流，必须被下载
            curContext.Response.WriteFile(OutFilePath); // 把文件流发送到客户端
            curContext.Response.End();


        }


        public static void OutFileToDisk2(DataTable dt, string modelFilePath, string exportpath, string[] Cols)
        {

            string OutFilePath = exportpath + "admin" + "_" + GetRandomFileName("xls");
            Workbook workbook = new Workbook(); //工作簿 
            Worksheet sheet = workbook.Worksheets[0]; //工作表 
            Cells cells = sheet.Cells;//单元格 

            //为标题设置样式     
            Style styleTitle = workbook.Styles[workbook.Styles.Add()];//新增样式 
            styleTitle.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
            styleTitle.Font.Name = "宋体";//文字字体 
            styleTitle.Font.Size = 18;//文字大小 
            styleTitle.Font.IsBold = true;//粗体 

            //样式2 
            Style style2 = workbook.Styles[workbook.Styles.Add()];//新增样式 
            style2.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
            style2.Font.Name = "宋体";//文字字体 
            style2.Font.Size = 14;//文字大小 
            style2.Font.IsBold = true;//粗体 
            style2.IsTextWrapped = true;//单元格内容自动换行 
            style2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            style2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

            //样式3 
            Style style3 = workbook.Styles[workbook.Styles.Add()];//新增样式 
            style3.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
            style3.Font.Name = "宋体";//文字字体 
            style3.Font.Size = 12;//文字大小 
            style3.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style3.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            style3.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style3.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

            int Colnum = dt.Columns.Count;//表格列数 
            int Rownum = dt.Rows.Count;//表格行数 

            //生成行1 标题行    
            //    cells.Merge(0, 0, 1, Colnum);//合并单元格 
            //  cells[0, 0].PutValue(tableName);//填写内容 

            cells[0, 0].SetStyle(styleTitle);
            cells.SetRowHeight(0, 38);

           // cells.Merge(1, 2, 1,1);
            cells[0, 0].PutValue("城市名称");
            cells[0, 0].SetStyle(style3);
            cells.Merge(0, 0, 2, 1);
         //   cells.Merge(1, 2, 3, 3);
            cells[0, 1].PutValue("点位名称");
            cells[0, 1].SetStyle(style3);
            cells.Merge(0, 1, 2, 1);
            cells[0, 2].PutValue("PM2.5");
            cells[0, 2].SetStyle(style3);
            cells.Merge(0, 2, 1, 5);
            cells[0, 7].PutValue("PM10");
            cells.Merge(0, 7, 1, 5);
            cells[0, 7].SetStyle(style3);
            //生成行2 列名行 
            //for (int i = 0; i < Cols.Length; i++)
            //{
            //    cells[0, i].PutValue(Cols[i]);
            //    cells[0, i].SetStyle(style3);
            //    cells.SetRowHeight(0, 25);
            //}

            //生成数据行 
            //for (int i = 0; i < Rownum; i++)
            //{
            //    for (int k = 0; k < Colnum; k++)
            //    {
            //        cells[1 + i, k].PutValue(dt.Rows[i][k].ToString());
            //        cells[1 + i, k].SetStyle(style3);
            //    }
            //    cells.SetRowHeight(1 + i, 24);

            //}
            workbook.Save(OutFilePath);
            System.Web.HttpContext curContext = System.Web.HttpContext.Current;
            System.IO.FileInfo file = new System.IO.FileInfo(OutFilePath);
            curContext.Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            curContext.Response.ContentType = "application/ms-excel";// 指定返回的是一个不能被客户端读取的流，必须被下载
            curContext.Response.WriteFile(OutFilePath); // 把文件流发送到客户端
            curContext.Response.End();


        }

    }
}
