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
    /// 销售订单
    /// </summary>
    public class MesPlanWorkOrderMaterialDao : BaseDao
    {
        private static MesPlanWorkOrderMaterialDao _Instance = null;
        private object lockobj = new object();

        public static MesPlanWorkOrderMaterialDao Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MesPlanWorkOrderMaterialDao();
                }
                return _Instance;
            }
        }


        #region 主信息

        public List<Mes_Plan_Material> FindByPage(Mes_Plan_Material obj, ref PagerBase pager)
        {
            string sql = @"SELECT T1.*,T2.Name as Show_ProcessCode FROM Mes_Plan_Material T1 
                        LEFT JOIN dbo.Mes_Tec_Process T2 ON T1.ProcessCode = T2.ProcessCode 
                        WHERE 1=1 ";

            if (TConvertHelper.FormatDBInt(obj.WorkOrderNumber) > 0)
            {
                sql += string.Format(" AND T1.WorkOrderNumber = '{0}'", obj.WorkOrderNumber);
            }
            if (!string.IsNullOrEmpty(obj.MaterialProNo))
            {
                sql += string.Format(" AND T1.MaterialProNo='{0}'", obj.MaterialProNo);
            }           
            if (!string.IsNullOrEmpty(obj.MaterialCode))
            {
                sql += string.Format(" AND T1.MaterialCode= '{0}'", obj.MaterialCode);
            }
            if (!string.IsNullOrEmpty(obj.ProcessCode))
            {
                sql += string.Format(" AND T1.ProcessCode= '{0}'", obj.ProcessCode);
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
            return this.CurDbSession.FromSql(cmdPageSql).ToList<Mes_Plan_Material>();
        }

        /// <summary>
        /// 按照ID单个查找工单
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Mes_Plan_Material Find(Mes_Plan_Material obj)
        {
            string sql = @"SELECT top 1 T1.*,T2.Name as Show_ProcessCode FROM Mes_Plan_Material T1 
                        LEFT JOIN dbo.Mes_Tec_Process T2 ON T1.ProcessCode = T2.ProcessCode 
                        WHERE 1=1  ";
            if (obj.ID>0)
            {
                sql += string.Format(" AND T1.ID = '{0}'", obj.ID);
            }
            return this.CurDbSession.FromSql(sql.ToString()).ToFirstDefault<Mes_Plan_Material>();
        }

        public bool Delete(string Id)
        {
            string sql = "delete from Mes_Plan_Material where ID={0}";
            sql = string.Format(sql, Id);
            int result = this.CurDbSession.FromSql(sql).ExecuteNonQuery();
            if(result>0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Save(Mes_Plan_Material obj)
        {
            int result = -1;
            if(obj.ID>0)
            {
                result = this.CurDbSession.Update<Mes_Plan_Material>(obj);
            }
            else
            {
                result = this.CurDbSession.Insert<Mes_Plan_Material>(obj);
            }
            if(result<0)
            {
                return false;
            }
            return true;
        }

        #endregion

    }
}
