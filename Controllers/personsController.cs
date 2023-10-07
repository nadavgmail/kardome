using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Context;
using WebApplication1.Models;
using System.Data.Entity.Migrations;

namespace WebApplication1.Controllers
{
    public class personsController : Controller
    {
       
        private PersonManagmentContext db = new PersonManagmentContext();

        // GET: persons
        public ActionResult Index()
        {
            return View(db.Persons.ToList());
        }

        // GET: persons/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Persons.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: persons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: persons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TZ,Name,Email,Birthday,Gender,Phone")] Person person)
        {
            //check if the tz is unique width leading zero (000012 012 are the same tz)
            string CurrentTZ = person.TZ.TrimStart('0');
            if (db.Persons.AsEnumerable().Where(p => p.TZ.TrimStart('0') == CurrentTZ).Count() > 0)
            {
                ModelState.AddModelError("TZ", "person is already exsit");
                return View(person);
            }

            //Birthday cant be in the future
            if (person.Birthday >DateTime.Now)
            {
                ModelState.AddModelError("Birthday", "you have to pick higher date");
                return View();
            }
            if (ModelState.IsValid)
            {
                person.ID = Guid.NewGuid();
                db.Persons.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(person);
        }

        // GET: persons/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Persons.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        [HttpPost]
        public ActionResult DeleteConfirmedAajax(Guid? id)
        {
            JsonResult result = new JsonResult();
            if (id == null)
            {
                result.Data = new { status = "fail", deletedPerson = "null" };
                return result;
            }
           
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
           
            var person = db.Persons.Find(id);
            if (person == null)
            {
                result.Data = new { status = "fail", deletedPerson = id };
                return result;
            }
            db.Persons.Remove(person);
            db.SaveChanges();

            result.Data = new { status = "ok", deletedPerson = id };
            return result;

        }


     

        // POST: persons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "ID,TZ,Name,Email,Birthday,Gender,Phone")] Person person)
        {
            Person oldPerson = db.Persons.AsNoTracking().Where(p => p.ID == person.ID).FirstOrDefault();
            if (oldPerson == null)
            {
                return HttpNotFound();
            }
            string CurrentTZ = person.TZ.TrimStart('0');
            //if we change the tz and the tz  allready exsit in db if OldTZ = CurrentTZ we did not change the tz
            string OldTZ = oldPerson.TZ.TrimStart('0');
            if (OldTZ != CurrentTZ && db.Persons.AsEnumerable().Where(p => p.TZ.TrimStart('0') == CurrentTZ).Count() > 0)
            {
                ModelState.AddModelError("TZ", "person is already exsit");
                return View(person);
            }


            //Birthday cant be in the future
            if (person.Birthday > DateTime.Now)
            {
                ModelState.AddModelError("Birthday", "you have to pick higher date");
                return View();
            }
            if (ModelState.IsValid)
            {
               db.Persons.AddOrUpdate(person);
               // db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
        }

        // GET: persons/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Persons.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: persons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Person person = db.Persons.Find(id);
            db.Persons.Remove(person);
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
