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
    /// 进货单审批表
    /// </summary>
    public class MesStockInStockApprovalDao : BaseDao
    {
        private static MesStockInStockApprovalDao _Instance = null;
        private object lockobj = new object();

        public static MesStockInStockApprovalDao Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MesStockInStockApprovalDao();
                }
                return _Instance;
            }
        }


        #region 主信息

        public List<Mes_Stock_InStockApproval> FindByPage(Mes_Stock_InStockApproval obj, ref PagerBase pager)
        {
            string sql = @"SELECT T1.* FROM Mes_Stock_InStockApproval T1 
                        WHERE 1=1 ";

            if (!string.IsNullOrEmpty(obj.BillNo))
            {
                sql += string.Format(" AND T1.BillNo ='{0}'", obj.BillNo);
            }
            if (!string.IsNullOrEmpty(obj.BillType))
            {
                sql += string.Format(" AND T1.BillType ='{0}'", obj.BillType);
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
            return this.CurDbSession.FromSql(cmdPageSql).ToList<Mes_Stock_InStockApproval>();
        }

        public bool DeleteExt(Mes_Stock_InStockApproval obj)
        {
            ////作废订单
            //obj.OrderStatus = OrderStatus.ZF;
            //this.CurDbSession.Update < Mes_Plan_SaleOrder>(obj);
            return true;
        }

        #endregion 主信息


    }
}
