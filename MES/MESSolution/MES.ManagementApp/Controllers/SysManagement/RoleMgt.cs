using Sys.Dao;
using Sys.Model;
using Sys.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MES.ManagementApp.Controllers
{
    /// <summary>
    /// 系统管理
    /// </summary>
    public partial class SysManagementController
    {
        public ActionResult RoleMgt()
        {
            return View();
        }

        public ActionResult RoleMgt_FindByPage(Mes_Sys_Role obj, int page, int rows)
        {
            var pager = new PagerBase() { CurrentPageIndex = page, PageSize = rows };
            var list = MesSysRoleDao.Instance.FindByPage(obj, ref pager);
            return Json(new { total = pager.TotalItemCount, rows = list }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 角色查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public ActionResult RoleMgt_Select(int page, int rows, string q)
        {
            List<Mes_Sys_Role> list = null;
            Mes_Sys_Role obj = new Mes_Sys_Role();
            if (!string.IsNullOrEmpty(q))
            {
                obj.RoleName = q;
            }
            list = MesSysRoleDao.Instance.FindByCond(obj);
            if (list == null)
            {
                list = new List<Mes_Sys_Role>();
            }

            var result = new { rows = list, total = list.Count };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RoleMgt_Save(Mes_Sys_Role obj)
        {
            if (string.IsNullOrEmpty(obj.RoleName))
            {
                return Json(new { IsSuccess = false, Message = "角色名称不能为空！" });
            }
            if (obj.ID <= 0)
            {
                obj.RecordStatus = YesNoType.Yes;
                obj.Creater = base.CurUser.UserId;
                obj.CreatedTime = DateTime.Now;
            }

            MesSysRoleDao.Instance.Save(obj);
            return Json(new { IsSuccess = true, Message = "操作成功！" });
        }

        public ActionResult RoleMgt_Delete(int ID)
        {
            if (ID <= 0)
            {
                return Json(new { IsSuccess = false, Message = "选择的记录有误，请刷新后重试！" });
            }
            Mes_Sys_Role obj = MesSysRoleDao.Instance.Find<Mes_Sys_Role, int>(ID);
            if (obj == null)
            {
                return Json(new { IsSuccess = false, Message = "角色信息有误！" });
            }

            obj.RecordStatus = YesNoType.No;
            MesSysRoleDao.Instance.Save<Mes_Sys_Role>(obj);
            return Json(new { IsSuccess = true, Message = "" });
        }

    }
}
