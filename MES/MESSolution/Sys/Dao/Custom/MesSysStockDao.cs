using Dos.ORM;
using Sys.Model;
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
    public class MesSysStockDao : BaseDao
    {
        public static MesSysStockDao _Instance = null;

        public static MesSysStockDao Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MesSysStockDao();
                }
                return _Instance;
            }
        }


        #region 查询和分页方法

        /// <summary>
        /// 获取单个用户实体
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Mes_Sys_Stock GetStock(Mes_Sys_Stock obj)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            string sql = "select top 1 * from Mes_Sys_Stock where 1=1 ";
            if (!string.IsNullOrEmpty(obj.StockCode))
            {
                sql = sql + string.Format(" AND StockCode='{0}'", obj.StockCode);
            }
            if (!string.IsNullOrEmpty(obj.StockName))
            {
                sql = sql + string.Format(" AND StockName='{0}'", obj.StockName);
            }
            return this.CurDbSession.FromSql(sql.ToString()).ToFirstDefault<Mes_Sys_Stock>();
        }

        public List<Mes_Sys_Stock> FindByPage(Mes_Sys_Stock obj, ref PagerBase pager)
        {
            string sql = @"select * from Mes_Sys_Stock WHERE   1=1 ";

            if (!string.IsNullOrEmpty(obj.StockCode))
            {
                sql += string.Format(" AND StockCode Like '%{0}%'", obj.StockCode);
            }

            if (!string.IsNullOrEmpty(obj.StockName))
            {
                sql += string.Format(" AND StockName Like '%{0}%'", obj.StockName);
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
            return this.CurDbSession.FromSql(cmdPageSql).ToList<Mes_Sys_Stock>();
        }
      

        #endregion 查询和分页方法

       /// <summary>
       /// 保存用户
       /// </summary>
       /// <param name="obj"></param>
       /// <returns></returns>
        public bool Save(Mes_Sys_Stock obj)
        {
            int result = -1;
            if(obj.ID>0)
            {
                //修改 
                result = this.CurDbSession.Update<Mes_Sys_Stock>(obj);
            }
            else
            {
                result = this.CurDbSession.Insert<Mes_Sys_Stock>(obj);
            }
            if(result<0)
            {
                return false;
            }
            return true;           
        }

        /// <summary>
        /// 单个删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool Delete(string Id)
        {
            string sql = "delete from Mes_Sys_Stock where ID={0}";
            sql = string.Format(sql, Id);
            int result = this.CurDbSession.FromSql(sql).ExecuteNonQuery();                       
            if(result>0)
            {
                return true;
            }
            return false;
        }




    }
}
