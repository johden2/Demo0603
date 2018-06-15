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
    /// 配套明细表管理
    /// </summary>
    public partial class ProcessManagementController
    {
        public ActionResult ProcessBomMgt()
        {
            return View();
        }

        /// <summary>
        /// 获取主表产品树
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string ProcessBomMgt_FindTree(Mes_Tec_ProcessBom obj)
        {
            string result = MesTecProcessBomDao.Instance.GetTree(obj);
            return result;
        }

        public ActionResult ProcessBomMgt_FindByPage(Mes_Tec_ProcessBom obj, int page, int rows)
        {
            var pager = new PagerBase() { CurrentPageIndex = page, PageSize = rows };
            var list = MesTecProcessBomDao.Instance.FindByPage(obj, ref pager);
            return Json(new { total = pager.TotalItemCount, rows = list }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProcessBomMgt_SaveBom(Mes_Tec_ProcessBom obj)
        {
            if (string.IsNullOrEmpty(obj.MaterialProNo))
            {
                return Json(new { IsSuccess = false, Message = "【产品编码】不能为空！" });
            }
            if (string.IsNullOrEmpty(obj.MaterialCode))
            {
                return Json(new { IsSuccess = false, Message = "【产品简称】不能为空！" });
            }
            if (string.IsNullOrEmpty(obj.Version))
            {
                return Json(new { IsSuccess = false, Message = "【产品版本】不能为空！" });
            }

            Mes_Tec_ProcessBom searchObj = new Mes_Tec_ProcessBom();
            searchObj.MaterialProNo = obj.MaterialProNo;
            searchObj.Version = obj.Version;
            Mes_Tec_ProcessBom resultObj = MesTecProcessBomDao.Instance.FindExt(searchObj);
            if (resultObj == null)
            {
                resultObj = obj;
                resultObj.ID = 0;
                resultObj.Creater = base.CurUser.UserId;
                resultObj.CreatedTime = DateTime.Now;
            }
            else
            {
                resultObj.Memo = obj.Memo;
                resultObj.MaterialCode = obj.MaterialCode;
            }

            MesTecProcessBomDao.Instance.Save(resultObj);
            return Json(new { IsSuccess = true, Message = "操作成功！" });
        }
        public ActionResult ProcessBomMgt_Delete(Mes_Tec_ProcessBom obj)
        {
            if (string.IsNullOrEmpty(obj.MaterialProNo))
            {
                return Json(new { IsSuccess = false, Message = "【产品编码】有误！" });
            }
            if (string.IsNullOrEmpty(obj.Version))
            {
                return Json(new { IsSuccess = false, Message = "【产品版本】有误！" });
            }

            Mes_Tec_ProcessBom searchObj = new Mes_Tec_ProcessBom();
            searchObj.MaterialProNo = obj.MaterialProNo;
            searchObj.Version = obj.Version;
            Mes_Tec_ProcessBom resultObj = MesTecProcessBomDao.Instance.FindExt(searchObj);
            if (resultObj == null || resultObj.ID <=0)
            {
                return Json(new { IsSuccess = false, Message = "产品信息有误，请刷新后重试！" });
            }

            MesTecProcessBomDao.Instance.DeleteExt(resultObj);
            return Json(new { IsSuccess = true, Message = "操作成功！" });
        }


        public ActionResult ProcessBomMgt_Find(Mes_Tec_ProcessBom obj)
        {
            if (string.IsNullOrEmpty(obj.MaterialProNo))
            {
                return Json(new { IsSuccess = false, Message = "【产品编码】有误！" });
            }
            if (string.IsNullOrEmpty(obj.Version))
            {
                return Json(new { IsSuccess = false, Message = "【产品版本】有误！" });
            }
            Mes_Tec_ProcessBom result = MesTecProcessBomDao.Instance.FindExt(obj);
            if (result == null)
            {
                return Json(new { IsSuccess = false, Message = "信息不存在，请刷新后重试！" });
            }

            return Json(new { IsSuccess = true, Message = JsonHelper.SerializeObject(result) }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ProcessBomMgt_SaveBomItem(Mes_Tec_ProcessBom obj)
        {
            if (string.IsNullOrEmpty(obj.MaterialProNo))
            {
                return Json(new { IsSuccess = false, Message = "【产品编码】不能为空！" });
            }
            if (string.IsNullOrEmpty(obj.MaterialCode))
            {
                return Json(new { IsSuccess = false, Message = "【产品简称】不能为空！" });
            }
            if (string.IsNullOrEmpty(obj.Version))
            {
                return Json(new { IsSuccess = false, Message = "【产品版本】不能为空！" });
            }
            if (string.IsNullOrEmpty(obj.SubMaterialProNo))
            {
                return Json(new { IsSuccess = false, Message = "【物料编码】不能为空！" });
            }
            if (string.IsNullOrEmpty(obj.SubMaterialCode))
            {
                return Json(new { IsSuccess = false, Message = "【物料简称】不能为空！" });
            }
            if (obj.Num <=0)
            {
                return Json(new { IsSuccess = false, Message = "【数量】不能为空！" });
            }
            if (string.IsNullOrEmpty(obj.Unit))
            {
                return Json(new { IsSuccess = false, Message = "【单位】不能为空！" });
            }

            Mes_Tec_ProcessBom searchObj = new Mes_Tec_ProcessBom();
            searchObj.MaterialProNo = obj.MaterialProNo;
            searchObj.Version = obj.Version;
            Mes_Tec_ProcessBom resultObj = MesTecProcessBomDao.Instance.FindExt(searchObj);
            if (resultObj == null)
            {
               return Json(new { IsSuccess = false, Message = "产品信息有误，请刷新后重试！" });
            }

            obj.Creater = base.CurUser.UserId;
            obj.CreatedTime = DateTime.Now;

            MesTecProcessBomDao.Instance.SaveBomItem(obj, resultObj);
            return Json(new { IsSuccess = true, Message = "操作成功！" });
        }

        public ActionResult ProcessBomMgt_DeleteBomItem(Mes_Tec_ProcessBom obj)
        {
            if (obj.ItemID <=0)
            {
                return Json(new { IsSuccess = false, Message = "传入的参数有误！" });
            }

            Mes_Tec_ProcessBomItem itemObj = MesTecProcessBomDao.Instance.Find<Mes_Tec_ProcessBomItem, int>(obj.ItemID);
            if (itemObj == null)
            {
                return Json(new { IsSuccess = false, Message = "物料信息有误，请刷新后重试！" });
            }

            MesTecProcessBomDao.Instance.Delete<Mes_Tec_ProcessBomItem>(itemObj);
            return Json(new { IsSuccess = true, Message = "操作成功！" });
        }

    }
}
