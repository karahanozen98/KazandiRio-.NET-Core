using KazandiRio.Application.Modules.CategoryModule.Commands.CreateCategory;
using KazandiRio.Application.Modules.CategoryModule.Queries.GetAllCategories;
using KazandiRio.Application.Modules.CategoryModule.Queries.GetCategoryById;
using KazandiRio.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KazandiRio.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "UserPolicy")]
    public class CategoriesController : Controller
    {
        private readonly IMediator _mediatr;

        public CategoriesController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _mediatr.Send(new GetAllCategoriesQuery());
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _mediatr.Send(new GetCategoryByIdQuery { Id = id });
            return Ok(category);
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> Create([FromBody] Category category)
        {
            await _mediatr.Send(new CreateCategoryCommand { Category = category });
            return Ok("Ok");
        }
    }
}
