using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Phap
{
    public class BookPhap
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public decimal? BookPrice { get; set; }
        public string imgPath { get; set; }
        public string AuthorName { get; set; }
    }
}
