using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirewoodMVC.Helper
{
	public class ProductList
	{

        public ProductList()
        {

        }

        public ProductList(int orderID, int productID, string name, decimal price)
        {
            OrderID = orderID;
            ProductID = productID;
            Name = name;
            Price = price;
        }

        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}