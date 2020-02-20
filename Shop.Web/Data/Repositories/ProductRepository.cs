using Microsoft.EntityFrameworkCore;
using Shop.Web.Data.Entities;
using System.Linq;

namespace Shop.Web.Data
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly DataContext context;

        // base(context) pass the context injection to the base implementation class (GenericRepository)
        public ProductRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public IQueryable GetAllWithUsers()
        {
            return context.Products.Include(p => p.User);
        }
    }
}
