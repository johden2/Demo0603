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
    public class MesSysMapDao : BaseDao
    {
        public static MesSysMapDao _Instance = null;

        public static MesSysMapDao Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MesSysMapDao();
                }
                return _Instance;
            }
        }


        #region 查询和分页方法

      

        #endregion 查询和分页方法

    }
}
