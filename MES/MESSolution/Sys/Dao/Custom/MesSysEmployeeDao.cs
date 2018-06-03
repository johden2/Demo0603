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
    public class MesSysEmployeeDao : BaseDao
    {
        public static MesSysEmployeeDao _Instance = null;

        public static MesSysEmployeeDao Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MesSysEmployeeDao();
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
        public Mes_Sys_Employee GetUser(Mes_Sys_Employee obj)
        {
            string sql = "SELECT TOP 1 T1.* FROM Mes_Sys_Employee T1 WITH (NOLOCK) WHERE T1.RecordStatus=1";
            if (!string.IsNullOrEmpty(obj.EmpNo))
            {
                sql = sql + string.Format(" AND T1.EmpNo='{0}'", obj.EmpNo);
            }
            if (!string.IsNullOrEmpty(obj.EmpName))
            {
                sql = sql + string.Format(" AND T1.EmpName LIKE '%{0}%'", obj.EmpName);
            }

            return this.CurDbSession.FromSql(sql.ToString()).ToFirstDefault<Mes_Sys_Employee>();
        }

        public List<Mes_Sys_Employee> FindByPage(Mes_Sys_Employee obj, ref PagerBase pager)
        {
            string sql = @"SELECT T1.*,T2.OrgName FROM Mes_Sys_Employee T1 WITH(NOLOCK)
                        LEFT JOIN dbo.Mes_Sys_Organization T2 WITH(NOLOCK) ON T1.OrgID = T2.ID
                        WHERE T1.RecordStatus=1";
            if (!string.IsNullOrEmpty(obj.EmpNo))
            {
                sql = sql + string.Format(" AND T1.EmpNo='{0}'", obj.EmpNo);
            }
            if (!string.IsNullOrEmpty(obj.EmpName))
            {
                sql = sql + string.Format(" AND T1.EmpName LIKE '%{0}%'", obj.EmpName);
            }
            if (obj.OrgID > 0)
            {
                sql = sql + string.Format(" AND T1.OrgID = {0}", obj.OrgID);
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
            return this.CurDbSession.FromSql(cmdPageSql).ToList<Mes_Sys_Employee>();
        }
      
        #endregion 查询和分页方法


    }
}
