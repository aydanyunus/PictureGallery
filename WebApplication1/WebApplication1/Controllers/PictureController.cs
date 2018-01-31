using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.IO;

namespace WebApplication1.Controllers
{
    public class PictureController : Controller
    {

        PictureGalleryEntities db = new PictureGalleryEntities();
        // GET: Picture
        public ActionResult Index()
        {
            ViewBag.AllPictures = db.Pictures.Where(pc => pc.Status == 1).ToList();
            return View();
        }

        // GET: Picture/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Picture/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Picture/Create
        [HttpPost]
        public ActionResult Create(IEnumerable <HttpPostedFileBase> Photo, Picture pic)
        {

            try
            {
                foreach(HttpPostedFileBase picture in Photo)
                {
                    if (picture.ContentLength > 0 && picture.ContentLength < 3 * 1024 * 1024)
                    {
                        if (picture.ContentType.ToLower() == "image/jpeg" ||
                            picture.ContentType.ToLower() == "image/jpg" ||
                            picture.ContentType.ToLower() == "image/png" ||
                            picture.ContentType.ToLower() == "image/gif")
                        {
                            DateTime dt = DateTime.Now;
                            var dtformat = dt.Year + "_" + dt.Month + "_" + dt.Day + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond;
                            var filename = dtformat + Path.GetFileName(picture.FileName);
                            var path = Path.Combine(Server.MapPath("~/Uploads/Pictures/"), filename);

                            picture.SaveAs(path);                           
                            pic.Name = filename;
                            pic.Status = 1;
                            db.Pictures.Add(pic);
                            db.SaveChanges();
                            return RedirectToAction("Index");

                        }
                        else
                        {
                            ViewBag.error = "yalniz yuxaridaki formatlarda ola biler.";
                        }
                    }
                    else
                    {
                        ViewBag.error = "3 MBdan artiq olmaz.";
                    }
                    return View();
                }
              
            }
            catch
            {
                ViewBag.error = "unexpected error.";
            }
            return View();
        }

        // GET: Picture/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Picture/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Picture/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Picture/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
