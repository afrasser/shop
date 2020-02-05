using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Web.Data;
using Shop.Web.Data.Entities;
using Shop.Web.Data.Helpers;
using Shop.Web.Models;
using System.IO;
using System.Threading.Tasks;

namespace Shop.Web.Controllers
{
    //TODO: Fix error on ProductsController
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
        public IActionResult Index() => View(this.repository.GetAll());

        // GET: Products/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = this.repository.GetByIdAsync(id.Value);
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
                    path = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot\\images\\products",
                        product.ImageFile.FileName);

                    // A simple way to use using statement
                    using var stream = new FileStream(path, FileMode.Create);
                    await product.ImageFile.CopyToAsync(stream);
                }

                path = $"~/images/products/{product.ImageFile.FileName}";

                //TODO: Change how user are logged
                product.User = await this.userHelper.GetUserByEmailAsync("andrew8805@gmail.com");

                //TODO: Create generic mapper method
                /*
                // fix image field mapping
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<ProductViewModel, Product>();
                });
                Product p = config.CreateMapper().Map<Product>(product);
                */

                var _product = this.ToProduct(product, path);
                await this.repository.CreateAsync(_product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        private Product ToProduct(ProductViewModel view, string path)
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

            var product = await repository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            var view = this.ToProducViewModel(product);
            return View(view);
        }

        public ProductViewModel ToProducViewModel(Product product)
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
                        path = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot\\images\\products",
                            product.ImageFile.FileName);

                        // A simple way to use using statement
                        using var stream = new FileStream(path, FileMode.Create);
                        await product.ImageFile.CopyToAsync(stream);
                        path = $"~/images/products/{product.ImageFile.FileName}";
                    }

                    //TODO: Change for the logged user
                    product.User = await this.userHelper.GetUserByEmailAsync("andrew8805@gmail.com");
                    await this.repository.UpdateAsync(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.repository.ExistAsync(product.Id))
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

            var product = this.repository.GetByIdAsync(id.Value);
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
            var product = await this.repository.GetByIdAsync(id);
            await this.repository.DeleteAsync(product);
            return RedirectToAction(nameof(Index));
        }
    }
}
