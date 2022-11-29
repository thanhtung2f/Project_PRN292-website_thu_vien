using Model.DAO;
using ProjectPRNver2._0.EF;
using PagedList;
using PagedList.Mvc;
using ProjectPRNver2._0.Common;
using Scrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectPRNver2._0.Controllers
{
    public class AdminUserController : Controller
    {
        // GET: AdminUser
        ProjectPRNDbContext db = new ProjectPRNDbContext();
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.Users.ToList().OrderBy(n => n.Roleid).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult AddUser()
        {
            ViewBag.RoleId = new SelectList(db.Roles.ToList(), "id", "name");
            return View();
        }
        [HttpPost]
        public ActionResult AddUser(User user)
        {
            ScryptEncoder encoder = new ScryptEncoder();
            ViewBag.RoleId = new SelectList(db.Roles.ToList(), "id", "name");
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (dao.CheckUserName(user.UserName))
                {
                    ModelState.AddModelError("UsernameExist", "Tên đăng nhập đã tồn tại");
                }
                else if (dao.CheckEmail(user.UserMail))
                {
                    ModelState.AddModelError("EmailExist", "Email đã tồn tại");
                }
                else
                {
                    user.PassWord = encoder.Encode(user.PassWord);
                    user.CreatedDate = DateTime.Now;
                    user.Status = true;
                    db.Users.Add(user);
                    db.SaveChanges();
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult UpdateUser(int userId)
        {
            User user = db.Users.SingleOrDefault(x => x.UserId == userId);
            if (user == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.RoleId = new SelectList(db.Roles.ToList(), "id", "name");
            return View(user);
        }
        [HttpPost]
        public ActionResult UpdateUser(User user)
        {
            ViewBag.RoleId = new SelectList(db.Roles.ToList(), "id", "name");
            if (ModelState.IsValid)
            {       
                    user.CreatedDate = DateTime.Now;
                    user.Status = true;
                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
              
            }
            return RedirectToAction("Index", "AdminUser");
        }

    }
}