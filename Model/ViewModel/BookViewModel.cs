using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
  public  class BookViewModel
    {
        public int BookId { set; get; }
        public string BookName { set; get; }
        public string BookDescription { set; get; }
        public decimal? BookPrice { set; get; }
        public int AuthorId { set; get; }
        public string ImgPath { set; get; }

        public DateTime ReleaseDate { set; get; }
        public string AuthorName { set; get; }

        public int CategoryId { set; get; }
    }
}
