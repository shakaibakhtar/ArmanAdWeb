using EarningWebsite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EarningWebsite.Controllers
{
    public class HomeController : Controller
    {
        EarningWebsiteEntities db = new EarningWebsiteEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(SignUpVM user)
        {
            try
            {
                UserData userData = new UserData();
                userData.name = user.name;
                userData.last_name = user.last_name;
                userData.email = user.email;
                userData.password = user.password;
                userData.phone = user.phone;
                userData.dob = user.dob;
                userData.payment_method = user.payment_method;

                db.UserDatas.Add(userData);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View("Error");
            }
            return RedirectToAction("Index");
        }

        public ActionResult SignIn()
        {
            return View("SignUp");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(SignInVM user)
        {
            try
            {
                var usr = db.UserDatas.SingleOrDefault(x =>
               x.email == user.email && x.password == user.password);

                if (usr != null)
                {
                    Session["UserName"] = usr.email;
                    Session["UserType"] = usr.password;
                    return RedirectToAction("Index");
                }

                return View("SignUp");
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View("Error");
            }
            return RedirectToAction("Index");
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