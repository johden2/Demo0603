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

        private static string _treeItemContent = "{\"id\":\"[0]\",\"pId\":\"[1]\",\"name\":\"[2]\",\"open\":\"false\",\"nocheck\":\"[3]\",\"isparent\":\"[4]\"},";
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
                  );
            }

            string result = jsonStr.ToString();
            return result.Remove(result.Length - 1) + "]";
        }

        public void GetChildNodes(List<Mes_Sys_Organization> list, Mes_Sys_Organization node, StringBuilder jsonStr)
        {
            List<Mes_Sys_Organization> childList = list.Where(p => p.ParentID == node.ID).ToList();
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

        
             /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public List<Mes_Sys_Supplier> FindByPage(Mes_Sys_Supplier obj, ref PagerBase pager)
        {
            string sql = @"select * from Mes_Sys_Supplier WHERE   1=1 ";

            if (!string.IsNullOrEmpty(obj.SupplierCode))
            {
                sql += string.Format(" AND SupplierCode Like '%{0}%'", obj.SupplierCode);
            }

            if (!string.IsNullOrEmpty(obj.SupplierName))
            {
                sql += string.Format(" AND SupplierName Like '%{0}%'", obj.SupplierName);
            }
            if (!string.IsNullOrEmpty(obj.SupplierPrcName))
            {
                sql += string.Format(" AND SupplierPrcName Like '%{0}%'", obj.SupplierPrcName);
            }
            string orderBy = pager.OrderBy;
            if (string.IsNullOrEmpty(orderBy))
            {
                orderBy = "CreatedTime DESC";
            }
            string cmdPageSql = string.Format(BaseDao.PageSql, orderBy, sql, pager.StartNo, pager.EndNo);
            string cmdCountSql = string.Format(BaseDao.CountSql, sql.Substring(sql.ToLower().IndexOf("from", StringComparison.Ordinal)));
            //查询总记录数
            pager.TotalItemCount = this.CurDbSession.FromSql(cmdCountSql).ToScalar<int>();
            //返回当前页的记录数
            return this.CurDbSession.FromSql(cmdPageSql).ToList<Mes_Sys_Supplier>();
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
