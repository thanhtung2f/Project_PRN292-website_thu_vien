using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectPRNver2._0.Common
{
    [Serializable]
    public class UserLogin
    {
        public int UserID { set; get; }
        public string UserName { set; get; }
        public string UserMail { get; set; }
        public string UserPhone { get; set; }
        public string Address { get; set; }
    }
}