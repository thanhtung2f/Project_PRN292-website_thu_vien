using ProjectPRNver2._0.Common;
using ProjectPRNver2._0.Models;
using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Scrypt;
using ProjectPRNver2._0.EF;
using System.Net.Mail;
using System.Net;

namespace ProjectPRNver2._0.Controllers
{
    public class LoginController : Controller
    {
        ProjectPRNDbContext db = new ProjectPRNDbContext();
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string ReturnUrl = "")
        {
            ScryptEncoder encoder = new ScryptEncoder();
            if (ModelState.IsValid)
            {
                var dao = new UserDao();

                var validUser = (from c in db.Users where c.UserName.Equals(model.UserName) select c).SingleOrDefault(); 

                bool result = encoder.Compare(model.Password, validUser.PassWord);
                
                if (result)
                {
                    var user = dao.GetUserById(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.UserId;
                    userSession.UserMail = user.UserMail;
                    userSession.UserPhone = user.UserPhone;
                    userSession.Address = user.Address;
                    if (user.Status)
                    {
                        int timeout = model.RememberMe ? 525600 : 1;
                        var ticket = new FormsAuthenticationTicket(model.UserName, model.RememberMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);
                        Session.Add(CommonConstants.USER_SESSION, userSession);

                        if (user.Roleid == 1 || user.Roleid == 2)
                        {
                            return RedirectToAction("Index", "Admin");

                        }
                        else
                        {

                            return RedirectToAction("Index", "Home");
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("", "Vui lòng xác nhận tài khoản theo đường liên kết được gửi qua Gmail.");
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không chính xác.");
                }
            }
            return View("Login");
        }


        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            string message = "";
            bool status = false;

            var account = db.Users.Where(a => a.UserMail == email).FirstOrDefault();

            if (account != null)
            {
                //send mail
                string resetCode = Guid.NewGuid().ToString();
                SendConfirmEmail(account.UserMail, resetCode, "ResetPassword");
                account.ResetPasswordCode = resetCode;

                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
                message = "Yêu cầu đặt lại mật khẩu thành công. Vui lòng kiểm tra email.";
            }
            else
            {
                message = "Không tìm thấy tài khoản";
            }
            ViewBag.Message = message;
            return View();
        }

        public ActionResult ResetPassword(string id)
        {
            var user = db.Users.Where(a => a.ResetPasswordCode == id).FirstOrDefault();
            if(user != null)
            {
                ResetPasswordModel model = new ResetPasswordModel();
                model.ResetCode = id;
                return View(model);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            ScryptEncoder encoder = new ScryptEncoder();
            var message = "";
            if (ModelState.IsValid)
            {
                var user = db.Users.Where(a => a.ResetPasswordCode == model.ResetCode).FirstOrDefault();
                if(user != null)
                {
                    user.PassWord = encoder.Encode(model.NewPassword);
                    user.ResetPasswordCode = "";
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                    message = "Đặt lại mật khẩu thành công";
                }
            }
            else
            {
                message = "Đã có lỗi xảy ra";

            }
            ViewBag.Message = message;
            return View(model);

        
        }

            [NonAction]
        public void SendConfirmEmail(string email, string activeCode, string emailFor = "VerifyAccount")
        {
            var verifyUrl = "/Login/"+emailFor+"/" + activeCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
                                            //sửa thành email của thầy
            var fromEmail = new MailAddress("nguyenducphap2000@gmail.com", "MonsterBook");
            var toEmail = new MailAddress(email);
                                    // sửa thành mật khẩu email của thầy
            var fromEmailPassword = "ducphap01122000";


            string subject = "";
            string body = "";

            if(emailFor == "VerifyAccount")
            {
                 subject = "Xác nhận tài khoản";
                 body = "<br/><br/> Tài khoản của bạn đã được tạo thành công. Bấm vào link để xác nhận tài khoản" +
                    "<br/></br><a href ='" + link + "'>" + link + "</a>";
            }
            else if (emailFor == "ResetPassword")
            {
                subject = "Đặt lại mật khẩu";
                body = "<br/><br/> Yêu cầu đặt lại mật khẩu. Bấm vào link bên dưới để đặt lại mật khẩu" +
                    "<br/></br><a href ='" + link + "'> Đặt lại mật khẩu</a>";
            }
            

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var messege = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true

            })
                smtp.Send(messege);

        }

        public ActionResult Logout()
        {
            Session[Common.CommonConstants.USER_SESSION] = null;
            return Redirect("/");
        }
    }
}