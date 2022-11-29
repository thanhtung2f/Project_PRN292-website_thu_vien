using Model.DAO;
using ProjectPRNver2._0.EF;
using Scrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectPRNver2._0.Controllers
{
    public class UserDetailController : Controller
    {
        // GET: UserDetail
        [HttpGet]
        public ActionResult UserDetail(int id)
        {
            var user = new UserDao().ViewUserDetail(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult UserDetail(User model)
        {
            ScryptEncoder encoder = new ScryptEncoder();
            var message = "";
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (model.PassWord != null)
                {
                    string newPass = Request.Params["newPass"];
                    string newPassConfirm = Request.Params["newPassConfirm"];
                    if (dao.Login(model.UserName, encoder.Encode(model.PassWord)) && newPass.Equals(newPassConfirm))
                    {
                        var encryptedMd5Pas = encoder.Encode(newPass);
                        model.PassWord = encryptedMd5Pas;
                        var result = dao.Update(model);
                    }
                }
            }
            ViewBag.message = message;
            return View(model);

        }
    }
}