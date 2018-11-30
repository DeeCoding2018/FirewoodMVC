using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FirewoodMVC.Helper;
using FirewoodMVC.Models;

namespace FirewoodMVC.Controllers
{
    public class Order_DetailsController : Controller
    {
        private FirewoodModel db = new FirewoodModel();

        private List<ProductList> productLists = new List<ProductList>();

        public void Init_ProductList()
        {
            productLists.Add(new ProductList(1, 1, "Mixedwood - One Fourth Cord", (decimal)180.00));
            productLists.Add(new ProductList(2,  1,   "Mixedwood - One Half Cord", (decimal)200.00));
            productLists.Add(new ProductList(3,  1,   "Mixedwood - One Cord", (decimal)350.00));
            productLists.Add(new ProductList(4,  1,   "Mixedwood - Two Cords", (decimal)700.00));
            productLists.Add(new ProductList(5,  2,   "Hardwood - One Fourth Cord", (decimal)200.00));
            productLists.Add(new ProductList(6,  2,   "Hardwood - One Half Cord", (decimal)300.00));
            productLists.Add(new ProductList(7,  2,   "Hardwood - One Cord", (decimal)500.00));
            productLists.Add(new ProductList(8,  2,   "Hardwood - Two Cords", (decimal)900.00));
            Session["ProductList"] = productLists;
        }


        // GET: Order_Details
        public ActionResult Index()
        {
            var order_Details = db.Order_Details.Include(o => o.Order).Include(o => o.Product);
            return View(order_Details.ToList());
        }

        // GET: Order_Details/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Details order_Details = db.Order_Details.Find(id);
            if (order_Details == null)
            {
                return HttpNotFound();
            }
            return View(order_Details);
        }

        // GET: Order_Details/Create
        public ActionResult Create()
        {
            ViewBag.Order_Id = new SelectList(db.Orders, "Order_Id", "Order_Id");
            ViewBag.Product_ID = new SelectList(db.Products, "Product_Id", "Product_Name");
            return View();
        }

        // POST: Order_Details/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Order_Id,Product_ID,Unit_Price,Quantity")] Order_Details order_Details)
        {
            if (ModelState.IsValid)
            {
                // Error, problem with foreign keys
                db.Order_Details.Add(order_Details);
                db.SaveChanges();

                // TRY CHECKING IN RUNNING IN CONTROLLER GIVES BETTER VALUES
                int pid = order_Details.Product_ID;
                int id = order_Details.Order_Id;
                //string query = "SELECT [Price] FROM [Firewood].[dbo].[Products] WHERE[Product_Id] = " + pid;
                //var price = from p in db.Products where p.Product_Id == pid select p.Price;
                //var dec = db.Products.Where(x => x.Product_Id == pid).Select(x => x.Price);
                // VALUE DOES NOT SHOW UP CORRECTLY
                Init_ProductList();
                return RedirectToAction("Edit", "Order_Details", new { id = order_Details.Order_Id, pid = order_Details.Product_ID });
            }

            ViewBag.Order_Id = new SelectList(db.Orders, "Order_Id", "Order_Id", order_Details.Order_Id);
            ViewBag.Product_ID = new SelectList(db.Products, "Product_Id", "Product_Name", order_Details.Product_ID);
            return View(order_Details);
        }

        // GET: Order_Details/Edit/5
        public ActionResult Edit(int? id, int? pid)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Details order_Details = db.Order_Details.Find(id, pid);
            if (order_Details == null)
            {
                return HttpNotFound();
            }
            ViewBag.Order_Id = new SelectList(db.Orders, "Order_Id", "Order_Id", order_Details.Order_Id);
            ViewBag.Product_ID = new SelectList(db.Products, "Product_Id", "Product_Name", order_Details.Product_ID);
            return View(order_Details);
        }

        // POST: Order_Details/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Order_Id,Product_ID,Unit_Price,Quantity")] Order_Details order_Details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order_Details).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Order_Id = new SelectList(db.Orders, "Order_Id", "Shipping_Address", order_Details.Order_Id);
            ViewBag.Product_ID = new SelectList(db.Products, "Product_Id", "Product_Name", order_Details.Product_ID);
            return View(order_Details);
        }

        // GET: Order_Details/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Details order_Details = db.Order_Details.Find(id);
            if (order_Details == null)
            {
                return HttpNotFound();
            }
            return View(order_Details);
        }

        // POST: Order_Details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order_Details order_Details = db.Order_Details.Find(id);
            db.Order_Details.Remove(order_Details);
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
