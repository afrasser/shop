using Shop.Web.Data.Entities;

namespace Shop.Web.Data
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        // base(context) pass the context injection to the base implementation class (GenericRepository)
        public ProductRepository(DataContext context): base(context)
        {

        }
    }
}
