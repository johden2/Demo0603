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
    public class MesSysOrganizationDao : BaseDao
    {
        public static MesSysOrganizationDao _Instance = null;

        public static MesSysOrganizationDao Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MesSysOrganizationDao();
                }
                return _Instance;
            }
        }


        #region 查询和分页方法

      

        #endregion 查询和分页方法


       private static string _treeItemContent = "{\"id\":\"[0]\",\"pId\":\"[1]\",\"name\":\"[2]\",\"open\":\"[3]\"},";
        public string GetTree(Mes_Sys_Organization obj)
        {
            string sql = @"SELECT * FROM dbo.Mes_Sys_Organization T1 WITH(NOLOCK) WHERE RecordStatus = 1";
            // { id: 1, pId: 0, name: "技术部", open: true},
            List<Mes_Sys_Organization> list = this.CurDbSession.FromSql(sql).ToList<Mes_Sys_Organization>();
            StringBuilder jsonStr = new StringBuilder();
            jsonStr.Append("[");
            //List<Mes_Sys_Organization> oneList = list.Where(p => obj.OrgLevel == 1).ToList();
            bool isOpen = false;
            foreach (var item in list)
            {
                isOpen = false;
                if (item.ParentID == 0)
                {
                    isOpen = true;
                }

                jsonStr.Append(_treeItemContent.Replace("[0]", item.ID.ToString())
                    .Replace("[1]", item.ParentID.ToString())
                    .Replace("[2]", item.OrgName)
                    .Replace("[3]", isOpen.ToString())
                    .Replace("[4]", item.OrgLevel.ToString())
                    );
            }

            string result = jsonStr.ToString();
            return result.Remove(result.Length - 1) + "]";
        }

        public void GetChildNodes(List<Mes_Sys_Organization> list, Mes_Sys_Organization node, StringBuilder jsonStr)
        {
            List<Mes_Sys_Organization> childList = list.Where(p =>p.ParentID == node.ID).ToList();
            if (childList == null || childList.Count == 0) return;

            foreach (var item in childList)
            {
                jsonStr.Append(_treeItemContent.Replace("[0]", item.ID.ToString())
                    .Replace("[1]", item.ParentID.ToString())
                    .Replace("[2]", item.OrgName)
                    .Replace("[3]", "true")
                    .Replace("[4]", item.OrgLevel.ToString())
                    );
            }

        }


        public string GetList()
        {
            string sql = @"SELECT * FROM dbo.Mes_Sys_Organization T1 WITH(NOLOCK) WHERE RecordStatus = 1";
            List<Mes_Sys_Organization> list = this.CurDbSession.FromSql(sql).ToList<Mes_Sys_Organization>();
            List<Mes_Sys_Organization> oneList = list.Where(p => p.OrgLevel == 1).ToList();

            StringBuilder jsonStr = new StringBuilder();
            jsonStr.Append("[");
            foreach (var item in oneList)
            {
                jsonStr.Append("{");
                jsonStr.AppendFormat("\"id\":\"{0}\",\"text\":\"{1}\"", item.ID, item.OrgName);

                GetChildNodesByList(list, item, jsonStr);
                jsonStr.Append("},");
            }
            jsonStr = jsonStr.Remove(jsonStr.Length - 1, 1);
            jsonStr.Append("]");
            return jsonStr.ToString();
        }
        public void GetChildNodesByList(List<Mes_Sys_Organization> list, Mes_Sys_Organization node, StringBuilder jsonStr)
        {
            List<Mes_Sys_Organization> childList = list.Where(p => p.ParentID == node.ID).ToList();
            if (childList == null || childList.Count == 0) return;

            jsonStr.Append(",\"children\":[");
            foreach (var item in childList)
            {
                jsonStr.Append("{");
                jsonStr.AppendFormat("\"id\":\"{0}\",\"text\":\"{1}\"", item.ID, item.OrgName);
                GetChildNodesByList(list, item, jsonStr);
                jsonStr.Append("},");
            }
            jsonStr = jsonStr.Remove(jsonStr.Length - 1, 1);
            jsonStr.Append("]");

        }


    }
}
