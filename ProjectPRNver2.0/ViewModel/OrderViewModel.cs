using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectPRNver2._0.ViewModel
{
    public class OrderViewModel
    {
        public int OrderDetailId { set; get; }
        public int? OrderId { set; get; }
        public int? BookId { set; get; }
        public string BookName { set; get; }
        public int? Quantity { set; get; }
        public decimal? price { set; get; }
        public string image { set; get; }
    }
}