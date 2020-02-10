using Microsoft.AspNetCore.Identity;
using Shop.Web.Data.Entities;
using Shop.Web.Models;
using System.Threading.Tasks;

namespace Shop.Web.Data.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public UserHelper(
            // Manage user accounts
            UserManager<User> userManager,
            // SignInManager manage user logging 
            SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IdentityResult> AddUserAsync(User user, string passworkd)
        {
            return await userManager.CreateAsync(user, passworkd);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await signInManager.PasswordSignInAsync(
                model.UserName,
                model.Password,
                model.RememberMe,
                false
            );
        }

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }
    }
}
