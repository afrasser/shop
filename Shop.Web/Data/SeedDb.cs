using Microsoft.AspNetCore.Identity;
using Shop.Web.Data.Entities;
using Shop.Web.Data.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;
        private Random random;

        // Inject Database context and user helper
        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            // Verify if the database is created firts
            await this.context.Database.EnsureCreatedAsync();

            var user = await this.userHelper.GetUserByEmailAsync("andrew8805@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Andrew",
                    LastName = "Fraser",
                    Email = "andrew8805@gmail.com",
                    UserName = "afraser",
                    PhoneNumber = "3001234567"
                };

                var result = await this.userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user");
                }
            }

            if (!this.context.Products.Any())
            {
                this.AddProduct("Product 1", user);
                this.AddProduct("Product 2", user);
                this.AddProduct("Product 3", user);
                await this.context.SaveChangesAsync();
            }
        }

        public void AddProduct(string name, User user)
        {
            this.context.Products.Add(new Entities.Product
            {
                Name = name,
                Price = random.Next(100),
                IsAvailable = true,
                Stock = this.random.Next(100),
                User = user
            });
        }
    }
}
