using ProjectPRNver2._0.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectPRNver2._0.Controllers
{
    public class ContactController : Controller
    {
        ProjectPRNDbContext db = new ProjectPRNDbContext();
        // GET: Contact
        public ActionResult Index()
        {
            var contact = db.Contacts.ToList();
            return View(contact);
        }
    }
}