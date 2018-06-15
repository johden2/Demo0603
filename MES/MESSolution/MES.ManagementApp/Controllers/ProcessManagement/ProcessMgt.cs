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
    /// 工艺管理
    /// </summary>
    public partial class ProcessManagementController
    {
        /// <summary>
        /// 返回视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Process()
        {
            return View();
        }

        public ActionResult ProcessMgt_FindTree(Mes_Tec_Process obj)
        {
            string stationTree = MesTecProcessDao.Instance.GetTree(obj);
            return Json(new { IsSuccess = true, Message = stationTree });
        }

        /// <summary>
        /// 保存记录
        /// </summary>
        /// <returns></returns>
        public ActionResult ProcessMgt_Save(Mes_Tec_Process obj)
        {
            string sMessage = string.Empty;
            //判断数据有效性
            if (string.IsNullOrEmpty(obj.ProcessCode))
            {
                sMessage = "工序编码不可为空";
                return Json(new { IsSuccess = false, Message = sMessage }, JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(obj.Name))
            {
                sMessage = "工序名称不可为空";
                return Json(new { IsSuccess = false, Message = sMessage }, JsonRequestBehavior.AllowGet);
            }
            if (obj.ID > 0)
            {
                obj.Modifier = base.CurUser.UserId;
                obj.ModifiedTime = DateTime.Now;
            }
            else
            {
                obj.Creater = base.CurUser.UserId;
                obj.CreatedTime = DateTime.Now;
            }
            bool result = MesTecProcessDao.Instance.Save(obj);
            if (!result)
            {
                sMessage = "保存失败";
                return Json(new { IsSuccess = false, Message = sMessage }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { IsSuccess = true, Message = sMessage }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <returns></returns>
        public ActionResult ProcessMgt_Delete(string Ids)
        {
            string sMessage = string.Empty;
            bool result = MesTecProcessDao.Instance.Delete(Ids);
            if (!result)
            {
                sMessage = "删除失败";
                return Json(new { IsSuccess = false, Message = sMessage });
            }
            return Json(new { IsSuccess = true, Message = sMessage });
        }

        public ActionResult ProcessMgt_FindByPage(Mes_Tec_Process obj, int page, int rows)
        {
            var pager = new PagerBase() { CurrentPageIndex = page, PageSize = rows };
            var list = MesTecProcessDao.Instance.FindByPage(obj, ref pager);
            return Json(new { total = pager.TotalItemCount, rows = list });
        }

        public string ProcessMgt_GetList(Mes_Tec_Process obj)
        {
            string result = MesTecProcessDao.Instance.GetList();
            return result;
        }

    }
}
