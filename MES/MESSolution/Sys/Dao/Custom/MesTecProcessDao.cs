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
    public class MesTecProcessDao : BaseDao
    {
        public static MesTecProcessDao _Instance = null;

        public static MesTecProcessDao Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MesTecProcessDao();
                }
                return _Instance;
            }
        }


        #region 查询和分页方法

        private static string _treeItemContent = "{\"id\":\"[0]\",\"pId\":\"[1]\",\"name\":\"[2]\",\"open\":\"false\",\"nocheck\":\"[3]\",\"isparent\":\"[4]\",\"ProcessID\":\"[5]\"},";
        public string GetTree(Mes_Tec_Process obj)
        {
            string sql = @"SELECT * FROM dbo.Mes_Tec_Process T1 WITH(NOLOCK) WHERE RecordStatus = 1";
            // { id: 1, pId: 0, name: "技术部", open: true},
            List<Mes_Tec_Process> list = this.CurDbSession.FromSql(sql).ToList<Mes_Tec_Process>();
            StringBuilder jsonStr = new StringBuilder();
            jsonStr.Append("[");
            bool noCheck = (obj.IsCheck == 1) ? false : true;
            int isParent = 0; //是否包含子节点
            foreach (var item in list)
            {
                isParent = 0;
                if (list.Exists(p => p.ParentCode == item.ProcessCode))
                {
                    isParent = 1;
                }

                jsonStr.Append(_treeItemContent.Replace("[0]", item.ProcessCode.ToString())
                    .Replace("[1]", item.ParentCode.ToString())
                    .Replace("[2]", item.Name)
                    .Replace("[3]", noCheck.ToString())
                    .Replace("[4]", isParent.ToString())
                    .Replace("[5]", item.ID.ToString())
                  );
            }

            string result = jsonStr.ToString();
            return result.Remove(result.Length - 1) + "]";
        }

        public string GetList()
        {
            Mes_Tec_Process root = new Mes_Tec_Process();
            root.ProcessCode = "01";
            root.Name = "工艺库";

            string sql = @"SELECT * FROM dbo.Mes_Tec_Process T1 WITH(NOLOCK) WHERE RecordStatus = 1";
            List<Mes_Tec_Process> list = this.CurDbSession.FromSql(sql).ToList<Mes_Tec_Process>();
            List<Mes_Tec_Process> oneList = list.Where(p => p.ParentCode == root.ProcessCode).ToList();

            StringBuilder jsonStr = new StringBuilder();
            jsonStr.Append("[{");
            jsonStr.AppendFormat("\"id\":\"{0}\",\"text\":\"{1}\"", root.ProcessCode, root.Name);
            jsonStr.Append(",\"children\":[");
            foreach (var item in oneList)
            {
                jsonStr.Append("{");
                jsonStr.AppendFormat("\"id\":\"{0}\",\"text\":\"{1}\"", item.ProcessCode, item.Name);
                GetChildNodesByList(list, item, jsonStr);
                jsonStr.Append("},");
            }
            if (oneList.Count > 0)
            {
                jsonStr = jsonStr.Remove(jsonStr.Length - 1, 1);
            }
            jsonStr.Append("]}]");
            return jsonStr.ToString();
        }
        public void GetChildNodesByList(List<Mes_Tec_Process> list, Mes_Tec_Process node, StringBuilder jsonStr)
        {
            List<Mes_Tec_Process> childList = list.Where(p => p.ParentCode == node.ProcessCode).ToList();
            if (childList == null || childList.Count == 0) return;

            jsonStr.Append(",\"children\":[");
            foreach (var item in childList)
            {
                jsonStr.Append("{");
                jsonStr.AppendFormat("\"id\":\"{0}\",\"text\":\"{1}\"", item.ProcessCode, item.Name);
                GetChildNodesByList(list, item, jsonStr);
                jsonStr.Append("},");
            }
            jsonStr = jsonStr.Remove(jsonStr.Length - 1, 1);
            jsonStr.Append("]");

        }

        
             /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public List<Mes_Tec_Process> FindByPage(Mes_Tec_Process obj, ref PagerBase pager)
        {
            string sql = @"SELECT T1.*,T2.Name AS ParentName FROM Mes_Tec_Process T1 WITH(NOLOCK) 
                        LEFT JOIN Mes_Tec_Process T2 WITH(NOLOCK)  ON T1.ParentCode=T2.ProcessCode
                        WHERE T1.RecordStatus = 1 ";

            if (!string.IsNullOrEmpty(obj.Name))
            {
                sql += string.Format(" AND T1.Name Like '%{0}%'", obj.Name);
            }
            if (!string.IsNullOrEmpty(obj.ProcessCode))
            {
                if(obj.ProcessCode != "01"){
                    sql += string.Format(" AND (T1.ProcessCode = '{0}' OR T1.ParentCode = '{0}')", obj.ProcessCode);
                }
            }
            string orderBy = pager.OrderBy;
            if (string.IsNullOrEmpty(orderBy))
            {
                orderBy = "State,ProcessCode";
            }
            string cmdPageSql = string.Format(BaseDao.PageSql, orderBy, sql, pager.StartNo, pager.EndNo);
            string cmdCountSql = string.Format(BaseDao.CountSql, sql.Substring(sql.ToLower().IndexOf("from", StringComparison.Ordinal)));
            //查询总记录数
            pager.TotalItemCount = this.CurDbSession.FromSql(cmdCountSql).ToScalar<int>();
            //返回当前页的记录数
            return this.CurDbSession.FromSql(cmdPageSql).ToList<Mes_Tec_Process>();
        }

        #endregion 查询和分页方法

        #region 操作方法
        /// <summary>
       /// 保存用户
       /// </summary>
       /// <param name="obj"></param>
       /// <returns></returns>
        public bool Save(Mes_Tec_Process obj)
        {
            int result = -1;
            if(obj.ID>0)
            {
                //修改 
                result=this.CurDbSession.Update<Mes_Tec_Process>(obj);
            }
            else
            {
                result = this.CurDbSession.Insert<Mes_Tec_Process>(obj);
            }
            if(result<0)
            {
                return false;
            }
            return true;           
        }

        /// <summary>
        /// 单个删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool Delete(string Id)
        {
            string sql = "update Mes_Tec_Process set RecordStatus=2 where ID={0}";
            sql = string.Format(sql, Id);
            int result = this.CurDbSession.FromSql(sql).ExecuteNonQuery();                       
            if(result>0)
            {
                return true;
            }
            return false;
        }


        #endregion 查询和分页方法

     

    }
}
