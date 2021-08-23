using KazandiRio.Application.DTO;
using KazandiRio.Application.Modules.ProductModule.Commands.CreateProduct;
using KazandiRio.Application.Modules.ProductModule.Commands.DeleteProduct;
using KazandiRio.Application.Modules.ProductModule.Commands.UpdateProduct;
using KazandiRio.Application.Modules.ProductModule.Queries.GetAllProducts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KazandiRio.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "UserPolicy")]
    public class ProductsController : Controller
    {
        private readonly IMediator _mediatr;

        public ProductsController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _mediatr.Send(new GetAllProductsQuery());
            return Ok(products);
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Create(CreateProductDto product)
        {
            await _mediatr.Send(new CreateProductCommand { Product = product });
            return Ok("Ok");
        }

        [HttpPut]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Update(UpdateProductDto product)
        {
            await _mediatr.Send(new UpdateProductCommand { Product = product });
            return Ok("Ok");
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Delete(ProductIdDto product)
        {
            await _mediatr.Send(new DeleteProductCommand { ProductId = product.productId });
            return Ok("Ok");
        }
    }
}
