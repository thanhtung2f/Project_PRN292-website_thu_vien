using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DAO;

namespace ProjectPRNver2._0.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var productDao = new ProductDao();
            ViewBag.NewBook = productDao.ListNewBook(4);
            ViewBag.Top1NewBook = productDao.ListNewBook(1);
            ViewBag.Manga = productDao.ListManga(4);
            ViewBag.Author = productDao.ListAuthor();
            return View();
        }
    }
}