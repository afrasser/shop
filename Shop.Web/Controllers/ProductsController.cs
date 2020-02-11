using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Web.Data;
using Shop.Web.Data.Entities;
using Shop.Web.Data.Helpers;
using Shop.Web.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductRepository repository;
        private readonly IUserHelper userHelper;

        public ProductsController(IProductRepository repository, IUserHelper userHelper)
        {
            this.repository = repository;
            this.userHelper = userHelper;
        }

        // GET: Products
        public IActionResult Index() => View(repository.GetAll().OrderBy(p => p.Name));

        // GET: Products/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product product = repository.GetByIdAsync(id.Value).Result;
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (product.ImageFile != null && product.ImageFile.Length > 0)
                {
                    var guid = Guid.NewGuid().ToString();
                    var file = $"{guid}.jpg";

                    path = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot\\images\\products",
                        file);

                    // A simple way to use using statement
                    using var stream = new FileStream(path, FileMode.Create);
                    await product.ImageFile.CopyToAsync(stream).ConfigureAwait(true);

                    path = $"~/images/products/{file}";
                }

                product.User = await userHelper.GetUserByEmailAsync(User.Identity.Name).ConfigureAwait(true);

                //TODO: Create generic mapper method
                /*
                // fix image field mapping
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<ProductViewModel, Product>();
                });
                Product p = config.CreateMapper().Map<Product>(product);
                */

                var _product = ToProduct(product, path);
                await repository.CreateAsync(_product).ConfigureAwait(true);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        private static Product ToProduct(ProductViewModel view, string path)
        {
            return new Product
            {
                Id = view.Id,
                ImageUrl = path,
                IsAvailable = view.IsAvailable,
                LastPurchase = view.LastPurchase,
                LastSale = view.LastSale,
                Name = view.Name,
                Price = view.Price,
                Stock = view.Stock,
                User = view.User
            };
        }


        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product product = await repository.GetByIdAsync(id.Value).ConfigureAwait(true);
            if (product == null)
            {
                return NotFound();
            }

            ProductViewModel view = ToProducViewModel(product);
            return View(view);
        }

        private static ProductViewModel ToProducViewModel(Product product)
        {
            return new ProductViewModel
            {
                Id = product.Id,
                ImageUrl = product.ImageUrl,
                IsAvailable = product.IsAvailable,
                LastPurchase = product.LastPurchase,
                LastSale = product.LastSale,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                User = product.User
            };
        }


        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductViewModel product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var path = product.ImageUrl;

                    if (product.ImageUrl != null && product.ImageFile.Length > 0)
                    {
                        var guid = Guid.NewGuid().ToString();
                        var file = $"{guid}.jpg";

                        path = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot\\images\\products",
                            file);

                        // A simple way to use using statement
                        using var stream = new FileStream(path, FileMode.Create);
                        await product.ImageFile.CopyToAsync(stream).ConfigureAwait(true);
                        path = $"~/images/products/{file}";
                    }

                    product.User = await userHelper.GetUserByEmailAsync(User.Identity.Name).ConfigureAwait(true);
                    await repository.UpdateAsync(product).ConfigureAwait(true);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await repository.ExistAsync(product.Id).ConfigureAwait(true))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product product = repository.GetByIdAsync(id.Value).Result;
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = repository.GetByIdAsync(id).Result;
            await repository.DeleteAsync(product).ConfigureAwait(true);
            return RedirectToAction(nameof(Index));
        }
    }
}
