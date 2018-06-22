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
    public class MesStockInStockDao : BaseDao
    {
        public static MesStockInStockDao _Instance = null;

        public static MesStockInStockDao Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MesStockInStockDao();
                }
                return _Instance;
            }
        }


        #region 查询和分页方法


        public List<Mes_Stock_InStock> FindByPage(Mes_Stock_InStock obj, ref PagerBase pager)
        {
            string sql = @"SELECT T1.* FROM Mes_Stock_InStock T1 WHERE 1=1 ";

            if (TConvertHelper.FormatDBInt(obj.BillType) > 0)
            {
                sql += string.Format(" AND T1.BillType = '{0}'", obj.BillType);
            }
            if (!string.IsNullOrEmpty(obj.BillNo))
            {
                sql += string.Format(" AND T1.BillNo Like '%{0}%'", obj.BillNo);
            }
            if (!string.IsNullOrEmpty(obj.InStockDateStart))
            {
                sql += string.Format(" AND T1.InStockDate >='{0}'", obj.InStockDateStart);
            }
            if (!string.IsNullOrEmpty(obj.InStockDateEnd))
            {
                sql += string.Format(" AND T1.InStockDate <'{0}'", TConvertHelper.FormatDBDate(obj.InStockDateEnd).AddDays(1));
            }
            if (obj.AuditStatus > 0)
            {
                sql += string.Format(" AND T1.AuditStatus = {0}", obj.AuditStatus);
            }
            if (obj.CheckStatus > 0)
            {
                sql += string.Format(" AND T1.CheckStatus = {0}", obj.CheckStatus);
            }
            if (!string.IsNullOrEmpty(obj.SupplierName))
            {
                sql += string.Format(" AND T1.SupplierName Like '%{0}%'", obj.SupplierName);
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
            return this.CurDbSession.FromSql(cmdPageSql).ToList<Mes_Stock_InStock>();
        }

        public List<Mes_Stock_InStock> FindByCond(Mes_Stock_InStock obj)
        {
            string sql = @"SELECT T1.* FROM Mes_Stock_InStock T1 WHERE 1=1 ";

            if (TConvertHelper.FormatDBInt(obj.BillType) > 0)
            {
                sql += string.Format(" AND T1.BillType = '{0}'", obj.BillType);
            }
            if (!string.IsNullOrEmpty(obj.BillNo))
            {
                sql += string.Format(" AND T1.BillNo Like '%{0}%'", obj.BillNo);
            }
            if (!string.IsNullOrEmpty(obj.InStockDateStart))
            {
                sql += string.Format(" AND T1.InStockDate >='{0}'", obj.InStockDateStart);
            }
            if (!string.IsNullOrEmpty(obj.InStockDateEnd))
            {
                sql += string.Format(" AND T1.InStockDate <'{0}'", TConvertHelper.FormatDBDate(obj.InStockDateEnd).AddDays(1));
            }
            if (obj.AuditStatus > 0)
            {
                sql += string.Format(" AND T1.AuditStatus = {0}", obj.AuditStatus);
            }
            if (obj.CheckStatus > 0)
            {
                sql += string.Format(" AND T1.CheckStatus = {0}", obj.CheckStatus);
            }
            if (!string.IsNullOrEmpty(obj.SupplierName))
            {
                sql += string.Format(" AND T1.SupplierName Like '%{0}%'", obj.SupplierName);
            }
            if (!string.IsNullOrEmpty(obj.CreatedTimeStart))
            {
                sql += string.Format(" AND T1.CreatedTime >='{0}'", obj.CreatedTimeStart);
            }
            if (!string.IsNullOrEmpty(obj.CreatedTimeEnd))
            {
                sql += string.Format(" AND T1.CreatedTime <'{0}'", TConvertHelper.FormatDBDate(obj.CreatedTimeEnd).AddDays(1));
            }

            sql += " ORDER BY CreatedTime DESC";
         
            //返回当前页的记录数
            return this.CurDbSession.FromSql(sql).ToList<Mes_Stock_InStock>();
        }

        /// <summary>
        /// 审批进货单
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string DoAudit(Mes_Stock_InStockApproval obj)
        {
            //1.先校验当前的审核人是否为配置对象，并且没有审核过
            string sql = @"SELECT TOP 1 ID FROM dbo.Mes_Sys_FlowConfig  WITH(NOLOCK) WHERE BusinessType = '{0}' AND OptUserID = {1}";
            sql = string.Format(sql, FlowBusinessType.A, obj.ApproverID);
            int result = this.CurDbSession.FromSql(sql).ToScalar<int>();
            if (result <= 0)
            {
                return string.Format("流程中未配置【{0}】对进货单进行审批！", obj.ApproverName);
            }

            sql = @"SELECT TOP 1 ID FROM Mes_Stock_InStockApproval WITH(NOLOCK) WHERE BillNo = '{0}' AND BillType='{1}'
                    AND ApproverID = {2}";
            sql = string.Format(sql, obj.BillNo, obj.BillType, obj.ApproverID);
            result = this.CurDbSession.FromSql(sql).ToScalar<int>();
            if (result > 0)
            {
                return string.Format("【{0}】已经审批过了，不允许重复审批！", obj.ApproverName);
            }
           

            //2.保存审批记录
            this.CurDbSession.Insert(obj);

            //3.检测是否所有人已审批完，并修改状态
            sql = @"SELECT TOP 1 ID FROM dbo.Mes_Sys_FlowConfig WITH(NOLOCK) WHERE BusinessType = '{0}'
                AND OptUserID NOT IN (
                 SELECT ApproverID FROM Mes_Stock_InStockApproval WITH(NOLOCK) WHERE BillNo = '{1}' AND BillType='{2}'
                )";
            sql = string.Format(sql, FlowBusinessType.A, obj.BillNo, obj.BillType);
            result = this.CurDbSession.FromSql(sql).ToScalar<int>();
            if (result <= 0) //表示所有人都已审批，可以修改进货单状态为【已审批】
            {
                sql = string.Format("UPDATE Mes_Stock_InStock SET AuditStatus = {0} WHERE ID = {1}", obj.InStockID, AuditEnum.Yes);
                result = this.CurDbSession.FromSql(sql).ExecuteNonQuery();
            }

            return string.Empty;
        }

        public bool DeleteExt(int id)
        {
            string sql = "DELETE FROM Mes_Stock_InStock WHERE ID ={0};";
            sql = string.Format(sql, id);
            int result = this.CurDbSession.FromSql(sql).ExecuteNonQuery();
            return result > 0;
        }

        public string GenBillNo()
        {
            string sPrefix = DateTime.Now.ToString("yyMM");
            string sql = "SELECT ISNULL(MAX(BillNo),'') FROM Mes_Stock_InStock WITH(NOLOCK) WHERE BillNo LIKE '{0}%'";
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

        #endregion 查询和分页方法

        #region 明细信息

        public List<Mes_Stock_InStockItem> FindItemByPage(Mes_Stock_InStockItem obj, ref PagerBase pager)
        {
            string sql = @"SELECT T1.*,T2.StockName FROM Mes_Stock_InStockItem T1 WITH(NOLOCK) 
                        LEFT JOIN dbo.Mes_Sys_Stock T2 WITH(NOLOCK)  ON T1.StockID = T2.ID
                        WHERE 1=1 AND T1.InStockID = {0}";
            sql = string.Format(sql, obj.InStockID);
            string orderBy = pager.OrderBy;
            if (string.IsNullOrEmpty(orderBy))
            {
                orderBy = "ResNum ASC";
            }
            string cmdPageSql = string.Format(BaseDao.PageSql, orderBy, sql, pager.StartNo, pager.EndNo);
            string cmdCountSql = string.Format(BaseDao.CountSql, sql.Substring(sql.ToLower().IndexOf("from", StringComparison.Ordinal)));
            //查询总记录数
            pager.TotalItemCount = this.CurDbSession.FromSql(cmdCountSql).ToScalar<int>();
            //返回当前页的记录数
            return this.CurDbSession.FromSql(cmdPageSql).ToList<Mes_Stock_InStockItem>();
        }

        /// <summary>
        /// 生成行号
        /// </summary>
        /// <returns></returns>
        public int GenResNum(Mes_Stock_InStock obj)
        {
            string sql = "SELECT ISNULL(MAX(ResNum),0) FROM Mes_Stock_InStockItem T1 WITH(NOLOCK) WHERE InStockID = {0}";
            sql = string.Format(sql, obj.ID);
            int resNo = this.CurDbSession.FromSql(sql).ToScalar<int>();
            return resNo + 1;
        }

        public bool DeleteItemExt(Mes_Stock_InStockItem obj)
        {
            string sql = string.Format("DELETE FROM Mes_Stock_InStockItem WHERE ID={0}", obj.ID);
            this.CurDbSession.FromSql(sql).ExecuteNonQuery();
            return true;
        }

        #endregion 明细信息

    }
}
