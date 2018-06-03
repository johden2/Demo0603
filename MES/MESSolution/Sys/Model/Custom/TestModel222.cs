using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sys.Model
{
    /// <summary>
    /// 用户模型
    /// </summary>
    public class TestModel222
    {
        public int ID { get; set; }

        public string UserId { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }

        public int OrgID { get; set; }

        public bool IsAdmin { get; set; }

         public int RoleType { get; set; }

        public string SessionId { get; set; }    }
}
