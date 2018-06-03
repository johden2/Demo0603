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
    /// 权限配置
    /// </summary>
    public partial class SysManagementController
    {
        public ActionResult RightConfig()
        {
            Mes_Sys_RoleMap obj = new Mes_Sys_RoleMap();
            obj.Sys_Role_ID = TConvertHelper.FormatDBInt(Request.Params["RoleID"]);
            return View(obj);
        }

        /// <summary>
        /// 查主键或自增Id进行查询
        /// </summary>
        /// <param name="Id">主键或自增Id</param>
        /// <returns>JSON数据</returns>
        [HttpGet]
        public ActionResult RightConfig_FindTree(int RoleID)
        {
            Mes_Sys_RoleMap obj = new Mes_Sys_RoleMap();
            obj.Sys_Role_ID = RoleID;
            obj.UseType = 2;
            var list = MesSysRoleMapDao.Instance.GetTree(obj);
            var result = list.Select(sub => new
            {
                id = sub.ID,
                pId = sub.ParentID,
                name = sub.MenuName,
                IsParent = sub.IsParent, //是否父节点,前后台好做处理
                ButtonRightFlag = sub.ButtonRightFlag
            });

            return Json(new { IsSuccess = true, Message = JsonHelper.SerializeObject(result) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RightConfig_Save(string NodeItems, int RoleID)
        {
            if (string.IsNullOrEmpty(NodeItems))
            {
                return Json(new { IsSuccess = false, Message = "菜单信息有误！" });
            }

            DateTime now = DateTime.Now;
            List<Mes_Sys_RoleMap> list = JsonHelper.DeserializeJsonToList<Mes_Sys_RoleMap>(NodeItems);
            foreach (var item in list)
            {
                item.Sys_Role_ID = RoleID;
                item.RecordStatus = YesNoType.Yes;
                item.Creater = base.CurUser.UserName;
                item.CreatedTime = now;
            }

            MesSysRoleMapDao.Instance.SaveExt(list, RoleID);

            return Json(new { IsSuccess = true, Message = "保存成功" });
        }

    }
}
