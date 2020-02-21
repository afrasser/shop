using Shop.Web.Data;
using Shop.Web.Data.Entities;

namespace Shop.Web
{
    public class CountryRepository: GenericRepository<Country>, ICountryRepository
    {
        private readonly DataContext context;
        
        public CountryRepository(DataContext context): base(context)
        {
            this.context = context;
        }
    }
}