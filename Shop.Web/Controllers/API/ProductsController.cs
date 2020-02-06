using Microsoft.AspNetCore.Mvc;
using Shop.Web.Data;

namespace Shop.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository repository;

        public ProductsController(IProductRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetProducts() => Ok(repository.GetAllWithUsers());
    }
}