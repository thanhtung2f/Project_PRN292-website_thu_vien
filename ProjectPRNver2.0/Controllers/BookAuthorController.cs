using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DAO;
using PagedList;

namespace ProjectPRNver2._0.Controllers
{
    public class BookAuthorController : Controller
    {
        // GET: BookAuthor
      
        public ActionResult detailAuthor(int id)
        {
            var author = new ProductDao().ViewAuthor(id);
            return View(author);
        }
        public ActionResult listAuthor(int page = 1, int pageSize = 8)
        {
            var dao = new ProductDao();
            var model = dao.ListAllAuthor(page, pageSize);
            return View(model);
        }

    }
}