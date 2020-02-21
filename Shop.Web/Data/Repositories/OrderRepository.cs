using Microsoft.EntityFrameworkCore;
using Shop.Web.Data;
using Shop.Web.Data.Entities;
using Shop.Web.Data.Helpers;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;

        public OrderRepository(DataContext context, IUserHelper userHelper) : base(context)
        {
            this.context = context;
            this.userHelper = userHelper;
        }

        public async Task<IQueryable<Order>> GetOrderAsync(string userName)
        {
            var user = await userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return null;
            }

            // TODO: Create this function in UserHelper
            //if(await userHelper.isUserInRoleAsync(user, "Admin"))
            //{
            //    return context.Orders
            //        .Include(o => o.Items)
            //        .ThenInclude(i => i.Product)
            //        .OrderByDescending(o => o.OrderDate);
            //}

            return context.Orders
                .Include(o => o.Items) // Join order items
                .ThenInclude(i => i.Product) // Join items products
                .Where(o => o.User == user) // Filter by logged user
                .OrderByDescending(o => o.OrderDate); // Order by date ascending
        }
    }
}