using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectPRNver2._0.Controllers
{
    public class OrderViewController : Controller
    {
        // GET: OrderView
        public ActionResult OrderView(int id)
        {
            var userDao = new UserDao();
            var orderDao = new OrderDao();
            ViewBag.user = userDao.ViewUserDetail(id);
            ViewBag.order = orderDao.OrderList(id);
            return View();
        }
    }
}