using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectPRNver2._0.ViewModel
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