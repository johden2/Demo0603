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
    public class MesSysModuleItemDao : BaseDao
    {
        public static MesSysModuleItemDao _Instance = null;

        public static MesSysModuleItemDao Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MesSysModuleItemDao();
                }
                return _Instance;
            }
        }


        #region 查询和分页方法

        public List<Mes_Sys_ModuleItem> FindByCondExt(Mes_Sys_ModuleItem obj, bool isAdmin, string userName)
        {
            string sql = string.Empty;
            if (isAdmin)
            {
                sql = @"SELECT * FROM dbo.Mes_Sys_ModuleItem T1 WITH(NOLOCK) WHERE T1.NotShow = 0";
            }
            else
            {
                sql = string.Format(@"SELECT T1.* FROM dbo.Mes_Sys_ModuleItem T1 WITH(NOLOCK) 
                        INNER JOIN Mes_Sys_RoleMap T2 WITH(NOLOCK)  ON T1.ID = T2.Sys_ModuleItem_ID
                        INNER JOIN Mes_Sys_Map T3 WITH(NOLOCK)  ON T3.RoleID = T2.Sys_Role_ID
                        WHERE T1.NotShow = 0 AND T3.UserID = '{0}'", userName);
            }

            if (obj.UseType > 0)
            {
                sql += string.Format(" AND ISNULL(T1.UseType,0) = {0}", obj.UseType);
            }
            if (obj.Level > 0)
            {
                sql += string.Format(" AND ISNULL(T1.Level,0) = {0}", obj.Level);
            }
            if (obj.IsParent > 0 && !string.IsNullOrEmpty(obj.ModuleCode))
            {
                sql += string.Format(" AND T1.ModuleCode LIKE '{0}%'", obj.ModuleCode);
            }

            var list = this.CurDbSession.FromSql(sql).ToList<Mes_Sys_ModuleItem>();
            if (list != null && list.Count > 0)
            {
                return list.OrderBy(p => p.SortNo).ToList();
            }

            return list;
        }

        #endregion 查询和分页方法

    }
}
