using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InstutiteOfFineArt.Core.Model;

namespace InstutiteFineArt.Areas.Admin.Controllers
{

    [Authorize(Roles = "Administrator")]
    public class UserClassesController : Controller
    {
        private InstutiteFineArtDbContext db = new InstutiteFineArtDbContext();

        // GET: Admin/UserClasses
        public ActionResult Index()
        {
            return View(db.UserClasses.ToList());
        }

        // GET: Admin/UserClasses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserClass userClass = db.UserClasses.Find(id);
            if (userClass == null)
            {
                return HttpNotFound();
            }
            return View(userClass);
        }

        // GET: Admin/UserClasses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/UserClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] UserClass userClass)
        {
            if (ModelState.IsValid)
            {
                db.UserClasses.Add(userClass);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userClass);
        }

        // GET: Admin/UserClasses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserClass userClass = db.UserClasses.Find(id);
            if (userClass == null)
            {
                return HttpNotFound();
            }
            return View(userClass);
        }

        // POST: Admin/UserClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClassId,Name")] UserClass userClass)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userClass).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userClass);
        }

        // GET: Admin/UserClasses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserClass userClass = db.UserClasses.Find(id);
            if (userClass == null)
            {
                return HttpNotFound();
            }
            return View(userClass);
        }

        // POST: Admin/UserClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserClass userClass = db.UserClasses.Find(id);
            db.UserClasses.Remove(userClass);
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
