using Shop.Web.Data;
using Shop.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web
{
    // This interface is for custom implementations.
    public interface IOrderRepository: IGenericRepository<Order>
    {
        Task<IQueryable<Order>> GetOrderAsync(string userName);
    }
}