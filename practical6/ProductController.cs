using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductCatalogApp.Models; // Ye line Model (Product) se connect karti hai

namespace ProductCatalogApp.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            // Practical ke liye ek Dummy Product List bana rahe hain
            List<Product> productList = new List<Product>
            {
                new Product { Id = 101, Name = "Laptop", Price = 45000, Category = "Electronics" },
                new Product { Id = 102, Name = "Wireless Mouse", Price = 800, Category = "Accessories" },
                new Product { Id = 103, Name = "Mechanical Keyboard", Price = 2500, Category = "Accessories" },
                new Product { Id = 104, Name = "Office Chair", Price = 5500, Category = "Furniture" }
            };

            // Is list ko HTML View mein bhej rahe hain
            return View(productList);
        }
    }
}