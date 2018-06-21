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
    /// <summary>
    /// 流程配置表
    /// </summary>
    public class MesSysFlowConfigDao : BaseDao
    {
        private static MesSysFlowConfigDao _Instance = null;
        private object lockobj = new object();

        public static MesSysFlowConfigDao Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MesSysFlowConfigDao();
                }
                return _Instance;
            }
        }


        #region 主信息

        public List<Mes_Sys_FlowConfig> FindByPage(Mes_Sys_FlowConfig obj, ref PagerBase pager)
        {
            string sql = @"SELECT T1.* FROM Mes_Sys_FlowConfig T1 
                        WHERE 1=1 ";

            if (!string.IsNullOrEmpty(obj.BusinessType))
            {
                sql += string.Format(" AND T1.BusinessType ='{0}'", obj.BusinessType);
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
            return this.CurDbSession.FromSql(cmdPageSql).ToList<Mes_Sys_FlowConfig>();
        }

        public bool DeleteExt(string Ids)
        {
            string sql = "delete from Mes_Sys_FlowConfig where ID IN {0}";
            sql = string.Format(sql, Ids);
            int result = this.CurDbSession.FromSql(sql).ExecuteNonQuery();
            return true;
        }

        #endregion 主信息
    }
}
