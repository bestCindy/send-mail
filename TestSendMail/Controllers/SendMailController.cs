using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Http;
using System.Web.Mvc;
using TestSendMail.Services;

namespace TestSendMail.Controllers
{
    public class SendMailController : Controller
    {
        SendMail _services = new SendMail();
        [HttpGet]
        public void Test() {
            _services.SentMail();
        }
        public ActionResult SendMail()
        {
            return View();
        }
    }
}