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
    public class MesTecTestDataPositionDao : BaseDao
    {
        public static MesTecTestDataPositionDao _Instance = null;

        public static MesTecTestDataPositionDao Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MesTecTestDataPositionDao();
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
        public Mes_Tec_TestDataPosition GetTestDataPosition(Mes_Tec_TestDataPosition obj)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            string sql = "select top 1 * from Mes_Tec_TestDataPosition where 1=1 ";
            if (!string.IsNullOrEmpty(obj.ProcessCode))
            {
                sql = sql + string.Format(" AND ProcessCode='{0}'", obj.ProcessCode);
            }
            if (!string.IsNullOrEmpty(obj.DataTableName))
            {
                sql = sql + string.Format(" AND DataTableName='{0}'", obj.DataTableName);
            }
            if (!string.IsNullOrEmpty(obj.DataProcessName))
            {
                sql = sql + string.Format(" AND DataProcessName='{0}'", obj.DataProcessName);
            }
            return this.CurDbSession.FromSql(sql.ToString()).ToFirstDefault<Mes_Tec_TestDataPosition>();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public List<Mes_Tec_TestDataPosition> FindByPage(Mes_Tec_TestDataPosition obj, ref PagerBase pager)
        {
            string sql = @"SELECT T1.*,T2.Name AS ProcessName FROM Mes_Tec_TestDataPosition T1  WITH(NOLOCK) 
                        LEFT JOIN Mes_Tec_Process T2 WITH(NOLOCK)  ON T1.ProcessCode=T2.ProcessCode
                        WHERE   1=1 ";

            if (!string.IsNullOrEmpty(obj.ProcessCode))
            {
                sql += string.Format(" AND T1.ProcessCode Like '%{0}%'", obj.ProcessCode);
            }

            if (!string.IsNullOrEmpty(obj.DataTableName))
            {
                sql += string.Format(" AND T1.DataTableName Like '%{0}%'", obj.DataTableName);
            }
            if (!string.IsNullOrEmpty(obj.DataProcessName))
            {
                sql += string.Format(" AND T1.DataProcessName Like '%{0}%'", obj.DataProcessName);
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
            return this.CurDbSession.FromSql(cmdPageSql).ToList<Mes_Tec_TestDataPosition>();
        }
      

        #endregion 查询和分页方法

       /// <summary>
       /// 保存用户
       /// </summary>
       /// <param name="obj"></param>
       /// <returns></returns>
        public bool Save(Mes_Tec_TestDataPosition obj)
        {
            int result = -1;
            if(obj.ID>0)
            {
                //修改 
                result = this.CurDbSession.Update<Mes_Tec_TestDataPosition>(obj);
            }
            else
            {
                result = this.CurDbSession.Insert<Mes_Tec_TestDataPosition>(obj);
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
            string sql = "delete from Mes_Tec_TestDataPosition where ID={0}";
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
