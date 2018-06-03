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
        public ActionResult OrganizationMgt()
        {
            return View();
        }

        public ActionResult OrganizationMgt_FindTree(Mes_Sys_Organization obj)
        {
            string stationTree = MesSysOrganizationDao.Instance.GetTree(obj);
            return Json(new { IsSuccess = true, Message = stationTree }, JsonRequestBehavior.AllowGet);
        }

        public string OrganizationMgt_GetList(Mes_Sys_Organization obj)
        {
            string result = MesSysOrganizationDao.Instance.GetList();
            //return Json(new { IsSuccess = true, Message = stationTree });
            return result;
        }

        public ActionResult OrganizationMgt_Save(Mes_Sys_Organization obj)
        {
            if (string.IsNullOrEmpty(obj.OrgName))
            {
                return Json(new { IsSuccess = false, Message = "部门名称不能为空！" });
            }
            if (obj.ID <= 0)
            {
                obj.RecordStatus = YesNoType.Yes;
                obj.Creater = base.CurUser.UserId;
                obj.CreatedTime = DateTime.Now;
            }

            MesSysOrganizationDao.Instance.Save(obj);
            return Json(new { IsSuccess = true, Message = "操作成功！" });
        }

        public ActionResult OrganizationMgt_Delete(int ID)
        {
            if (ID <= 0)
            {
                return Json(new { IsSuccess = false, Message = "选择的记录有误，请刷新后重试！" });
            }
            Mes_Sys_Organization obj = MesSysOrganizationDao.Instance.Find<Mes_Sys_Organization, int>(ID);
            if (obj == null)
            {
                return Json(new { IsSuccess = false, Message = "角色信息有误！" });
            }

            obj.RecordStatus = YesNoType.No;
            MesSysOrganizationDao.Instance.Save<Mes_Sys_Organization>(obj);
            return Json(new { IsSuccess = true, Message = "" });
        }

    }
}
