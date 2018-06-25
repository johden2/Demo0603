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
    public class MesTecDeviceOutRecordDao : BaseDao
    {
        public static MesTecDeviceOutRecordDao _Instance = null;

        public static MesTecDeviceOutRecordDao Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MesTecDeviceOutRecordDao();
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
        public Mes_Tec_DeviceOutRecord GetDeviceOutRecord(Mes_Tec_DeviceOutRecord obj)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            string sql = "select top 1 * from Mes_Tec_DeviceOutRecord where 1=1 ";
            if (obj.ID != 0)
            {
                sql = sql + string.Format(" AND ID='{0}'", obj.ID);
            }

            return this.CurDbSession.FromSql(sql.ToString()).ToFirstDefault<Mes_Tec_DeviceOutRecord>();
        }

        public List<Mes_Tec_DeviceOutRecord> FindByPage(Mes_Tec_DeviceOutRecord obj, ref PagerBase pager)
        {
            string sql = @"select * from Mes_Tec_DeviceOutRecord WHERE   1=1 ";

            if (!string.IsNullOrEmpty(obj.DeviceCode))
            {
                sql += string.Format(" AND DeviceCode = '{0}'", obj.DeviceCode);
            }
            if (obj.OrgID != 0)
            {
                sql += string.Format(" AND OrgID = '{0}'", obj.OrgID);
            }
            sql += string.Format(" AND OutDate>='{0}'", obj.OutDateStart);
            sql += string.Format(" AND OutDate<'{0}'", obj.OutDateEnd);
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
            return this.CurDbSession.FromSql(cmdPageSql).ToList<Mes_Tec_DeviceOutRecord>();
        }
      

        #endregion 查询和分页方法

       /// <summary>
       /// 保存用户
       /// </summary>
       /// <param name="obj"></param>
       /// <returns></returns>
        public bool Save(Mes_Tec_DeviceOutRecord obj)
        {
            int result = -1;
            if(obj.ID>0)
            {
                //修改 
                result = this.CurDbSession.Update<Mes_Tec_DeviceOutRecord>(obj);
            }
            else
            {
                result = this.CurDbSession.Insert<Mes_Tec_DeviceOutRecord>(obj);
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
            string sql = "delete from Mes_Tec_DeviceOutRecord where ID={0}";
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
