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
    public class MesPlanPlanInforDao : BaseDao
    {
        private static MesPlanPlanInforDao _Instance = null;
        private object lockobj = new object();

        public static MesPlanPlanInforDao Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MesPlanPlanInforDao();
                }
                return _Instance;
            }
        }


        #region 主信息

        public List<Mes_Plan_PlanInfor> FindByPage(Mes_Plan_PlanInfor obj, ref PagerBase pager)
        {
            string sql = @"SELECT T1.*,T2.WorkShopName FROM Mes_Plan_PlanInfor T1 
                        LEFT JOIN dbo.Mes_Sys_WorkShop T2 ON T1.WorkShopID = T2.ID
                        WHERE 1=1 ";

            if (TConvertHelper.FormatDBInt(obj.OrderType) > 0)
            {
                sql += string.Format(" AND T1.OrderType = '{0}'", obj.OrderType);
            }
            if (!string.IsNullOrEmpty(obj.OrderNo))
            {
                sql += string.Format(" AND T1.OrderNo LIKE '%{0}%'", obj.OrderNo);
            }
            if (!string.IsNullOrEmpty(obj.PlanCode))
            {
                sql += string.Format(" AND T1.PlanCode LIKE '%{0}%'", obj.PlanCode);
            }
            if (obj.PlanStatus > 0)
            {
                sql += string.Format(" AND T1.PlanStatus = {0}", obj.PlanStatus);
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
            return this.CurDbSession.FromSql(cmdPageSql).ToList<Mes_Plan_PlanInfor>();
        }

        public bool DeleteExt(Mes_Plan_SaleOrder obj)
        {
            //作废订单
            obj.OrderStatus = OrderStatus.ZF;
            this.CurDbSession.Update < Mes_Plan_SaleOrder>(obj);
            return true;
        }

        /// <summary>
        /// 生成计划批号
        /// </summary>
        /// <returns></returns>
        public string GenPlanNo()
        {
            string sPrefix = DateTime.Now.ToString("yyMM");
            string sql = "SELECT ISNULL(MAX(PlanCode),'') FROM Mes_Plan_PlanInfor WITH(NOLOCK) WHERE PlanCode LIKE '{0}%'";
            sql = string.Format(sql, sPrefix);
            string planNo = this.CurDbSession.FromSql(sql).ToScalar<string>();
            int maxNo = 0;
            if (!string.IsNullOrEmpty(planNo))
            {
                planNo = planNo.Replace(sPrefix, "");
                maxNo = TConvertHelper.FormatDBInt(planNo);
            }

            maxNo = maxNo + 1;
            return sPrefix + maxNo.ToString().PadLeft(6, '0');
        }

        #endregion 主信息


    }
}
