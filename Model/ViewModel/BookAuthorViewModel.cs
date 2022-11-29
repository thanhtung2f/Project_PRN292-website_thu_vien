using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
     public class BookAuthorViewModel
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }
    }
}
