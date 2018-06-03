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
    public class MesSysRoleMapDao : BaseDao
    {
        public static MesSysRoleMapDao _Instance = null;

        public static MesSysRoleMapDao Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MesSysRoleMapDao();
                }
                return _Instance;
            }
        }


        #region 查询和分页方法

        public List<Mes_Sys_ModuleItem> FindByCond(Mes_Sys_RoleMap obj)
        {
            string sql = @"SELECT T3.*,T1.ButtonRightFlag FROM Mes_Sys_ModuleItem T3 
                        LEFT JOIN 
                        (SELECT DISTINCT Sys_ModuleItem_ID,ButtonRightFlag FROM Mes_Sys_RoleMap WHERE Sys_Role_ID = {0}) T1 ON T1.Sys_ModuleItem_ID = T3.ID
                        WHERE 1=1";
            sql = string.Format(sql, obj.Sys_Role_ID);
            if (obj.UseType > 0)
            {
                sql += string.Format(" AND T3.UseType = {0}", obj.UseType);
            }

            return this.CurDbSession.FromSql(sql).ToList<Mes_Sys_ModuleItem>();
        }

        public List<Mes_Sys_ModuleItem> GetTree(Mes_Sys_RoleMap obj)
        {
            List<Mes_Sys_ModuleItem> list = this.FindByCond(obj);
            if (list == null || list.Count == 0) return null;

            List<Mes_Sys_ModuleItem> oneList = list.Where(p => p.Level == 1).ToList();
            foreach (var item in oneList)
            {
                GetChildNodes(list, item);
            }

            return list;
        }

        public void GetChildNodes(List<Mes_Sys_ModuleItem> list, Mes_Sys_ModuleItem node)
        {
            int level = node.Level.Value + 1;
            List<Mes_Sys_ModuleItem> childList = list.Where(p => p.Level == level && !string.IsNullOrEmpty(p.ModuleCode) 
                && p.ModuleCode.StartsWith(node.ModuleCode)).ToList();
            if (childList == null || childList.Count == 0) return;

            node.IsParent = 1;
            foreach (var item in childList)
            {
                item.ParentID = node.ID;
                GetChildNodes(list, item);
            }
        }
      

        #endregion 查询和分页方法

        /// <summary>
        /// 保存用户
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool SaveExt(List<Mes_Sys_RoleMap> list,int roleId)
        {
            int result = -1;

            try
            {
                //1.先删除所有的角色记录
                string sql = string.Format("DELETE FROM Mes_Sys_RoleMap WHERE Sys_Role_ID = {0}",roleId);
                this.CurDbSession.FromSql(sql).ExecuteNonQuery();

                //2.写入新的权限记录
                this.CurDbSession.Insert<Mes_Sys_RoleMap>(list);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

            return true;
        }

    }
}
