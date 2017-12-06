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
    public class EmployeesController : Controller
    {
        private IntexContext db = new IntexContext();

        [Authorize]
        public ActionResult empHome(int id)
        {
            IEnumerable<Employee> employee =
                db.Database.SqlQuery<Employee>("SELECT Employee.empID, Employee.empFirstName, Employee.empLastName, " +
                "Employee.empSSNHash,Employee.empWage, Employee.empPassword, Employee.empEmail, Employee.empPassword " +
                "FROM Employee " +
                "Where Employee.empID = " + id);

            return View(employee.FirstOrDefault());
        }

        // GET: Employees
        [Authorize]
        public ActionResult Index(int id)
        {
            IEnumerable<Employee> employee =
                db.Database.SqlQuery<Employee>("SELECT Employee.empID, Employee.empFirstName, Employee.empLastName, " +
                "Employee.empSSNHash,Employee.empWage, Employee.empPassword, Employee.empEmail, Employee.empPassword " +
                "FROM Employee " +
                "Where Employee.empID = " + id);

            return View(employee.FirstOrDefault());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "empID,empFirstName,empLastName,empSSNHash,empWage,empEmail,empPassword")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
               Employee employee = db.Employees.Find(id);

                return View(employee);
            }
            else
            {
                return RedirectToAction("Index", "Employees");
            }

        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "empID,empFirstName,empLastName,empSSNHash,empWage,empEmail,empPassword")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = employee.empID });
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
