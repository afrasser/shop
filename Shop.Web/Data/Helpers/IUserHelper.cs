namespace Shop.Web.Data.Helpers
{
    using Microsoft.AspNetCore.Identity;
    using Shop.Web.Data.Entities;
    using Shop.Web.Models;
    using System.Threading.Tasks;

    public interface IUserHelper
    {
        Task<User> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string passworkd);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();
    }
}
