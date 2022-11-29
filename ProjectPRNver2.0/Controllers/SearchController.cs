using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using ProjectPRNver2._0.EF;

namespace ProjectPRNver2._0.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search

        ProjectPRNDbContext db = new ProjectPRNDbContext();

        [HttpPost]
        public ActionResult SearchResult(FormCollection f, int? page)
        {
            string keyword = f["txtSearch"].ToString();
            ViewBag.Tukhoa = keyword;
            List<Book> lstSearch = db.Books.Where(s => s.BookName.Contains(keyword)).ToList();
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            if (lstSearch.Count == 0)
            {
                ViewBag.Noice = "Không tìm thấy sản phẩm nào";
                return View(db.Books.OrderBy(n => n.BookName).ToPagedList(pageNumber, pageSize));
            }
            ViewBag.Noice = "Đã tìm thấy " + lstSearch.Count + " kết quả";

            
            return View(lstSearch.OrderBy(n => n.BookName).ToPagedList(pageNumber, pageSize));
        }


        [HttpGet]
        public ActionResult SearchResult(int? page, string keyword)
        {
            ViewBag.Tukhoa = keyword;
            List<Book> lstSearch = db.Books.Where(s => s.BookName.Contains(keyword)).ToList();
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            if (lstSearch.Count == 0)
            {
                ViewBag.Noice = "Không tìm thấy sản phẩm nào";
                return View(db.Books.OrderBy(n => n.BookName).ToPagedList(pageNumber, pageSize));
            }
            ViewBag.Noice = "Đã tìm thấy " + lstSearch.Count + " kết quả";


            return View(lstSearch.OrderBy(n => n.BookName).ToPagedList(pageNumber, pageSize));
        }   
    }
}