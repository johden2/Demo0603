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
    public class MesSysRoleDao : BaseDao
    {
        public static MesSysRoleDao _Instance = null;

        public static MesSysRoleDao Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MesSysRoleDao();
                }
                return _Instance;
            }
        }


        #region 查询和分页方法

        public List<Mes_Sys_Role> FindByPage(Mes_Sys_Role obj, ref PagerBase pager)
        {
            var query = this.CurDbSession.From<Mes_Sys_Role>();
            var qwhere = new Where<Mes_Sys_Role>();
            qwhere.And(p => p.RecordStatus == YesNoType.Yes);
            if (obj.ID > 0)
            {
                qwhere.And(p => p.ID == obj.ID);
            }
            if (!string.IsNullOrEmpty(obj.RoleName))
            {
                qwhere.And(p =>p.RoleName.IsNotNull() && p.RoleName.Contains(obj.RoleName));
            }
            query = query.Where(qwhere);
            //1.查询总记录数
            pager.TotalItemCount = query.Count();
            //2.返回当前页的记录数
            query.OrderByDescending(P => P.CreatedTime);  //需要自定义设置
            return query.Page(pager.PageSize, pager.CurrentPageIndex).ToList();
        }

        public List<Mes_Sys_Role> FindByCond(Mes_Sys_Role obj)
        {
            var qwhere = new Where<Mes_Sys_Role>();
            if (obj.ID > 0)
            {
                qwhere.And(p => p.ID == obj.ID);
            }
            if (!string.IsNullOrEmpty(obj.RoleName))
            {
                qwhere.And(p => p.RoleName.Contains(obj.RoleName));
            }
            return this.CurDbSession.From<Mes_Sys_Role>().Where(qwhere).OrderByDescending(p => p.RoleName).ToList();
        }

        #endregion 查询和分页方法

    }
}
