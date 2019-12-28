using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataInMemory;

namespace MyShop.Web.Controllers
{
    public class ProductManagerController : Controller
    {
        //ProductRepository context;
        //ProductCategoryRepository contextCategory;
        InMemoryRepository<Product> context;
        InMemoryRepository<ProductCategory> contextCategory;

        public ProductManagerController() {

            context = new InMemoryRepository<Product>();
            contextCategory = new InMemoryRepository<ProductCategory>();
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            string usermane = User.Identity.Name;
            string serverName = Server.MachineName;
            string clientIP = Request.UserHostAddress;
            DateTime dateStamp = HttpContext.Timestamp;
            List<Product> products = context.Collection().ToList();

            return View(products);
        }

        public ActionResult Create() {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.product = new Product();
            viewModel.Categories = contextCategory.Collection();
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {

            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else 
            {
                context.Insert(product);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string id)
        {

            Product product = context.Find(id);
            if (product == null) {
                return HttpNotFound();
            }
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.product = product;
            viewModel.Categories = contextCategory.Collection();
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Edit(Product product, string id)
        {
            Product productToEdit = context.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            if (!ModelState.IsValid) {
                return View(product);
            }
            productToEdit.Category = product.Category;
            productToEdit.Description = product.Description;
            productToEdit.Id = product.Id;
            productToEdit.Image = product.Image;
            productToEdit.Name = product.Name;
            productToEdit.Price = product.Price;

            context.Commit();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {

            Product productToDelete = context.Find(id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            return View(productToDelete);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {

            Product productToDelete = context.Find(id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            context.Delete(id);
            return RedirectToAction("Index");
        }
    }
}