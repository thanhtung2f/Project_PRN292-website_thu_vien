using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DAO;
using ProjectPRNver2._0.EF;
using ProjectPRNver2._0.ViewModel;

namespace ProjectPRNver2._0.Controllers
{
    public class BookItemController : Controller
    {
        // GET: BookItem
        public ActionResult Detail(int id)
        {
            var book = new ProductDao().ViewDetail(id);
            return View(book);
        }
        public ActionResult AddWishList(WishList model)
        {
            int bookId = Convert.ToInt32(Request.Params["bookId"]);
            int userId = Convert.ToInt32(Request.Params["userId"]);
            var dao = new WishListDao();
            var book = new ProductDao().ViewDetail(bookId);
            try
            {


                //dao add db
                var wishList = new WishList();
                wishList.BookId = bookId;
                wishList.UserId = userId;
                dao.Insert(wishList);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            //get book id and user id
            return View(book);
        }
    }
}