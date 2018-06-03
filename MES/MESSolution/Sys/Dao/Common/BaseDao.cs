using Dos.ORM;
using Sys.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace Sys.Dao
{
    public class BaseDao
    {
        public const string PageSql = "with cte as( select id0=row_number() over(order by {0}),* from  ({1}) as cte1) select * from cte where id0 between {2} and {3}";
        public const string CountSql = "select count(*) {0}";

        private static DbSession _DbSessionObj = null;

        protected string DbConnName = "MasterDB";
      //  public string DbConnName = HttpContext.Current.Application["DBdata"].ToString();
        #region 构造方法

        public BaseDao()
        {
            try
            {
                if (_DbSessionObj == null)
                {
                    _DbSessionObj = new DbSession(DbConnName);
                    
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
        /// <summary>
        /// 供外层使用的数据操作对象
        /// </summary>
        public DbSession CurDbSession
        {
            get
            {
                return _DbSessionObj;
            }
        }

        /// <summary>
        /// 获取主键
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Field GetKeyField<T>(T obj) where T : Entity
        {
            Field[] fields = obj.GetPrimaryKeyFields();
            if (fields == null) return null;

            return fields[0];
        }
       
        #endregion 构造方法

        #region 新增、修改、删除功能

        
        /// <summary>
        /// 通用保存方法
        /// 新增时：要求保证主键是自增列，否则，请调用下面的Insert方法进行新增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int Save<T>(T obj) where T : Entity, new() 
        {
            Type type = obj.GetType();
            string className = type.Name;

            Field keyField = this.GetKeyField(obj);
            if (keyField == null)
            {
                throw new Exception("未定义主键属性");
            }

            PropertyInfo property = type.GetProperty(keyField.Name);
            if (property == null)
            {
                throw new Exception("未定义主键属性");
            }

            object idValue = property.GetValue(obj, null);
            int id = 0;
            if (idValue != null)
            {
                id = Convert.ToInt32(idValue.ToString());
            }

            if (id > 0)
            {
                this.CurDbSession.Update<T>(obj);
            }
            else
            {
                id = this.CurDbSession.Insert<T>(obj);
            }

            return id;
        }

        /// <summary>
        /// 插入实体(主键可以不是自增列)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int Insert<T>(T obj) where T : Entity, new()
        {
            Type type = obj.GetType();
            string className = type.Name;

            Field keyField = this.GetKeyField(obj);
            if (keyField == null)
            {
                throw new Exception("未定义主键属性");
            }

            PropertyInfo property = type.GetProperty(keyField.Name);
            if (property == null)
            {
                throw new Exception("未定义主键属性");
            }

            try
            {
                int id = this.GetMaxId<T>(keyField);
                property.SetValue(obj, id, null);

                this.CurDbSession.Insert<T>(obj);
                return id;

            }catch(System.Exception ex)
            {
                return -1;
            }
        }



        /// <summary>
        /// 通用的删除功能
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Delete<T>(T obj) where T : Entity
        {
            if (obj == null) return false;

            Field keyField = this.GetKeyField(obj);
            if (keyField == null)
            {
                throw new Exception("未定义主键属性");
            }

            Type type = obj.GetType();
            PropertyInfo property = type.GetProperty(keyField.Name);
            if (property == null)
            {
                throw new Exception("未定义主键属性");
            }
            object idValue = property.GetValue(obj, null);
            int id = 0;
            if (idValue != null)
            {
                id = Convert.ToInt32(idValue.ToString());
            }

            if (id <= 0) return false;

            try
            {
                this.CurDbSession.Delete<T>(obj);
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public bool Delete<T>(int id) where T : Entity, new()
        {
            if (id <= 0) return false;

            try
            {
                Field keyField = this.GetKeyField(new T());
                if (keyField == null)
                {
                    throw new Exception("未定义主键属性");
                }

                this.CurDbSession.Delete<T>(id);
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        #endregion 新增、修改、删除功能


        #region 查询功能

        /// <summary>
        /// 获取主键的最大编号 + 1
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public int GetMaxId<T>() where T : Entity, new()
        {
            Field keyField = this.GetKeyField(new T());
            if (keyField == null)
            {
                throw new Exception("未定义主键属性");
            }

            return this.GetMaxId<T>(keyField);
        }

        public int GetMaxId<T>(Field keyField) where T : Entity
        {
            object result = this.CurDbSession.Max<T>(keyField, new Where());
            if (result == null || result == DBNull.Value) return 1;

            return Convert.ToInt32(result) + 1;
        }

        /// <summary>
        ///依据主键查询单个实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Find<T, TParam>(TParam id)
            where T : Entity, new()
        {
            Field keyField = this.GetKeyField(new T());
            if (keyField == null)
            {
                throw new Exception("未定义主键属性");
            }
            if(keyField.Name=="Id")
            {
                return this.CurDbSession.From<T>().Where(string.Format("{0}={1}", keyField.Name, id)).ToFirst();
            }
            return this.CurDbSession.From<T>().Where(string.Format("{0}='{1}'", keyField.Name, id)).ToFirst();
        }


        /// <summary>
        /// 查询所有的记录
        /// </summary>
        public List<T> FindAll<T>() where T : Entity
        {
            return this.CurDbSession.From<T>().ToList();
        }

        #endregion 查询功能


    }
}
