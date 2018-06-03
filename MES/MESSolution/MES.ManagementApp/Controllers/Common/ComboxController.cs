using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.EnterpriseServices;

using System.Web.Script.Serialization;
using Sys.Model;
using Sys.Dao;


namespace MES.ManagementApp.Controllers
{
    /// <summary>
    /// 通用的下拉框数据源读取
    /// Created by Sunlz on 9/8/2015
    /// </summary>
    public partial class CommonController :BaseController
    {
        private static readonly JavaScriptSerializer _jsSerializer = new JavaScriptSerializer();

        #region Begin 读取下来框数据源

        [Description("根据类型获取数据列表")]
        public ActionResult Status_GetList(int headerType, string statusType)
        {
            IList<ItemModel> itemList = StatusHelper.GetConstStatus(statusType);
            return GetResult(headerType,itemList);
        }


        /// <summary>
        /// 绑定年份
        /// </summary>
        /// <param name="IsSelect"></param>
        /// <returns></returns>
        public ActionResult BindYearList(int headerType)
        {
            var itemList = new List<ItemModel>();
            int curYear = DateTime.Now.Year + 5;
            for (var i = 2015; i < curYear; i++)
            {
                itemList.Add(new ItemModel(i.ToString(), i.ToString()));
            }

            return GetResult(headerType, itemList);
        }

      
        [NonAction]
        private ActionResult GetResult(int headerType,IList<ItemModel> itemList)
        {
            if(headerType == HeaderType.Select)
            {
                itemList.Insert(0, new ItemModel("-1", "请选择"));
            }
            else if (headerType == HeaderType.All)
            {
                itemList.Insert(0, new ItemModel("-1", "全部"));
            }
            else if (headerType == HeaderType.Blank)
            {
                itemList.Insert(0, new ItemModel("-1", ""));
            }

            //将对象转换为Json字符串
            string sJsonStr = _jsSerializer.Serialize(itemList);
            return Json(new { IsSuccess = true, Message = sJsonStr }, JsonRequestBehavior.AllowGet);
        }




        [Description("添加下拉框的全部选项")]
        [NonAction]
        private ItemModel CreateAllOption()
        {
            return new ItemModel("", "全部");
        }


        #endregion End 读取下来框数据源


    }
}
