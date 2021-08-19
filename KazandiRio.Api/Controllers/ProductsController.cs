using KazandiRio.Application.DTO;
using KazandiRio.Application.ProductModule.Commands;
using KazandiRio.Application.ProductModule.Queries;
using KazandiRio.Application.Services;
using KazandiRio.Domain.Entities;
using KazandiRio.Repository.DAL;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KazandiRio.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = Role.Admin + "," + Role.Consumer)]
    public class ProductsController : Controller
    {
        private readonly IMediator _mediatr;

        public ProductsController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        // GET: Product
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _mediatr.Send(new GetAllProductsQuery());
            return Json(products);
        }

        // GET: CategoryController/ get by id
        [HttpGet("{id}")]
        public async Task<IActionResult> Login(int id)
        {
            var product = await _mediatr.Send(new GetProductByIdQuery { Id = id });
            if (product != null)
                return Json(product);

            else
                return NotFound();

        }

        // POST: CategoryController/Details/5
        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult> Create(Product product)
        {
            Boolean success = await _mediatr.Send(new CreateProductCommand { Product = product });

            if (success)
                return Json("Ok");
            else
                return BadRequest();
        }
    }
}
