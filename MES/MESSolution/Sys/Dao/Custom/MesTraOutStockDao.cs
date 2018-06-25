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
    public class MesTraOutStockDao : BaseDao
    {
        public static MesTraOutStockDao _Instance = null;

        public static MesTraOutStockDao Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MesTraOutStockDao();
                }
                return _Instance;
            }
        }


        #region 查询和分页方法



        public List<Mes_Tra_OutStock> FindByPage(Mes_Tra_OutStock obj, ref PagerBase pager)
        {
            string sql = @"select T1.*,T2.OrgName as Show_OrgID from Mes_Tra_OutStock T1 with (nolock) Left join Mes_Sys_Organization T2 with (nolock) 
            on T1.OrgID=T2.ID WHERE   1=1 ";

            if (!string.IsNullOrEmpty(obj.Barcode))
            {
                sql += string.Format(" AND T1.Barcode='{0}'", obj.Barcode);
            }

            if (!string.IsNullOrEmpty(obj.SN))
            {
                sql += string.Format(" AND T1.SN='{0}'", obj.SN);
            }     
            if(!string.IsNullOrEmpty(obj.PackSN))
            {
                sql += string.Format(" AND T1.PackSN='{0}'", obj.PackSN);
            }
            if(!string.IsNullOrEmpty(obj.MaterialProNo))
            {
                sql += string.Format(" AND T1.MaterialProNo='{0}'", obj.MaterialProNo);
            }
            if(!string.IsNullOrEmpty(obj.MaterialCode))
            {
                sql += string.Format(" AND T1.MaterialCode='{0}'", obj.MaterialCode);
            }
            if (!string.IsNullOrEmpty(obj.OrderNumber))
            {
                sql += string.Format(" AND T1.OrderNumber='{0}'", obj.OrderNumber);
            }
            if(!string.IsNullOrEmpty(obj.ScanTimeStart))
            {
                sql += string.Format(" AND T1.ScanTime>='{0}'", obj.ScanTimeStart);
            }
            if(!string.IsNullOrEmpty(obj.ScanTimeEnd))
            {
                sql += string.Format(" AND T1.ScanTime<'{0}'", TConvertHelper.FormatDBDate(obj.ScanTimeEnd).AddDays(1));
            }

            string orderBy = pager.OrderBy;
            if (string.IsNullOrEmpty(orderBy))
            {
                orderBy = "ScanTime DESC";
            }
            string cmdPageSql = string.Format(BaseDao.PageSql, orderBy, sql, pager.StartNo, pager.EndNo);
            string cmdCountSql = string.Format(BaseDao.CountSql, sql.Substring(sql.ToLower().IndexOf("from", StringComparison.Ordinal)));
            //查询总记录数
            pager.TotalItemCount = this.CurDbSession.FromSql(cmdCountSql).ToScalar<int>();
            //返回当前页的记录数
            return this.CurDbSession.FromSql(cmdPageSql).ToList<Mes_Tra_OutStock>();
        }
      

        #endregion 查询和分页方法

      




    }
}
