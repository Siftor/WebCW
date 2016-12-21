using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebCW.Models;
using Microsoft.AspNet.Identity; 

namespace WebCW.Controllers
{
    public class AnnsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Anns
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BuildAnnsTable()
        {
           
            return PartialView("_AnnTable",db.Announcements.ToList());
        }

        // GET: Anns/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ann ann = db.Announcements.Find(id);
            if (ann == null)
            {
                return HttpNotFound();
            }
            return View(ann);
        }

        // GET: Anns/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Anns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "id,description,isSeen")] Ann ann)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault(
                    x => x.Id == currentUserId);
                ann.User = currentUser;
                db.Announcements.Add(ann);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ann);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult AJAXCreate([Bind(Include = "id,description")] Ann ann)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault(
                    x => x.Id == currentUserId);
                ann.User = currentUser;
                ann.isSeen = false;
                db.Announcements.Add(ann);
                db.SaveChanges();
                
            }

            return PartialView("_AnnTable", db.Announcements.ToList());
        }





        // GET: Anns/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ann ann = db.Announcements.Find(id);

            if (ann == null)
            {
                return HttpNotFound();
            }
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser CurrentUser = db.Users.FirstOrDefault
                (x => x.Id == currentUserId);

            if (ann.User != CurrentUser)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(ann);
        }

        // POST: Anns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,description,isSeen")] Ann ann)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ann).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ann);
        }

        [HttpPost]
        [Authorize]

        public ActionResult AJAXEdit(int? id, bool value)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ann ann = db.Announcements.Find(id);
            if (ann == null)
            {
                return HttpNotFound();
            }
            else
            {
                ann.isSeen = value;
                db.Entry(ann).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("_AnnTable", db.Announcements.ToList());
            }
          
        }



        // GET: Anns/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ann ann = db.Announcements.Find(id);
            if (ann == null)
            {
                return HttpNotFound();
            }
            return View(ann);
        }

        // POST: Anns/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ann ann = db.Announcements.Find(id);
            db.Announcements.Remove(ann);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
