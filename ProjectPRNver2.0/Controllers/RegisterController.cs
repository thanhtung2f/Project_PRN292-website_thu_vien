using Model.DAO;
using ProjectPRNver2._0.EF;
using ProjectPRNver2._0.Common;
using ProjectPRNver2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Scrypt;

namespace ProjectPRNver2._0.Controllers
{
    public class RegisterController : Controller
    {

        // GET: Register
        [HttpGet]
        public ActionResult Register()
        {
            Session[Common.CommonConstants.USER_SESSION] = null;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            ScryptEncoder encoder = new ScryptEncoder();
            bool Status = true;
            string message = "";
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (dao.CheckUserName(model.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                else if (dao.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }
                else
                {
                    var user = new User();
                    user.UserName = model.UserName;
                    user.PassWord = encoder.Encode(model.Password);
                    user.FullName = model.FullName;
                    user.UserPhone = model.Phone;
                    user.UserMail = model.Email;
                    user.Address = model.Address;
                    user.Status = false;
                    user.CreatedDate = DateTime.Now;
                    user.Roleid = 3;
                    user.ActiveCode = Guid.NewGuid();
                    var result = dao.Insert(user);
                    if(result > 0)
                    {
                        model = new RegisterModel();
                        SendConfirmEmail(user.UserMail, user.ActiveCode.ToString());
                        message = "Đăng ký thành công, vui lòng kiểm tra Email để xác nhận tài khoản";
                        Status = true;
                        ViewBag.Success = "Đăng ký thành công, vui lòng kiểm tra Email để xác nhận tài khoản";
                        
                        
                    }
                    else
                    {
                        message= "Đăng ký thất bại";
                    }
                }
            }
            ViewBag.Mesage = message;
            ViewBag.Status = Status;
            return View(model);
        }

        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            using (ProjectPRNDbContext dc = new ProjectPRNDbContext())
            {
                dc.Configuration.ValidateOnSaveEnabled = false;
                var v = dc.Users.Where(a => a.ActiveCode == new Guid(id)).FirstOrDefault();
                if (v != null)
                {
                    v.Status = true;
                    dc.SaveChanges();
                    Status = true;
                }
                else
                {
                    ViewBag.Message = "Yêu cầu không hợp lệ";
                }
            }
            ViewBag.Status = true;
            return View();
        }

        [NonAction]
        public void SendConfirmEmail(string email, string activeCode)
        {
            var verifyUrl = "/Register/VerifyAccount/" + activeCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
                                            // sửa thành email của thầy
            var fromEmail = new MailAddress("nguyenducphap2000@gmail.com", "MonsterBook");
            var toEmail = new MailAddress(email);
                                    //sửa thành mật khẩu email của thầy
            var fromEmailPassword = "ducphap01122000";
            string subject = "Xác nhận tài khoản";
            string body = "<br/><br/> Tài khoản của bạn đã được tạo thành công. Bấm vào link để xác nhận tài khoản" +
                "<br/></br><a href ='"+link+"'>" +link+"</a>";

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
    }
}