using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers
{
    public class StudentOperationsController : Controller
    {
        private StudentEntities db = new StudentEntities();

        // GET: /StudentOperations/
        public ActionResult Index()
        {
            return View(db.StudentDatas.ToList());
        }

        // GET: /StudentOperations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentData studentdata = db.StudentDatas.Find(id);
            if (studentdata == null)
            {
                return HttpNotFound();
            }
            return View(studentdata);
        }

        // GET: /StudentOperations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /StudentOperations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="StudentID,Name,Class")] StudentData studentdata)
        {
            if (ModelState.IsValid)
            {
                db.StudentDatas.Add(studentdata);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(studentdata);
        }

        // GET: /StudentOperations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentData studentdata = db.StudentDatas.Find(id);
            if (studentdata == null)
            {
                return HttpNotFound();
            }
            return View(studentdata);
        }

        // POST: /StudentOperations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="StudentID,Name,Class")] StudentData studentdata)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentdata).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studentdata);
        }

        // GET: /StudentOperations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentData studentdata = db.StudentDatas.Find(id);
            if (studentdata == null)
            {
                return HttpNotFound();
            }
            return View(studentdata);
        }

        // POST: /StudentOperations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentData studentdata = db.StudentDatas.Find(id);
            db.StudentDatas.Remove(studentdata);
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
