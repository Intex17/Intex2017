using Intex2017.DAL;
using Intex2017.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Intex2017.Controllers
{
    public class HomeController : Controller
    {
        private IntexContext db = new IntexContext();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form, bool rememberMe = false)
        {
            String email = form["Email address"].ToString();
            String password = form["Password"].ToString();
            ClientLink rList = new ClientLink();
            List<Representative> myRep = new List<Representative>();
            rList.Representative = db.Representatives.ToList();
            myRep = db.Representatives.ToList();
            for (int iCount = 0; iCount < myRep.Count; iCount++)
            {
                if (string.Equals(email, myRep[iCount].repEmail) && (string.Equals(password, myRep[iCount].repPasswordHash)))
                {
                    FormsAuthentication.SetAuthCookie(email, rememberMe);
                    return RedirectToAction("Index", "Representatives", new { id = myRep[iCount].repID });
                }
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}