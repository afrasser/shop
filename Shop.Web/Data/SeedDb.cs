using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext context;
        private Random random;

        // Inject Database connection
        public SeedDb(DataContext context)
        {
            this.context = context;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            // Verify if the database is created firts
            await this.context.Database.EnsureCreatedAsync();

            if (!this.context.Products.Any())
            {
                this.AddProduct("Product 1");
                this.AddProduct("Product 2");
                this.AddProduct("Product 3");
                await this.context.SaveChangesAsync();
            }
        }

        public void AddProduct(string name)
        {
            this.context.Products.Add(new Entities.Product
            {
                Name = name,
                Price = random.Next(100),
                IsAvailable = true,
                Stock = this.random.Next(100)
            });
        }
    }
}
