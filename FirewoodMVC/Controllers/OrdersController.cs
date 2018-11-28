using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FirewoodMVC.Models;

namespace FirewoodMVC.Controllers
{
    public class OrdersController : Controller
    {
        private FirewoodModel db = new FirewoodModel();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Customer);
            return View(orders.ToList());
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
        public ActionResult Create()
        {
            ViewBag.Customer_Id = new SelectList(db.Customers, "Customer_Id", "User_Name");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Order_Id,Customer_Id,Order_Date,Shipped_Date,Shipping_Address,City,State,Zip_Code")] Order order)
        {
            if (ModelState.IsValid)
            {
                if (Session["User"] == null)
                {
                    Order tempOrder = new Order();
                    tempOrder.Order_Date = order.Order_Date;
                    tempOrder.Shipped_Date = order.Shipped_Date;
                    tempOrder.Shipping_Address = order.Shipping_Address;
                    tempOrder.City = order.City;
                    tempOrder.State = order.State;
                    tempOrder.Zip_Code = order.Zip_Code;
                    Session["Temp"] = tempOrder;
                    Session["FromOrder"] = new bool[2] { true, false };
                    return RedirectToAction("Login", "Home");
                }
                // AFTER login repopulate field
                db.Orders.Add(order);
                db.SaveChanges();

                Session["Order_Count"] = db.Orders.Count();
                return RedirectToAction("Create", "Order_Details");
            }

            ViewBag.Customer_Id = new SelectList(db.Customers, "Customer_Id", "User_Name", order.Customer_Id);
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
            ViewBag.Customer_Id = new SelectList(db.Customers, "Customer_Id", "User_Name", order.Customer_Id);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Order_Id,Customer_Id,Order_Date,Shipped_Date,Shipping_Address,City,State,Zip_Code")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Customer_Id = new SelectList(db.Customers, "Customer_Id", "User_Name", order.Customer_Id);
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
