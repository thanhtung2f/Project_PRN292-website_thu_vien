using ProjectPRNver2._0.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectPRN.Models
{
    [Serializable]
    public class CartItem
    {
        //public int catId { set; get; }
        public Book Book { set; get; }
        //public int userId { set; get; }
        public int Quantity { set; get; }
    }
}