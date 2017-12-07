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
    public class TasksController : Controller
    {
        private IntexContext db = new IntexContext();

        [Authorize]
        public ActionResult Index(int id)
        {
            IEnumerable<Tasks> tasks =
                db.Database.SqlQuery<Tasks>("SELECT tasks.taskID, Concat('Order ' , [dbo].[Order].orderNumber , ', ' ,'Test Tube ' , Test_Tube.tubeNumber , ', ', 'TaskID ' , Tasks.taskID) as 'taskName', " +
                " Concat('Work on Test Tube ', Test_Tube.tubeNumber, ', ', 'Under Work Oder ', [dbo].[Order].orderNumber, ', ', 'Customer Comments = ', [dbo].[Order].orderCustomerComment) as 'taskDesc', " +
                "  Tasks.tubeNumber, Tasks.empID " +
                " FROM Tasks " +
                " inner join Test_Tube on Test_Tube.sampleTestCode = Tasks.tubeNumber " +
                " inner join Sample_Test on Sample_Test.sampleTestCode = Test_Tube.sampleTestCode " +
                " inner join Sample on Sample.sampleSequenceCode = Sample_Test.sampleSequenceCode and Sample.sampleLTnum = Sample_Test.sampleLTnum " +
                " inner join Assay on Assay.assayID = Sample.assayID " +
                " inner join Customer_Assay on Customer_Assay.assayID = Assay.assayID " +
                " inner join[dbo].[Order] on[dbo].[Order].orderNumber = Customer_Assay.orderNumber " +
                " where Tasks.empID = " + id + " and [dbo].[Order].orderPctCompletion != 100");

            ViewBag.empid = id;

            return View(tasks.ToList());
        }

        [Authorize]
        public ActionResult IndexCompleted(int id)
        {
            IEnumerable<Tasks> tasks =
                db.Database.SqlQuery<Tasks>("SELECT tasks.taskID, Concat('Order ' , [dbo].[Order].orderNumber , ', ' ,'Test Tube ' , Test_Tube.tubeNumber , ', ', 'TaskID ' , Tasks.taskID) as 'taskName', " +
                " Concat('Work on Test Tube ', Test_Tube.tubeNumber, ', ', 'Under Work Oder ', [dbo].[Order].orderNumber, ', ', 'Customer Comments = ', [dbo].[Order].orderCustomerComment) as 'taskDesc', " +
                "  Tasks.tubeNumber, Tasks.empID " +
                " FROM Tasks " +
                " inner join Test_Tube on Test_Tube.sampleTestCode = Tasks.tubeNumber " +
                " inner join Sample_Test on Sample_Test.sampleTestCode = Test_Tube.sampleTestCode " +
                " inner join Sample on Sample.sampleSequenceCode = Sample_Test.sampleSequenceCode and Sample.sampleLTnum = Sample_Test.sampleLTnum " +
                " inner join Assay on Assay.assayID = Sample.assayID " +
                " inner join Customer_Assay on Customer_Assay.assayID = Assay.assayID " +
                " inner join[dbo].[Order] on[dbo].[Order].orderNumber = Customer_Assay.orderNumber " +
                " where Tasks.empID = " + id + " and [dbo].[Order].orderPctCompletion = 100");

            ViewBag.empid = id;

            return View(tasks.ToList());
        }

        // GET: Tasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tasks tasks = db.Tasks.Find(id);
            if (tasks == null)
            {
                return HttpNotFound();
            }
            return View(tasks);
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "taskID,taskName,taskDesc,tubeNumber,empID")] Tasks tasks)
        {
            if (ModelState.IsValid)
            {
                db.Tasks.Add(tasks);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tasks);
        }

        // GET: Tasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tasks tasks = db.Tasks.Find(id);
            if (tasks == null)
            {
                return HttpNotFound();
            }
            return View(tasks);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "taskID,taskName,taskDesc,tubeNumber,empID")] Tasks tasks)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tasks).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tasks);
        }

        // GET: Tasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tasks tasks = db.Tasks.Find(id);
            if (tasks == null)
            {
                return HttpNotFound();
            }
            return View(tasks);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tasks tasks = db.Tasks.Find(id);
            db.Tasks.Remove(tasks);
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
