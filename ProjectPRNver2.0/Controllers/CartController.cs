using Mail;
using Model.DAO;
using ProjectPRNver2._0.EF;
using ProjectPRN.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ProjectPRNver2._0.Controllers
{
    public class CartController : Controller
    {
        private string CartSession = "CartSession";
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Delete(int id)
        {
            var SessionCart = (List<CartItem>)Session[CartSession];
            SessionCart.RemoveAll(x => x.Book.BookId == id);
            Session[CartSession] = SessionCart;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var SessionCart = (List<CartItem>)Session[CartSession];
            foreach (var item in SessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Book.BookId == item.Book.BookId);
                System.Diagnostics.Debug.WriteLine(jsonItem);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            SessionCart = (List<CartItem>)Session[CartSession];
            return Json(new
            {
                status = true
            });
        }

        public ActionResult AddItem(int bookid, int quantity)
        {
            var book = new ProductDao().ViewDetail(bookid);
            var cart = Session[CartSession];
            if (cart != null)
            {

                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Book.BookId == bookid))
                {
                    foreach (var item in list)
                    {
                        if (item.Book.BookId == bookid)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.Book = book;
                    item.Quantity = quantity;
                    list.Add(item);
                }

            }
            else
            {
                var item = new CartItem();
                item.Book = book;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Payment()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }




        [HttpPost]
        public ActionResult Payment(string name, string email, string phone, string address)
        {
            var session = (ProjectPRNver2._0.Common.UserLogin)Session[ProjectPRNver2._0.Common.CommonConstants.USER_SESSION];
            var order = new Order();
            order.UserId = session.UserID;
            order.CreateDate = DateTime.Now;
            order.ShipAddress = address;
            order.ShipMoble = phone;
            order.ShipName = name;
            order.ShopEmail = email;
            decimal total = 0;
            var id = new OrderDao().InsertOrder(order);
            var cart = (List<CartItem>)Session[CartSession];
            var detail = new Model.DAO.OrderDetailDao();
            try
            {
                foreach (var item in cart)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.BookId = item.Book.BookId;
                    orderDetail.OrderId = (int)id;
                    orderDetail.Price = item.Book.BookPrice;
                    orderDetail.Quantity = item.Quantity;
                    detail.Insert(orderDetail);


                    total += (item.Book.BookPrice.GetValueOrDefault(0) * item.Quantity);
                }
                string content = System.IO.File.ReadAllText(Server.MapPath("~/resource/js/sendMail.html"));

                content = content.Replace("{{CustomerName}}", name);
                content = content.Replace("{{Phone}}", phone);
                content = content.Replace("{{Email}}", email);
                content = content.Replace("{{Address}}", address);

                string s = "";
                foreach (var item in cart)
                {
                    s += item.Book.BookName + ", ";
                }
                content = content.Replace("{{Book}}", s);
                //var format = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
                //string cost = string.Format(format, "{0:c3}", total1);
                content = content.Replace("{{Total}}", total.ToString());
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                new Mail_send().SendMail(email, "Đơn hàng mới từ Monster Book", content);
                new Mail_send().SendMail(toEmail, "Đơn hàng mới từ Monster Book", content);
            }
            catch (Exception e)
            {

            }

            return Redirect("/hoan-thanh");
        }

        public ActionResult Success()
        {
            return View();
        }
    }

}
