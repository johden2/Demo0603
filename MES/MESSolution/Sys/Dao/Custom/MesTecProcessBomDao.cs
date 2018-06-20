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
    public class MesTecProcessBomDao : BaseDao
    {
        public static MesTecProcessBomDao _Instance = null;

        public static MesTecProcessBomDao Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MesTecProcessBomDao();
                }
                return _Instance;
            }
        }


        #region 查询和分页方法


        private const string _itemHTML = "\"id\":\"[0]\",\"text\":\"[1]\", \"attributes\":{\"MaterialProNo\":\"[2]\",\"Version\":\"[3]\",\"MaterialCode\":\"[4]\"}";
        /// <summary>
        /// 获取树结构
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string GetTree(Mes_Tec_ProcessBom obj)
        {
            string sql = @"SELECT ID,MaterialProNo,Version,MaterialCode FROM dbo.Mes_Tec_ProcessBom T1 WITH(NOLOCK) WHERE 1=1";
            List<Mes_Tec_ProcessBom> list = this.CurDbSession.FromSql(sql).ToList<Mes_Tec_ProcessBom>();
            StringBuilder jsonStr = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                jsonStr.Append("[");
                int index = 0;
                List<KeyModel> keyList = list.Select(p => new KeyModel { Key = p.MaterialProNo, Value = p.MaterialCode })
                          .GroupBy(p => new { p.Key, p.Value })
                          .Select(g => g.First())
                          .ToList();

                foreach (var item in keyList)
                {
                    index++;
                    jsonStr.Append("{");
                    jsonStr.Append(_itemHTML.Replace("[0]", index.ToString()).Replace("[1]", item.Value).Replace("[2]", item.Key).Replace("[3]", "").Replace("[4]", item.Value));
                    GetChildNodes(list, item, jsonStr, ref index);
                    jsonStr.Append("},");
                }
                jsonStr = jsonStr.Remove(jsonStr.Length - 1, 1);
                jsonStr.Append("]");
            }
           
            return jsonStr.ToString();
        }

        public void GetChildNodes(List<Mes_Tec_ProcessBom> list, KeyModel node, StringBuilder jsonStr,ref int index)
        {
            List<Mes_Tec_ProcessBom> childList = list.Where(p => p.MaterialProNo == node.Key).ToList();
            if (childList == null || childList.Count == 0) return;

            jsonStr.Append(",\"children\":[");
            foreach (var item in childList)
            {
                index++;
                jsonStr.Append("{");
                jsonStr.Append(_itemHTML.Replace("[0]", index.ToString()).Replace("[1]", item.Version).Replace("[2]", item.MaterialProNo).Replace("[3]", item.Version).Replace("[4]", item.MaterialCode));
                jsonStr.Append("},");
            }
            jsonStr = jsonStr.Remove(jsonStr.Length - 1, 1);
            jsonStr.Append("]");
        }


        public List<Mes_Tec_ProcessBom> FindByPage(Mes_Tec_ProcessBom obj, ref PagerBase pager)
        {
            string sql = @"SELECT T1.ID,T1.MaterialProNo,T1.Version,T1.MaterialCode,T2.ID AS ItemID,T2.SubMaterialProNo,T2.SubMaterialCode,T2.Num,T2.Unit,T2.Memo,T2.ProcessID,T3.Name AS ProcessName 
                        FROM dbo.Mes_Tec_ProcessBom T1 WITH(NOLOCK) 
                        INNER JOIN Mes_Tec_ProcessBomItem T2 WITH(NOLOCK) ON T1.ID = T2.ProcessBomID
                        LEFT JOIN dbo.Mes_Tec_Process T3 WITH(NOLOCK) ON T2.ProcessID = T3.ID
                        WHERE 1=1";

            if (!string.IsNullOrEmpty(obj.MaterialProNo))
            {
                sql += string.Format(" AND T1.MaterialProNo = '{0}'", obj.MaterialProNo);
            }
            if (!string.IsNullOrEmpty(obj.SubMaterialProNo))
            {
                sql += string.Format(" AND T2.SubMaterialProNo Like '%{0}%'", obj.SubMaterialProNo);
            }
            if (!string.IsNullOrEmpty(obj.MaterialCode))
            {
                sql += string.Format(" AND T1.MaterialCode = '{0}'", obj.MaterialCode);
            }
            if (!string.IsNullOrEmpty(obj.SubMaterialCode))
            {
                sql += string.Format(" AND T2.SubMaterialCode Like '%{0}%'", obj.SubMaterialCode);
            }
            if (!string.IsNullOrEmpty(obj.Version))
            {
                sql += string.Format(" AND T1.Version = '{0}'", obj.Version);
            }
            if (obj.ProcessID > 0)
            {
                sql += string.Format(" AND T2.ProcessID = '{0}'", obj.ProcessID);
            }

            string orderBy = pager.OrderBy;
            if (string.IsNullOrEmpty(orderBy))
            {
                orderBy = "MaterialProNo ASC,Version DESC,SubMaterialProNo ASC";
            }
            string cmdPageSql = string.Format(BaseDao.PageSql, orderBy, sql, pager.StartNo, pager.EndNo);
            string cmdCountSql = string.Format(BaseDao.CountSql, sql.Substring(sql.ToLower().IndexOf("from", StringComparison.Ordinal)));
            //查询总记录数
            pager.TotalItemCount = this.CurDbSession.FromSql(cmdCountSql).ToScalar<int>();
            //返回当前页的记录数
            return this.CurDbSession.FromSql(cmdPageSql).ToList<Mes_Tec_ProcessBom>();
        }

        /// <summary>
        /// 获取实体对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Mes_Tec_ProcessBom FindExt(Mes_Tec_ProcessBom obj)
        {
            string sql = "SELECT T1.* FROM dbo.Mes_Tec_ProcessBom T1 WITH(NOLOCK) WHERE 1=1";
            if (!string.IsNullOrEmpty(obj.MaterialProNo))
            {
                sql += string.Format(" AND T1.MaterialProNo = '{0}'", obj.MaterialProNo);
            }
            if (!string.IsNullOrEmpty(obj.Version))
            {
                sql += string.Format(" AND T1.Version = '{0}'", obj.Version);
            }

            return this.CurDbSession.FromSql(sql).ToFirst<Mes_Tec_ProcessBom>();
        }


        #endregion 查询和分页方法

        public bool DeleteExt(Mes_Tec_ProcessBom obj)
        {
            string sql = "DELETE FROM Mes_Tec_ProcessBomItem WHERE ProcessBomID = {0}";
            sql = string.Format(sql, obj.ID);
            int result = this.CurDbSession.FromSql(sql).ExecuteNonQuery();

            this.CurDbSession.Delete(obj);
            return true;
        }

        /// <summary>
        /// 保存明细
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="resultObj"></param>
        /// <returns></returns>
        public bool SaveBomItem( Mes_Tec_ProcessBom obj,Mes_Tec_ProcessBom resultObj)
        {
            int result = -1;

            //2.保存明细
            Mes_Tec_ProcessBomItem itemObj = new Mes_Tec_ProcessBomItem();
            itemObj.ID = obj.ItemID;
            itemObj.ProcessBomID = resultObj.ID;
            itemObj.ProcessID = obj.ProcessID;
            itemObj.SubMaterialProNo = obj.SubMaterialProNo;
            itemObj.SubMaterialCode = obj.SubMaterialCode;
            itemObj.Num = obj.Num;
            itemObj.Unit = obj.Unit;
            itemObj.Memo = obj.Memo;

            if (itemObj.ID > 0)
            {
                result = this.CurDbSession.Update<Mes_Tec_ProcessBomItem>(itemObj);
            }
            else
            {
                itemObj.Creater = obj.Creater;
                itemObj.CreatedTime = obj.CreatedTime;
                result = this.CurDbSession.Insert<Mes_Tec_ProcessBomItem>(itemObj);
            }
           
            return true;
        }

        //样品导入
        public bool Import(List<Mes_Tec_ProcessBom> bomList,List<Mes_Tec_ProcessBomItem> list, IList<ImportMessageModel> resultList)
        {
            try
            {
                int result = 0;
                using (DbTrans trans = this.CurDbSession.BeginTransaction())
                {
                    //1.保存主表
                    foreach (var item in bomList)
                    {
                        result = this.CurDbSession.Insert<Mes_Tec_ProcessBom>(item);
                        if (result <= 0)
                        {
                            trans.Rollback();
                            return false;
                        }

                        list.ForEach(p =>
                        {
                            if (p.MaterialProNo == item.MaterialProNo && p.Version == item.Version)
                            {
                                p.ProcessBomID = result;
                            }
                        });
                    }
                    //2.保存明细
                    result = this.CurDbSession.Insert<Mes_Tec_ProcessBomItem>(list);
                    if (result <= 0)
                    {
                        trans.Rollback();
                        return false;
                    }

                    //提交事务
                    trans.Commit();
                }
                 return true;
            }
            catch (Exception ex)
            {
                resultList.Add(new ImportMessageModel { RowData = "导入失败", RowMessage = "导入出错，错误信息：" + ex.Message });
            }
            return false;
        }

    }
}
