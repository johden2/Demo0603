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
    public class MesSysDataCollectPointDao : BaseDao
    {
        public static MesSysDataCollectPointDao _Instance = null;

        public static MesSysDataCollectPointDao Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MesSysDataCollectPointDao();
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
        public Mes_Sys_DataCollectPoint GetSupplier(Mes_Sys_DataCollectPoint obj)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            string sql = "select top 1 * from Mes_Sys_DataCollectPoint where 1=1 ";
            if (!string.IsNullOrEmpty(obj.CollectCode))
            {
                sql = sql + string.Format(" AND CollectCode='{0}'", obj.CollectCode);
            }
            if (!string.IsNullOrEmpty(obj.CollectName))
            {
                sql = sql + string.Format(" AND CollectName='{0}'", obj.CollectName);
            }
            return this.CurDbSession.FromSql(sql.ToString()).ToFirstDefault<Mes_Sys_DataCollectPoint>();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public List<Mes_Sys_DataCollectPoint> FindByPage(Mes_Sys_DataCollectPoint obj, ref PagerBase pager)
        {
            string sql = @"select * from Mes_Sys_DataCollectPoint WHERE   1=1 ";

            if (!string.IsNullOrEmpty(obj.CollectCode))
            {
                sql += string.Format(" AND CollectCode Like '%{0}%'", obj.CollectCode);
            }

            if (!string.IsNullOrEmpty(obj.CollectName))
            {
                sql += string.Format(" AND CollectName Like '%{0}%'", obj.CollectName);
            }
            if (!string.IsNullOrEmpty(obj.MacAddress))
            {
                sql += string.Format(" AND MacAddress Like '%{0}%'", obj.MacAddress);
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
            return this.CurDbSession.FromSql(cmdPageSql).ToList<Mes_Sys_DataCollectPoint>();
        }
      

        #endregion 查询和分页方法

       /// <summary>
       /// 保存用户
       /// </summary>
       /// <param name="obj"></param>
       /// <returns></returns>
        public bool Save(Mes_Sys_DataCollectPoint obj)
        {
            int result = -1;
            if(obj.ID>0)
            {
                //修改 
                result = this.CurDbSession.Update<Mes_Sys_DataCollectPoint>(obj);
            }
            else
            {
                result = this.CurDbSession.Insert<Mes_Sys_DataCollectPoint>(obj);
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
            string sql = "delete from Mes_Sys_DataCollectPoint where ID={0}";
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
