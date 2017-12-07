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
    public class OrdersController : Controller
    {
        private IntexContext db = new IntexContext();
        

        [Authorize]
        public ActionResult IndexProgress(int? id)
        {
            IEnumerable<Order> order =
                db.Database.SqlQuery<Order>("SELECT [dbo].[Order].orderNumber, [dbo].[Order].orderDate, " +
                " Order_Status.orderStatusDesc, [dbo].[Order].orderDateDue, " +
                " CASE WHEN[dbo].[Order].orderStatusID = 1 THEN 0.0 " +
                " WHEN[dbo].[Order].orderStatusID = 2 THEN 10.0 " +
                " WHEN[dbo].[Order].orderStatusID = 3 THEN 25.0 " +
                " WHEN[dbo].[Order].orderStatusID = 4 THEN 50.0 " +
                " WHEN[dbo].[Order].orderStatusID = 5 THEN 80.0" +
                " WHEN[dbo].[Order].orderStatusID = 6 THEN 100.0 END as 'orderPctCompletion', " +
                " [dbo].[Order].orderCustomerComment, " +
                " CASE WHEN[dbo].[Order].orderDeliveryPaper = '0' THEN 'No' " +
                " WHEN[dbo].[Order].orderDeliveryPaper = '1' THEN 'Yes' END as 'orderDeliveryPaper', " +
                " CASE WHEN[dbo].[Order].orderDeliveryElectronic = '0' THEN 'No' " +
                " WHEN[dbo].[Order].orderDeliveryElectronic = '1' THEN 'Yes' END as 'orderDeliveryElectronic', " +
                " [dbo].[Order].orderAdvancePayment, [dbo].[Order].clientID " +
                " FROM[dbo].[Order] " +
                " inner join Order_Status on[dbo].[Order].orderStatusID = Order_Status.orderStatusID " +
                " inner join Representative on Representative.clientID = [dbo].[Order].clientID " +
                " WHERE Representative.repID = " + id + " and Order_Status.orderStatusDesc != 'Completed' "
                );

            ViewBag.myid = id;

            return View(order.ToList());
        }

        [Authorize]
        public ActionResult IndexCompleted(int? id)
        {
            IEnumerable<Order> order =
                db.Database.SqlQuery<Order>("SELECT [dbo].[Order].orderNumber, [dbo].[Order].orderDate, " +
                " Order_Status.orderStatusDesc, [dbo].[Order].orderDateDue, " +
                " CASE WHEN[dbo].[Order].orderStatusID = 1 THEN 0.0 " +
                " WHEN[dbo].[Order].orderStatusID = 2 THEN 10.0 " +
                " WHEN[dbo].[Order].orderStatusID = 3 THEN 25.0 " +
                " WHEN[dbo].[Order].orderStatusID = 4 THEN 50.0 " +
                " WHEN[dbo].[Order].orderStatusID = 5 THEN 80.0" +
                " WHEN[dbo].[Order].orderStatusID = 6 THEN 100.0 END as 'orderPctCompletion', " +
                " [dbo].[Order].orderCustomerComment, " +
                " CASE WHEN[dbo].[Order].orderDeliveryPaper = '0' THEN 'No' " +
                " WHEN[dbo].[Order].orderDeliveryPaper = '1' THEN 'Yes' END as 'orderDeliveryPaper', " +
                " CASE WHEN[dbo].[Order].orderDeliveryElectronic = '0' THEN 'No' " +
                " WHEN[dbo].[Order].orderDeliveryElectronic = '1' THEN 'Yes' END as 'orderDeliveryElectronic', " +
                " [dbo].[Order].orderAdvancePayment, [dbo].[Order].clientID " +
                " FROM[dbo].[Order] " +
                " inner join Order_Status on[dbo].[Order].orderStatusID = Order_Status.orderStatusID " +
                " inner join Representative on Representative.clientID = [dbo].[Order].clientID " +
                " WHERE Representative.repID = " + id + " and Order_Status.orderStatusDesc = 'Completed' "
                );

            ViewBag.myid = id;

            return View(order.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        [Authorize]
        public ActionResult Create()
        {

            
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "orderNumber,orderDate,orderStatusID,orderDateDue,orderPctCompletion,orderCustomerComment,orderDeliveryPaper,orderDeliveryElectronic,orderAdvancePayment,clientID")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();

                db.Database.SqlQuery<Order>("SET IDENTITY_INSERT[dbo].[Order] ON " +
                    " insert into[dbo].[Order] (orderNumber, orderDate, orderStatusID, clientID) " +
                    " SELECT(max(orderNumber) + 1), GETDATE(), 1, [dbo].[Order].clientID " +
                    " FROM[dbo].[Order] " +
                    " inner join Representative on Representative.clientID = Representative.clientID " +
                    " where[dbo].[Order].clientID =  " + order.clientID +
                    " group by[dbo].[Order].clientID " +
                    " SET IDENTITY_INSERT[dbo].[Order] ON");

                ViewBag.repid = db.Database.SqlQuery<Order>("SELECT distinct Representative.repID " +
                    " FROM[dbo].[Order] " +
                    " inner join Representative on Representative.clientID = [dbo].[Order].clientID " +
                    " where[dbo].[Order].clientID = " + order.clientID);

                return RedirectToAction("IndexProgress", "Orders", new { id = ViewBag.repid });
            }

            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "orderNumber,orderDate,orderStatusID,orderDateDue,orderPctCompletion,orderCustomerComment,orderDeliveryPaper,orderDeliveryElectronic,orderAdvancePayment,clientID")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
