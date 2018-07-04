using Dos.ORM;
using Sys.Model;
using Sys.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using SysTools = Sys.Tools;

namespace Sys.Dao
{
    public class SourceBarcodeDao : BaseDao
    {
        public static SourceBarcodeDao _Instance = null;

        public static SourceBarcodeDao Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new SourceBarcodeDao();
                }
                return _Instance;
            }
        }


        #region 查询和分页方法


        public List<Mes_Tra_SourceBarcode> FindByPage(Mes_Tra_SourceBarcode obj, ref PagerBase pager)
        {
            string sql = @"SELECT T1.* FROM Mes_Tra_SourceBarcode T1 WITH(NOLOCK) WHERE 1=1 ";
            if (!string.IsNullOrEmpty(obj.Barcode))
            {
                sql += string.Format(" AND T1.Barcode Like '{0}%'", obj.Barcode);
            }
            if (!string.IsNullOrEmpty(obj.PackSN))
            {
                sql += string.Format(" AND T1.PackSN Like '{0}%'", obj.PackSN);
            }
            if (!string.IsNullOrEmpty(obj.CreatedTimeStart))
            {
                sql += string.Format(" AND T1.CreatedTime >='{0}'", obj.CreatedTimeStart);
            }
            if (!string.IsNullOrEmpty(obj.CreatedTimeEnd))
            {
                sql += string.Format(" AND T1.CreatedTime <'{0}'", TConvertHelper.FormatDBDate(obj.CreatedTimeEnd).AddDays(1));
            }
           
            string orderBy = pager.OrderBy;
            if (string.IsNullOrEmpty(orderBy))
            {
                orderBy = "CreatedTime DESC";
            }
            string cmdPageSql = string.Format(BaseDao.PageSql, orderBy, sql, pager.StartNo, pager.EndNo);
            string cmdCountSql = string.Format(BaseDao.CountSql, sql.Substring(sql.ToLower().IndexOf("from", StringComparison.Ordinal)));
            //查询总记录数
            pager.TotalItemCount = this.CurDbSession.FromSql(cmdCountSql).ToScalar<int>();
            //返回当前页的记录数
            return this.CurDbSession.FromSql(cmdPageSql).ToList<Mes_Tra_SourceBarcode>();
        }

        /// <summary>
        /// 判断条码是否重复
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public bool IsExist(string barcode)
        {
            return this.CurDbSession.Exists<Mes_Tra_SourceBarcode>(p => p.Barcode == barcode);
        }

        /// <summary>
        /// 导入条码信息
        /// </summary>
        /// <param name="list"></param>
        /// <param name="resultList"></param>
        /// <returns></returns>
        public bool Import(List<Mes_Tra_SourceBarcode> list, IList<ImportMessageModel> resultList)
        {
            try
            {
                int result = this.CurDbSession.Insert<Mes_Tra_SourceBarcode>(list);
                return true;
            }
            catch (Exception ex)
            {
                resultList.Add(new ImportMessageModel { RowData = "导入失败", RowMessage = "导入出错，错误信息：" + ex.Message });
            }
            return false;
        }

        #endregion 查询和分页方法


    }
}
