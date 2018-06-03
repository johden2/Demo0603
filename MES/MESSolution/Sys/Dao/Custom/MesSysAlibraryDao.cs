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
    public class MesSysAlibraryDao : BaseDao
    {
        public static MesSysAlibraryDao _Instance = null;

        public static MesSysAlibraryDao Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MesSysAlibraryDao();
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
        public Mes_Sys_Alibrary GetAlibrary(Mes_Sys_Alibrary obj)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            string sql = "select top 1 * from Mes_Sys_Alibrary where 1=1 ";
            if (!string.IsNullOrEmpty(obj.AlibraryCode))
            {
                sql = sql + string.Format(" AND AlibraryCode='{0}'", obj.AlibraryCode);
            }
            if (!string.IsNullOrEmpty(obj.AlibraryName))
            {
                sql = sql + string.Format(" AND AlibraryName='{0}'", obj.AlibraryName);
            }
            return this.CurDbSession.FromSql(sql.ToString()).ToFirstDefault<Mes_Sys_Alibrary>();
        }

        public List<Mes_Sys_Alibrary> FindByPage(Mes_Sys_Alibrary obj, ref PagerBase pager)
        {
            string sql = @"select T1.*,T2.StockName as Show_StockCode from Mes_Sys_Alibrary T1 Left join Mes_Sys_Stock T2 ON T1.StockCode=T2.StockCode WHERE  1=1 ";

            if (!string.IsNullOrEmpty(obj.AlibraryCode))
            {
                sql += string.Format(" AND T1.AlibraryCode Like '%{0}%'", obj.AlibraryCode);
            }

            if (!string.IsNullOrEmpty(obj.AlibraryName))
            {
                sql += string.Format(" AND T1.AlibraryName Like '%{0}%'", obj.AlibraryName);
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
            return this.CurDbSession.FromSql(cmdPageSql).ToList<Mes_Sys_Alibrary>();
        }
      

        #endregion 查询和分页方法

       /// <summary>
       /// 保存用户
       /// </summary>
       /// <param name="obj"></param>
       /// <returns></returns>
        public bool Save(Mes_Sys_Alibrary obj)
        {
            int result = -1;
            if(obj.ID>0)
            {
                //修改 
                result = this.CurDbSession.Update<Mes_Sys_Alibrary>(obj);
            }
            else
            {
                result = this.CurDbSession.Insert<Mes_Sys_Alibrary>(obj);
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
            string sql = "delete from Mes_Sys_Alibrary where ID={0}";
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
