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
    public class MesPlanSaleOrderDao : BaseDao
    {
        private static MesPlanSaleOrderDao _Instance = null;
        private object lockobj = new object();

        public static MesPlanSaleOrderDao Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MesPlanSaleOrderDao();
                }
                return _Instance;
            }
        }


        #region 主订单信息

        public List<Mes_Plan_SaleOrder> FindByPage(Mes_Plan_SaleOrder obj, ref PagerBase pager)
        {
            string sql = @"SELECT T1.*,T2.CustomerName FROM Mes_Plan_SaleOrder T1 WITH(NOLOCK)
                        LEFT JOIN dbo.Mes_Sys_Customer T2 WITH(NOLOCK) ON T1.CustomerID = T2.ID
                        WHERE OrderStatus<>20 ";

            if (TConvertHelper.FormatDBInt(obj.OrderType) > 0)
            {
                sql += string.Format(" AND T1.OrderType = '{0}'", obj.OrderType);
            }
            if (!string.IsNullOrEmpty(obj.OrderNo))
            {
                sql += string.Format(" AND T1.OrderNo Like '%{0}%'", obj.OrderNo);
            }
            if (!string.IsNullOrEmpty(obj.OrderDateStart))
            {
                sql += string.Format(" AND T1.OrderDate >='{0}'", obj.OrderDateStart);
            }
            if (!string.IsNullOrEmpty(obj.OrderDateEnd))
            {
                sql += string.Format(" AND T1.OrderDate <'{0}'", TConvertHelper.FormatDBDate(obj.OrderDateEnd).AddDays(1));
            }
            if (obj.OrderStatus > 0)
            {
                sql += string.Format(" AND T1.OrderStatus = {0}", obj.OrderStatus);
            }
            if (!string.IsNullOrEmpty(obj.CustomerName))
            {
                sql += string.Format(" AND T1.CustomerName Like '%{0}%'", obj.CustomerName);
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
            return this.CurDbSession.FromSql(cmdPageSql).ToList<Mes_Plan_SaleOrder>();
        }

        public bool DeleteExt(Mes_Plan_SaleOrder obj)
        {
            //作废订单
            obj.OrderStatus = OrderStatus.ZF;
            this.CurDbSession.Update < Mes_Plan_SaleOrder>(obj);
            return true;
        }

        /// <summary>
        /// 生成订单号
        /// </summary>
        /// <returns></returns>
        public string GenOrderNo()
        {
            string sPrefix = DateTime.Now.ToString("yyMM");
            string sql = "SELECT ISNULL(MAX(OrderNo),'') FROM Mes_Plan_SaleOrder WITH(NOLOCK) WHERE OrderNo LIKE '{0}%'";
            sql = string.Format(sql, sPrefix);
            string orderNo = this.CurDbSession.FromSql(sql).ToScalar<string>();
            int maxNo = 0;
            if (!string.IsNullOrEmpty(orderNo))
            {
                orderNo = orderNo.Replace(sPrefix, "");
                maxNo = TConvertHelper.FormatDBInt(orderNo);
            }

            maxNo = maxNo + 1;
            return sPrefix + maxNo.ToString().PadLeft(6, '0');
        }

        #endregion 主订单信息

        #region 订单明细

        public List<Mes_Plan_SaleOrderItem> FindItemByPage(Mes_Plan_SaleOrderItem obj, ref PagerBase pager)
        {
            if (obj.OrderID <= 0) return null;
            Mes_Plan_SaleOrder order = this.Find<Mes_Plan_SaleOrder, int>(obj.OrderID);
            if (order == null) return null;

            string sql = @"SELECT T1.* FROM Mes_Plan_SaleOrderItem T1 WITH(NOLOCK) WHERE T1.OrderType = '{0}' AND T1.OrderNo = '{1}'";
            sql = string.Format(sql, order.OrderType, order.OrderNo);
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
            return this.CurDbSession.FromSql(cmdPageSql).ToList<Mes_Plan_SaleOrderItem>();
        }

        /// <summary>
        /// 生成订单行号
        /// </summary>
        /// <returns></returns>
        public int GenOrderResNum(Mes_Plan_SaleOrder order)
        {
            string sql = "SELECT ISNULL(MAX(ResNum),0) FROM Mes_Plan_SaleOrderItem T1 WITH(NOLOCK) WHERE OrderType = '{0}' AND OrderNo='{1}'";
            sql = string.Format(sql, order.OrderType, order.OrderNo);
            int resNo = this.CurDbSession.FromSql(sql).ToScalar<int>();
            return resNo + 1;
        }

        public bool DeleteItemExt(Mes_Plan_SaleOrderItem obj)
        {
            string sql = string.Format("DELETE FROM Mes_Plan_SaleOrderItem WHERE ID={0}", obj.ID);
            this.CurDbSession.FromSql(sql).ExecuteNonQuery();
            return true;
        }

        #endregion 订单明细

    }
}
