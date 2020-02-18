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
        private readonly RoleManager<IdentityRole> roleManager;

        public UserHelper(
            // Manage user accounts
            UserManager<User> userManager,
            // SignInManager manage user logging 
            SignInManager<User> signInManager, 
            // Manage Roles
            RoleManager<IdentityRole> roleManager
            )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        public async Task<IdentityResult> AddUserAsync(User user, string passworkd)
        {
            return await userManager.CreateAsync(user, passworkd);
        }

        public async Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword)
        {
            return await userManager.ChangePasswordAsync(user, oldPassword, newPassword);
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

        public async Task<IdentityResult> UpdatePasswordAsync(User user)
        {
            return await userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> UpdateUserAsync(User user)
        {
            return await userManager.UpdateAsync(user);
        }

        public async Task<SignInResult> ValidatePasswordAsync(User user, string passowrd)
        {
            return await signInManager.CheckPasswordSignInAsync(user, passowrd, false);
        }
    }
}
