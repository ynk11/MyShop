using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.DataInMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.Web.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        //ProductCategoryRepository context;
        IRepository<ProductCategory> context;

        public ProductCategoryManagerController(IRepository<ProductCategory> _context)
        {

            context = _context;
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            string usermane = User.Identity.Name;
            string serverName = Server.MachineName;
            string clientIP = Request.UserHostAddress;
            DateTime dateStamp = HttpContext.Timestamp;
            List<ProductCategory> productCategories = context.Collection().ToList();

            return View(productCategories);
        }

        public ActionResult Create()
        {

            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);
        }
        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {

            if (!ModelState.IsValid)
            {
                return View(productCategory);
            }
            else
            {
                context.Insert(productCategory);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string id)
        {

            ProductCategory productCategory = context.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            return View(productCategory);
        }
        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory, string id)
        {
            ProductCategory productToEdit = context.Find(id);
            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(productToEdit);
            }
            productToEdit.Category = productCategory.Category;
            productToEdit.Id = productCategory.Id;
         

            context.Commit();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {

            ProductCategory productToDelete = context.Find(id);
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

            ProductCategory productToDelete = context.Find(id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            context.Delete(id);
            return RedirectToAction("Index");
        }
    }
}