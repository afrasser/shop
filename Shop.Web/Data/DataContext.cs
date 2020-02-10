using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.Web.Data.Entities;
using System.Linq;

namespace Shop.Web.Data
{
    public class DataContext: IdentityDbContext<User>
    {
        public DbSet<Product> Products { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        /// <summary>
        /// Personalize database migrations behavior
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Map decimal type to correct sql type
            builder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            // Select foreign keys that can be deleted on cascade
            var cascadeFks = builder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            // Set a restriction to the precvios selected fk's
            foreach(var fk in cascadeFks)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(builder);
        }
    }
}
