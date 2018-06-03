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
    public class MesSysUserDao : BaseDao
    {
        public static MesSysUserDao _Instance = null;

        public static MesSysUserDao Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MesSysUserDao();
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
        public Mes_Sys_User GetUser(Mes_Sys_User obj)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            string sql = "select top 1 * from Mes_Sys_User where 1=1 ";
            if (!string.IsNullOrEmpty(obj.UserID))
            {
                sql = sql + string.Format(" AND UserID='{0}'", obj.UserID);
            }
            if (!string.IsNullOrEmpty(obj.Pass))
            {
                sql = sql + string.Format(" AND Pass='{0}'", obj.Pass);
            }

            return this.CurDbSession.FromSql(sql.ToString()).ToFirstDefault<Mes_Sys_User>();
        }

        public List<Mes_Sys_User> FindByPage(Mes_Sys_User obj, ref PagerBase pager)
        {
            string sql = @"SELECT T1.*,T3.OrgName,T4.RoleName FROM Mes_Sys_User T1 WITH(NOLOCK) 
                            LEFT JOIN Mes_Sys_Map T2 WITH(NOLOCK)  ON T1.UserID = T2.UserID
                            LEFT JOIN Mes_Sys_Role T4 WITH(NOLOCK)  ON T2.RoleID = T4.ID
                            LEFT JOIN dbo.Mes_Sys_Organization T3 WITH(NOLOCK)  ON T1.OrgID = T3.ID
                    WHERE   1=1 ";

            if (!string.IsNullOrEmpty(obj.UserID))
            {
                sql += string.Format(" AND T1.UserID Like '%{0}%'", obj.UserID);
            }

            if (!string.IsNullOrEmpty(obj.UserName))
            {
                sql += string.Format(" AND T1.UserName Like '%{0}%'", obj.UserName);
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
            return this.CurDbSession.FromSql(cmdPageSql).ToList<Mes_Sys_User>();
        }
      

        #endregion 查询和分页方法

       /// <summary>
       /// 保存用户
       /// </summary>
       /// <param name="obj"></param>
       /// <returns></returns>
        public bool SaveExt(Mes_Sys_User obj)
        {
            int result = -1;

            try
            {
                using (DbTrans trans = this.CurDbSession.BeginTransaction())
                {
                    //1.保存用户
                    if (obj.ID > 0)
                    {
                        result = trans.Update<Mes_Sys_User>(obj);
                    }
                    else
                    {
                        result = trans.Insert<Mes_Sys_User>(obj);
                    }
                   
                    if (result <= 0)
                    {
                        trans.Rollback();
                        return false;
                    }

                    //2.保存用户角色、部门关系表
                    string sql = @"DELETE FROM Mes_Sys_Map WHERE UserID ='{0}';
                        INSERT INTO Mes_Sys_Map(UserID,RoleID,OrgID,RecordStatus,Creater,CreatedTime)VALUES('{0}',{1},{2},1,'{3}',GETDATE())";
                    sql = string.Format(sql, obj.UserID, obj.RoleID, obj.OrgID, obj.Creater);
                    result = trans.FromSql(sql).ExecuteNonQuery();
                    if (result <= 0)
                    {
                        trans.Rollback();
                        return false;
                    }

                    //提交事务
                    trans.Commit();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

            return true;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DeleteList(string ids)
        {
            string sql = "DELETE FROM Mes_Sys_User WHERE ID IN ({0})";
            sql = string.Format(sql, ids);
            int result = this.CurDbSession.FromSql(sql).ExecuteNonQuery();
            return result > 0;
        }




    }
}
