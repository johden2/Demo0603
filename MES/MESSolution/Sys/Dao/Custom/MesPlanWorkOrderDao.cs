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
    public class MesPlanWorkOrderDao : BaseDao
    {
        private static MesPlanWorkOrderDao _Instance = null;
        private object lockobj = new object();

        public static MesPlanWorkOrderDao Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MesPlanWorkOrderDao();
                }
                return _Instance;
            }
        }


        #region 主信息

        public List<Mes_Plan_WorkOrder> FindByPage(Mes_Plan_WorkOrder obj, ref PagerBase pager)
        {
            string sql = @"SELECT T1.*,T2.WorkShopName FROM Mes_Plan_WorkOrder T1 
                        LEFT JOIN dbo.Mes_Sys_WorkShop T2 ON T1.WorkShopCode = T2.WorkShopCode
                        WHERE 1=1 ";

            if (TConvertHelper.FormatDBInt(obj.WorkOrderNumber) > 0)
            {
                sql += string.Format(" AND T1.WorkOrderNumber = '{0}'", obj.WorkOrderNumber);
            }
            if (!string.IsNullOrEmpty(obj.PlanCode))
            {
                sql += string.Format(" AND T1.PlanCode LIKE '%{0}%'", obj.PlanCode);
            }
            if (!string.IsNullOrEmpty(obj.MaterialProNo))
            {
                sql += string.Format(" AND T1.MaterialProNo= '{0}'", obj.MaterialProNo);
            }
            if (!string.IsNullOrEmpty(obj.MaterialCode))
            {
                sql += string.Format(" AND T1.MaterialCode= '{0}'", obj.MaterialCode);
            }
            if (obj.WorkOrderStatus > 0)
            {
                sql += string.Format(" AND T1.WorkOrderStatus = {0}", obj.WorkOrderStatus);
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
            return this.CurDbSession.FromSql(cmdPageSql).ToList<Mes_Plan_WorkOrder>();
        }

        /// <summary>
        /// 按照工单号单个查找工单
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Mes_Plan_WorkOrder Find(Mes_Plan_WorkOrder obj)
        {
            string sql = @"SELECT T1.*,T2.WorkShopName FROM Mes_Plan_WorkOrder T1 
                        LEFT JOIN dbo.Mes_Sys_WorkShop T2 ON T1.WorkShopCode = T2.WorkShopCode 
                        WHERE 1=1 ";
            if (TConvertHelper.FormatDBInt(obj.WorkOrderNumber) > 0)
            {
                sql += string.Format(" AND T1.WorkOrderNumber = '{0}'", obj.WorkOrderNumber);
            }
            return this.CurDbSession.FromSql(sql.ToString()).ToFirstDefault<Mes_Plan_WorkOrder>();
        }

        public bool Delete(string Id)
        {
            string sql = "delete from Mes_Plan_WorkOrder where ID={0}";
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
        public bool Save(Mes_Plan_WorkOrder obj)
        {
            int result = -1;
            if(obj.ID>0)
            {
                result = this.CurDbSession.Update<Mes_Plan_WorkOrder>(obj);
            }
            else
            {
                result = this.CurDbSession.Insert<Mes_Plan_WorkOrder>(obj);
            }
            if(result<0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 工单号产生
        /// </summary>
        /// <returns></returns>
        public string GetWorkOrderNo()
        {
            return "WK" + DateTime.Now.ToString("yyyyMMddhhmmsss");
        }

        /// <summary>
        /// 下达计划
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool GenerateWorkOrder(Mes_Plan_WorkOrder obj)
        {
            int result = -1;
            ProcSection proc = this.CurDbSession.FromProc("")
                .AddInParameter("@WorkOrderType", System.Data.DbType.String, 50,obj.WorkOrderType)
                .AddInParameter("@WorkOrderNumber", System.Data.DbType.String, 50,obj.WorkOrderNumber)
                .AddInParameter("@MaterialProNo", System.Data.DbType.String, 50,obj.MaterialProNo)
                .AddInParameter("@MaterialCode", System.Data.DbType.String, 50,obj.MaterialCode)
                .AddInParameter("@Version", System.Data.DbType.String, 20,obj.Version)
                .AddInParameter("@WorkNum", System.Data.DbType.Int32, obj.WorkNum)
                .AddOutParameter("@ErrMsg", System.Data.DbType.String, 100)
                .AddOutParameter("@OptionResult", System.Data.DbType.Int32, 1);
            proc.ExecuteNonQuery();
            Dictionary<string, object> returnValue = proc.GetReturnValues();

            Int32 recout = -1;
            string sMsg = string.Empty;

            foreach(KeyValuePair<string,object> kv in returnValue)
            {
                if(kv.Key=="OptionResult")
                {
                    recout = Convert.ToInt32(kv.Value);
                }
                if (kv.Key == "ErrMsg")
                {
                    sMsg = Convert.ToString(kv.Value);
                }
            }
            if(recout<0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 强制关闭工单
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool QzCloseWorkOrder(Mes_Plan_WorkOrder obj)
        {
            Mes_Plan_WorkOrder entity = new Mes_Plan_WorkOrder();
            entity = MesPlanWorkOrderDao.Instance.Find(obj);
            int result = -1;
            result = this.CurDbSession.Update<Mes_Plan_WorkOrder>(entity);
            if(result<0)
            {
                return false;
            }
            return true;
        }
        #endregion 主信息


    }
}
