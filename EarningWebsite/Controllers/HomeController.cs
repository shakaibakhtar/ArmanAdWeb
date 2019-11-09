using EarningWebsite.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace EarningWebsite.Controllers
{
    public class HomeController : Controller
    {
        EarningWebsiteEntities db = new EarningWebsiteEntities();
        public ActionResult Index()
        {
            try
            {
                if (Session["UserScore"].ToString() != null)
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("SignIn");
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
                var usr = db.UserDatas.SingleOrDefault(x => x.email == user.email && x.password == user.password);

                if (usr != null)
                {
                    Session["UserID"] = usr.id;
                    Session["UserName"] = usr.name;
                    Session["UserPhone"] = usr.phone;
                    Session["UserScore"] = usr.score;

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

        public ActionResult LoadImage()
        {
            try
            {
                if (Session["UserName"].ToString() != null)
                {
                    string dir = HostingEnvironment.ApplicationPhysicalPath + "/Images/";
                    string[] files;
                    int numFiles;
                    files = Directory.GetFiles(dir);
                    numFiles = files.Length;

                    string fileName = new Random().Next(1, numFiles).ToString();
                    ViewBag.imageName = fileName + ".jpg";

                    return View();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("SignIn");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DataEntry(DataEntryVM image)
        {
            try
            {
                if (Session["UserName"].ToString() != null)
                {
                    string name = ViewBag.imageName;
                    var img = db.ImageDatas.FirstOrDefault(x => x.imageName == name);
                    if (image.imageText.Equals(img.imageText))
                    {
                        var usr = db.UserDatas.FirstOrDefault(x => x.id == (int)Session["id"]);
                        usr.score += 10;
                        db.SaveChanges();
                    }
                    else
                    {
                        return RedirectToAction("LoadImage");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("LoadImage");
            }
            return View();
        }

        public ActionResult SignOut()
        {
            Session.Abandon();
            Session.Clear();

            return RedirectToAction("SignIn");
        }

        public ActionResult Withdraw()
        {
            try
            {
                if (Session["UserID"] != null)
                {
                    WithdrawVM w = new WithdrawVM();
                    w.UserID = Convert.ToInt32(Session["UserID"]);
                    w.UserName = Session["UserName"].ToString();
                    w.UserScore = Convert.ToInt32(Session["UserScore"]);

                    return View(w);
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("SignIn");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Withdraw(WithdrawVM vM)
        {
            try
            {
                if (Session["UserName"] != null)
                {
                    Withdraw withdraw = new Withdraw();

                    withdraw.UserID = Convert.ToInt32(Session["UserID"]);
                    withdraw.phone = Session["UserPhone"].ToString();
                    withdraw.date = DateTime.Now;
                    withdraw.amount = vM.PointsToWithdraw;

                    db.Withdraws.Add(withdraw);

                    return RedirectToAction("Index");
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View();
        }
    }
}