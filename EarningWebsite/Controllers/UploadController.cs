using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EarningWebsite.Controllers
{
    public class UploadController : Controller
    {
        EarningWebsiteEntities db = new EarningWebsiteEntities();
        public ActionResult UploadDocument()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase files)
        {
            var file = Request.Files[0];

            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Images/"), fileName); file.SaveAs(path);

                try
                {
                    ImageData img = new ImageData();
                    img.imageName = file.FileName + ".jpg";
                    img.imageText = null;

                    db.ImageDatas.Add(img);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return RedirectToAction("UploadDocument");
        }
    }
}