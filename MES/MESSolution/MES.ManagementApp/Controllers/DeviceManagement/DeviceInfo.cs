using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Sys.Dao;
using Sys.Model;
using Sys.Tools;


namespace MES.ManagementApp.Controllers
{
    public partial class DeviceManagementController
    {
        public ActionResult DeviceInfo()
        {
            return View();
        }

        /// <summary>
        /// 保存记录
        /// </summary>
        /// <returns></returns>
        public ActionResult DeviceInfo_Save(Mes_Tec_DeviceInfo obj)
        {
            string sMessage = string.Empty;
            //判断数据有效性
            if(string.IsNullOrEmpty(obj.DeviceCode))
            {
                sMessage = "工装夹具编号不可为空";
                return Json(new { IsSuccess = false, Message = sMessage },JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(obj.DeviceName))
            {
                sMessage = "工装夹具名称不可为空";
                return Json(new { IsSuccess = false, Message = sMessage },JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(obj.DeviceType))
            {
                sMessage = "工装夹具类型不可为空";
                return Json(new { IsSuccess = false, Message = sMessage }, JsonRequestBehavior.AllowGet);
            }

            if (obj.ID > 0)
            {
                
            }
            else
            {
                obj.Creater = base.CurUser.UserId;
                obj.CreatedTime = DateTime.Now;
            }
            bool result = MesTecDeviceInfoDao.Instance.Save(obj);
            if(!result)
            {
                sMessage = "保存失败";
                return Json(new { IsSuccess = false, Message =sMessage },JsonRequestBehavior.AllowGet);
            }
            return Json(new { IsSuccess = true, Message = sMessage },JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <returns></returns>
        public ActionResult DeviceInfo_Delete(string Ids)
        {            
            string sMessage = string.Empty;
            bool result = MesTecDeviceInfoDao.Instance.Delete(Ids);
            if(!result)
            {
                sMessage = "删除失败";
                return Json(new { IsSuccess = false, Message = sMessage });
            }
            return Json(new { IsSuccess = true, Message = sMessage });
        }

        public ActionResult DeviceInfo_FindByPage(Mes_Tec_DeviceInfo obj, int page, int rows)
        {
            var pager = new PagerBase() { CurrentPageIndex = page, PageSize = rows };
            var list = MesTecDeviceInfoDao.Instance.FindByPage(obj, ref pager);
            return Json(new { total = pager.TotalItemCount, rows = list });
        }

        public ActionResult DeviceInfo_ExpiredDetail(int page, int rows)
        {
            var pager = new PagerBase() { CurrentPageIndex = page, PageSize = rows };
            var list = MesTecDeviceInfoDao.Instance.ExpiredDetail(ref pager);
            return Json(new { total = pager.TotalItemCount, rows = list });
        }


        /// <summary>
        /// 夹具停用
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public ActionResult DeviceInfo_DisableDevice(Mes_Tec_DeviceInfo obj)
        {
            string sMessage = string.Empty;
            obj.DeviceStatus = DeviceStatus.TY;
            bool result = MesTecDeviceInfoDao.Instance.Save(obj);
            if (!result)
            {
                sMessage = "操作失败";
                return Json(new { IsSuccess = false, Message = sMessage }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { IsSuccess = true, Message = sMessage }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 夹具报废
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public ActionResult DeviceInfo_DiscardDevice(Mes_Tec_DeviceInfo obj)
        {
            string sMessage = string.Empty;
            obj.DeviceStatus = DeviceStatus.BF;
            bool result = MesTecDeviceInfoDao.Instance.Save(obj);
            if (!result)
            {
                sMessage = "操作失败";
                return Json(new { IsSuccess = false, Message = sMessage }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { IsSuccess = true, Message = sMessage }, JsonRequestBehavior.AllowGet);
        }
    }
}