using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataInMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;

        List<Product> Products;

        public ProductRepository() { 
        
            Products = cache["Products"] as List<Product>;
            if (Products == null) {
                Products = new List<Product>();
            }
        }

        public void Commit() {

            cache["Products"] = Products;
        }

        public void Insert(Product product) {

            Products.Add(product);
        }

        public void Update(Product product) {

            Product productToUpdate = Products.Find(x => x.Id == product.Id);

            if (productToUpdate != null)
            {

                productToUpdate = product;
            }
            else 
            {
                throw new Exception("Product not found");
            }
        }

        public Product Find(string id) {

            Product product = Products.Find(x => x.Id == id);

            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        public IQueryable<Product> Collection() {

            return Products.AsQueryable();
        }

        public void Detete(string id) {

            Product product = Products.Find(x => x.Id == id);

            if (product != null)
            {
                Products.Remove(product);
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    }
}
