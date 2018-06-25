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
    public class MesTecDeviceInfoDao : BaseDao
    {
        public static MesTecDeviceInfoDao _Instance = null;

        public static MesTecDeviceInfoDao Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MesTecDeviceInfoDao();
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
        public Mes_Tec_DeviceInfo GetDeviceInfo(Mes_Tec_DeviceInfo obj)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            string sql = "select top 1 * from Mes_Tec_DeviceInfo where 1=1 ";
            if (!string.IsNullOrEmpty(obj.DeviceCode))
            {
                sql = sql + string.Format(" AND DeviceCode='{0}'", obj.DeviceCode);
            }
            return this.CurDbSession.FromSql(sql.ToString()).ToFirstDefault<Mes_Tec_DeviceInfo>();
        }

        public List<Mes_Tec_DeviceInfo> ExpiredDetail(ref PagerBase pager)
        {
            string sql = @"select * from Mes_Tec_DeviceInfo WHERE ValidEndTime<=Convert(datetime,convert(varchar(10),getdate(),20)+' 00:00:00')";            
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
            return this.CurDbSession.FromSql(cmdPageSql).ToList<Mes_Tec_DeviceInfo>();
        }

        public List<Mes_Tec_DeviceInfo> FindByPage(Mes_Tec_DeviceInfo obj, ref PagerBase pager)
        {
            string sql = @"select * from Mes_Tec_DeviceInfo WHERE   1=1 ";

            if (!string.IsNullOrEmpty(obj.DeviceCode))
            {
                sql += string.Format(" AND DeviceCode='{0}'", obj.DeviceCode);
            }

            if (!string.IsNullOrEmpty(obj.DeviceName))
            {
                sql += string.Format(" AND DeviceTypeName Like '%{0}%'", obj.DeviceName);
            }
            if (!string.IsNullOrEmpty(obj.DeviceType))
            {
                sql += string.Format(" AND DeviceType = '{0}'", obj.DeviceType);
            }
            sql += " AND DeviceStatus=" + obj.DeviceStatus.ToString().Trim();
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
            return this.CurDbSession.FromSql(cmdPageSql).ToList<Mes_Tec_DeviceInfo>();
        }
      

        #endregion 查询和分页方法

       /// <summary>
       /// 保存用户
       /// </summary>
       /// <param name="obj"></param>
       /// <returns></returns>
        public bool Save(Mes_Tec_DeviceInfo obj)
        {
            int result = -1;
            if(obj.ID>0)
            {
                //修改 
                result = this.CurDbSession.Update<Mes_Tec_DeviceInfo>(obj);
            }
            else
            {
                result = this.CurDbSession.Insert<Mes_Tec_DeviceInfo>(obj);
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
            string sql = "delete from Mes_Tec_DeviceInfo where ID={0}";
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
