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
    public class MesSysCustomerDao : BaseDao
    {
        public static MesSysCustomerDao _Instance = null;

        public static MesSysCustomerDao Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MesSysCustomerDao();
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
        public Mes_Sys_Customer GetCustomer(Mes_Sys_Customer obj)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            string sql = "select top 1 * from Mes_Sys_Customer where 1=1 ";
            if (!string.IsNullOrEmpty(obj.CustomerCode))
            {
                sql = sql + string.Format(" AND CustomerCode='{0}'", obj.CustomerCode);
            }
            if (!string.IsNullOrEmpty(obj.CustomerName))
            {
                sql = sql + string.Format(" AND CustomerName='{0}'", obj.CustomerName);
            }
            return this.CurDbSession.FromSql(sql.ToString()).ToFirstDefault<Mes_Sys_Customer>();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public List<Mes_Sys_Customer> FindByPage(Mes_Sys_Customer obj, ref PagerBase pager)
        {
            string sql = @"select * from Mes_Sys_Customer WHERE   1=1 ";

            if (!string.IsNullOrEmpty(obj.CustomerCode))
            {
                sql = sql + string.Format(" AND CustomerCode='{0}'", obj.CustomerCode);
            }
            if (!string.IsNullOrEmpty(obj.CustomerName))
            {
                sql = sql + string.Format(" AND CustomerName='{0}'", obj.CustomerName);
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
            return this.CurDbSession.FromSql(cmdPageSql).ToList<Mes_Sys_Customer>();
        }
      

        #endregion 查询和分页方法

       /// <summary>
       /// 保存用户
       /// </summary>
       /// <param name="obj"></param>
       /// <returns></returns>
        public bool Save(Mes_Sys_Customer obj)
        {
            int result = -1;
            if(obj.ID>0)
            {
                //修改 
                result = this.CurDbSession.Update<Mes_Sys_Customer>(obj);
            }
            else
            {
                result = this.CurDbSession.Insert<Mes_Sys_Customer>(obj);
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
            string sql = "delete from Mes_Sys_Customer where ID={0}";
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
