using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectPRNver2._0.Controllers
{
    public class WishListController : Controller
    {
        // GET: WishList
        public ActionResult WishList(int id)
        {
            var userDao = new UserDao();
            var productDao = new ProductDao();
            ViewBag.wishBook = productDao.ListWishList(id);
            ViewBag.user = userDao.ViewUserDetail(id);
            return View();
        }
    }
}