using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Intex2017.DAL;
using Intex2017.Models;

namespace Intex2017.Controllers
{
    public class RepresentativesController : Controller
    {
        private IntexContext db = new IntexContext();

        [Authorize]
        public ActionResult repHome(int id)
        {
            List<Representative> myRep = new List<Representative>();
            myRep = db.Representatives.ToList();

            Representative representative = db.Representatives.Find(id);

            ViewBag.repID = myRep[id].repID;
            ViewBag.repFirstName = myRep[id].repFirstName;
            ViewBag.repLastName = myRep[id].repLastName;
            ViewBag.repPhoneNumber = myRep[id].repPhoneNumber;
            ViewBag.repEmail = myRep[id].repEmail;

            return View();
        }

        // GET: Representatives
        [Authorize]
        public ActionResult Index(int id)
        {
            IEnumerable<Representative> representative =
                db.Database.SqlQuery<Representative>("SELECT Representative.repID, Representative.repFirstName, " +
                " Representative.repLastName, Representative.repPhoneNumber, Representative.repEmail, " +
                " Representative.repUserName, Representative.repPasswordHash, Representative.clientID " +
                "FROM Representative " +
                "WHERE Representative.repID = " + id);
            
            return View(representative.FirstOrDefault());
        }

        // GET: Representatives/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Representative representative = db.Representatives.Find(id);
            if (representative == null)
            {
                return HttpNotFound();
            }
            return View(representative);
        }

        // GET: Representatives/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Representatives/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "repID,repFirstName,repLastName,repPhoneNumber,repEmail,repUserName,repPasswordHash,clientID")] Representative representative)
        {
            if (ModelState.IsValid)
            {
                db.Representatives.Add(representative);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(representative);
        }

        // GET: Representatives/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                Representative representative = db.Representatives.Find(id);

                return View(representative);
            }
            else
            {
                return RedirectToAction("Index", "Representatives");
            }
        }

        // POST: Representatives/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "repID,repFirstName,repLastName,repPhoneNumber,repEmail,repUserName,repPasswordHash,clientID")] Representative representative)
        {
            if (ModelState.IsValid)
            {
                db.Entry(representative).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(representative);
        }

        // GET: Representatives/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Representative representative = db.Representatives.Find(id);
            if (representative == null)
            {
                return HttpNotFound();
            }
            return View(representative);
        }

        // POST: Representatives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Representative representative = db.Representatives.Find(id);
            db.Representatives.Remove(representative);
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
