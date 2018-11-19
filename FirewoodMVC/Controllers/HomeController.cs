using FirewoodMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace FirewoodMVC.Controllers
{
    public class HomeController : Controller
    {
        private FirewoodModel db = new FirewoodModel();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Order()
        {
            ViewBag.Message = "Your order page.";

            return View();
        }

        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Customer customer)
        {
            var search = db.Customers.Single(x => x.User_Name == customer.User_Name);
            if (search != null)
            {
                using (MD5 mD5Hash = MD5.Create())
                {
                    string customerPassword = customer.Password;
                    string customerPasswordHash = Helper.Hashing.GetMD5Hash(mD5Hash, customerPassword);

                    var storedPasswordHash = search.Password.TrimEnd(); 
                    if(Helper.Hashing.VerifyHash(mD5Hash, customerPassword, storedPasswordHash))
                    {
                        //create session info now
                        Session["User"] = customer;
                        ViewBag.Username = customer.User_Name;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Username = "Incorrect password";
                    }
                }
            }
            else
            {
                ViewBag.Username = "No";
            }

            return View();
        }


        //public ActionResult Registration()
        //{
        //    ViewBag.Message = "Your Registration page.";

        //    return View();
        //}

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Customer_Id,User_Name,Password,First_Name,Last_Name,Address,Zip_Code,Phone_Number,Email_Address")] Customer customer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (CheckUsernameAvailable(customer.User_Name))
        //        {
        //            db.Customers.Add(customer);
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            ViewBag.ErrorUserName = "User Name already in use";
        //            return RedirectToAction("Index");
        //        }
        //    }

        //    return View(customer);
        //}

        public bool CheckUsernameAvailable(string username)
        {
            var SearchData = db.Customers.Where(x => x.User_Name == username).Single();
            if (SearchData != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}