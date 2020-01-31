using Shop.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Data
{
    public class Repository : IRepository
    {
        private readonly DataContext context;

        public Repository(DataContext context)
        {
            this.context = context;
        }

        public IEnumerable<Product> GetProducts() => context.Products.OrderBy(p => p.Name);

        public Product GetProduct(int id) => context.Products.Find(id);

        public void AddProduct(Product product)
        {
            context.Products.Add(product);
        }

        public void RemoveProduct(Product product)
        {
            context.Products.Remove(product);
        }

        public void UpdateProduct(Product product)
        {
            context.Products.Update(product);
        }

        public bool ProductExists(int id) => this.context.Products.Any(p => p.Id == id);

        public async Task<bool> SaveAllAsync() => await this.context.SaveChangesAsync() > 0;

        public Product GetProduct(int? id)
        {
            throw new System.NotImplementedException();
        }
    }
}
