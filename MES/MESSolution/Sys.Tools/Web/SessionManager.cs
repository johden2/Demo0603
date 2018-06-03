using System.Collections.Generic;
using System.Web;

namespace Sys.Tools
{
    /// <summary>
    /// Session管理类
    /// Created by Sunlz on 17/10/2015
    /// </summary>
    public class SessionManager
    {
        /// <summary>
        /// 对外实例对象
        /// </summary>
        public static readonly SessionManager Instance = new SessionManager();

        /// <summary>
        /// Session的Key的容器
        /// </summary>
        private IDictionary<string, string> _sessionDict = new Dictionary<string, string>();

        /// <summary>
        /// 将数据添加到Session中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sessionKey"></param>
        /// <param name="sessionValue"></param>
        public void AddSession<T>(string sessionKey, T sessionValue)
        {
            HttpContext.Current.Session[sessionKey] = sessionValue;
            //向容器中添加该Session的Key，值为空
            _sessionDict[sessionKey] = null;
        }

        /// <summary>
        /// 获取Session对象值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sessionKey"></param>
        /// <returns></returns>
        public T GetSession<T>(string sessionKey)
        {
            if (HttpContext.Current.Session[sessionKey] != null)
            {
                return (T)HttpContext.Current.Session[sessionKey];
            }

            return default(T);
        }

        /// <summary>
        /// 删除指定Session
        /// </summary>
        /// <param name="sessionKey"></param>
        public void RemoveSession(string sessionKey)
        {
            HttpContext.Current.Session[sessionKey] = null;

            if (_sessionDict.ContainsKey(sessionKey))
            {
                _sessionDict.Remove(sessionKey);
            }
        }

        /// <summary>
        /// 清理所有的Session数据
        /// </summary>
        public void ClearAllSession()
        {
            if (_sessionDict == null || _sessionDict.Count == 0) return;

            //遍历清理Session
            foreach (var item in _sessionDict)
            {
                HttpContext.Current.Session[item.Key] = null;
            }

            _sessionDict.Clear();
        }

    }
}