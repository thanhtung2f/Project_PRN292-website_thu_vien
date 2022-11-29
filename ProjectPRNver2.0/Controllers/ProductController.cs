using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectPRNver2._0.EF;
using PagedList;
using PagedList.Mvc;
namespace ProjectPRNver2._0.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        ProjectPRNDbContext db = new ProjectPRNDbContext();
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.Books.ToList().OrderByDescending(n => n.BookId).ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult AddNew()
        {
            ViewBag.AuthorId = new SelectList(db.Book_Authors.ToList(), "AuthorId", "AuthorName");
            ViewBag.CategoryId = new SelectList(db.Book_Category.ToList(), "CategoryId", "CategoryName");
            return View();
        }
        [HttpPost]
        public ActionResult AddNew(Book book, HttpPostedFileBase upload)
        {

            ViewBag.AuthorId = new SelectList(db.Book_Authors.ToList(), "AuthorId", "AuthorName");
            ViewBag.CategoryId = new SelectList(db.Book_Category.ToList(), "CategoryId", "CategoryName");

            if (upload == null)
            {
                ViewBag.Noice = "Chọn hình ảnh";
                return View();
            }

            if (ModelState.IsValid)
            {
                var imgName = Path.GetFileName(upload.FileName);
                var imgPath = Path.Combine(Server.MapPath("~/resource/img"), imgName);

                if (System.IO.File.Exists(imgPath))
                {
                    ViewBag.Noice = "Hình ảnh đã tồn tại";
                }
                else
                {
                    upload.SaveAs(imgPath);
                }
                book.imgPath = upload.FileName;
                db.Books.Add(book);
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        public ActionResult UpdateBook(int bookId)
        {

            Book book = db.Books.SingleOrDefault(x => x.BookId == bookId);
            if (book == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.AuthorId = new SelectList(db.Book_Authors.ToList(), "AuthorId", "AuthorName");
            ViewBag.CategoryId = new SelectList(db.Book_Category.ToList(), "CategoryId", "CategoryName");
            return View(book);
        }

        [HttpPost]

        public ActionResult UpdateBook(Book book, HttpPostedFileBase upload)
        {

            ViewBag.AuthorId = new SelectList(db.Book_Authors.ToList(), "AuthorId", "AuthorName");
            ViewBag.CategoryId = new SelectList(db.Book_Category.ToList(), "CategoryId", "CategoryName");

            if (upload == null)
            {
                ViewBag.Noice = "Chọn hình ảnh";
                return View();
            }

            if (ModelState.IsValid)
            {
                var imgName = Path.GetFileName(upload.FileName);
                var imgPath = Path.Combine(Server.MapPath("~/resource/img"), imgName);

                if (System.IO.File.Exists(imgPath))
                {
                    ViewBag.Noice = "Hình ảnh đã tồn tại";
                }
                else
                {
                    upload.SaveAs(imgPath);
                }
                book.imgPath = upload.FileName;
                db.Entry(book).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Product");
        }

        public ActionResult Detail(int bookId)
        {
            Book book = db.Books.SingleOrDefault(x => x.BookId == bookId);
            if (book == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(book);
        }

        [HttpGet]
        public ActionResult Delete(int bookId)
        {
            Book book = db.Books.SingleOrDefault(x => x.BookId == bookId);
            if (book == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int bookId)
        {
            Book book = db.Books.SingleOrDefault(x => x.BookId == bookId);
            if (book == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.Books.Remove(book);
            db.SaveChanges();

            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        public ActionResult AddNewAuthor()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult AddNewAuthor(Book_Authors author, HttpPostedFileBase upload)
        {


            if (upload == null)
            {
                ViewBag.Noice = "Chọn hình ảnh";
                return View();
            }

            if (ModelState.IsValid)
            {
                var imgName = Path.GetFileName(upload.FileName);
                var imgPath = Path.Combine(Server.MapPath("~/resource/img"), imgName);

                if (System.IO.File.Exists(imgPath))
                {
                    ViewBag.Noice = "Hình ảnh đã tồn tại";
                }
                else
                {
                    upload.SaveAs(imgPath);
                }
                author.Image = upload.FileName;
                db.Book_Authors.Add(author);
                db.SaveChanges();
            }

            return RedirectToAction("ManageAuthor", "Product");
        }

        public ActionResult ManageAuthor(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.Book_Authors.ToList().OrderByDescending(n => n.AuthorId).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult UpdateAuthor(int authorId)
        {

            Book_Authors author = db.Book_Authors.SingleOrDefault(x => x.AuthorId == authorId);
            if (author == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(author);
        }

        [HttpPost]

        public ActionResult UpdateAuthor(Book_Authors author, HttpPostedFileBase upload)
        {


            if (upload == null)
            {
                ViewBag.Noice = "Chọn hình ảnh";
                return View();
            }

            if (ModelState.IsValid)
            {
                var imgName = Path.GetFileName(upload.FileName);
                var imgPath = Path.Combine(Server.MapPath("~/resource/img"), imgName);

                if (System.IO.File.Exists(imgPath))
                {
                    ViewBag.Noice = "Hình ảnh đã tồn tại";
                }
                else
                {
                    upload.SaveAs(imgPath);
                }
                author.Image = upload.FileName;
                db.Entry(author).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("ManageAuthor", "Product");
        }

        public ActionResult DetailAuthorAdmin(int authorId)
        {
            Book_Authors author = db.Book_Authors.SingleOrDefault(x => x.AuthorId == authorId);
            if (author == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(author);
        }

        [HttpGet]
        public ActionResult DeleteAuthor(int authorId)
        {
            Book_Authors author = db.Book_Authors.SingleOrDefault(x => x.AuthorId == authorId);
            if (author == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(author);
        }

        [HttpPost, ActionName("DeleteAuthor")]
        public ActionResult ConfirmDeleteAuthor(int authorId)
        {
            Book_Authors author = db.Book_Authors.SingleOrDefault(x => x.AuthorId == authorId);
            if (author == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.Book_Authors.Remove(author);
            db.SaveChanges();

            return RedirectToAction("ManageAuthor", "Product");
        }
        
    }
}