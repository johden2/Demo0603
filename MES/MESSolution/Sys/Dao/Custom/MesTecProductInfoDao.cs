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
    public class MesTecProductInfoDao : BaseDao
    {
        public static MesTecProductInfoDao _Instance = null;

        public static MesTecProductInfoDao Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MesTecProductInfoDao();
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
        public Mes_Tec_ProductInfo GetProductInfo(Mes_Tec_ProductInfo obj)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            string sql = "select top 1 * from Mes_Tec_ProductInfo where 1=1 ";
            if (!string.IsNullOrEmpty(obj.MaterialProNo))
            {
                sql = sql + string.Format(" AND MaterialProNo='{0}'", obj.MaterialProNo);
            }
            if (!string.IsNullOrEmpty(obj.MaterialCode))
            {
                sql = sql + string.Format(" AND MaterialCode='{0}'", obj.MaterialCode);
            }
            if (!string.IsNullOrEmpty(obj.MaterialClass))
            {
                sql = sql + string.Format(" AND MaterialClass='{0}'", obj.MaterialClass);
            }
            return this.CurDbSession.FromSql(sql.ToString()).ToFirstDefault<Mes_Tec_ProductInfo>();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public List<Mes_Tec_ProductInfo> FindByPage(Mes_Tec_ProductInfo obj, ref PagerBase pager)
        {
            string sql = @"select T1.*,T2.StockName as Show_StockCode,T3.AlibraryName as Show_AlibraryCode from Mes_Tec_ProductInfo T1 
                        Left join Mes_Sys_Stock T2 ON T1.StockCode=T2.StockCode 
                        left join Mes_Sys_Alibrary T3 ON T1.AlibrayCode=T3.AlibraryCode 
                        WHERE   1=1 ";

            if (!string.IsNullOrEmpty(obj.MaterialProNo))
            {
                sql += string.Format(" AND T1.MaterialProNo Like '%{0}%'", obj.MaterialProNo);
            }

            if (!string.IsNullOrEmpty(obj.MaterialCode))
            {
                sql += string.Format(" AND T1.MaterialCode Like '%{0}%'", obj.MaterialCode);
            }
            if (!string.IsNullOrEmpty(obj.MaterialClass) && obj.MaterialClass != "-1")
            {
                sql += string.Format(" AND T1.MaterialClass Like '%{0}%'", obj.MaterialClass);
            }
            if (obj.MaterialStatus > 0)
            {
                sql += string.Format(" AND T1.MaterialStatus={0}", obj.MaterialStatus);
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
            return this.CurDbSession.FromSql(cmdPageSql).ToList<Mes_Tec_ProductInfo>();
        }
      

        #endregion 查询和分页方法

       /// <summary>
       /// 保存用户
       /// </summary>
       /// <param name="obj"></param>
       /// <returns></returns>
        public bool Save(Mes_Tec_ProductInfo obj)
        {
            int result = -1;
            if(obj.ID>0)
            {
                //修改 
                result = this.CurDbSession.Update<Mes_Tec_ProductInfo>(obj);
            }
            else
            {
                result = this.CurDbSession.Insert<Mes_Tec_ProductInfo>(obj);
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
            string sql = "delete from Mes_Tec_ProductInfo where ID={0}";
            sql = string.Format(sql, Id);
            int result = this.CurDbSession.FromSql(sql).ExecuteNonQuery();                       
            if(result>0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 导入物料信息
        /// </summary>
        /// <param name="list"></param>
        /// <param name="resultList"></param>
        /// <returns></returns>
        public bool Import(List<Mes_Tec_ProductInfo> list, IList<ImportMessageModel> resultList)
        {
            try
            {
                int result = this.CurDbSession.Insert<Mes_Tec_ProductInfo>(list);
                return true;
            }
            catch (Exception ex)
            {
                resultList.Add(new ImportMessageModel { RowData = "导入失败", RowMessage = "导入出错，错误信息：" + ex.Message });
            }
            return false;
        }



    }
}
