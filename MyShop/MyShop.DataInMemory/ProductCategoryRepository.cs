using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataInMemory
{
   public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;

        List<ProductCategory> ProductCategories;

        public ProductCategoryRepository()
        {

            ProductCategories = cache["ProductCategories"] as List<ProductCategory>;
            if (ProductCategories == null)
            {
                ProductCategories = new List<ProductCategory>();
            }
        }

        public void Commit()
        {

            cache["ProductCategories"] = ProductCategories;
        }

        public void Insert(ProductCategory productCategory)
        {

            ProductCategories.Add(productCategory);
        }

        public void Update(ProductCategory productCategory)
        {

            ProductCategory productToUpdate = ProductCategories.Find(x => x.Id == productCategory.Id);

            if (productToUpdate != null)
            {

                productToUpdate = productCategory;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        public ProductCategory Find(string id)
        {

            ProductCategory productCategory = ProductCategories.Find(x => x.Id == id);

            if (productCategory != null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {

            return ProductCategories.AsQueryable();
        }

        public void Delete(string id)
        {

            ProductCategory productCategory = ProductCategories.Find(x => x.Id == id);

            if (productCategory != null)
            {
                ProductCategories.Remove(productCategory);
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    }
}
