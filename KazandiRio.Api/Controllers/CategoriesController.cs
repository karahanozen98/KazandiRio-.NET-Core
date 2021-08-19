using KazandiRio.Application.CategoryModule.Commands;
using KazandiRio.Application.CategoryModule.Queries;
using KazandiRio.Application.DTO;
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
    public class CategoriesController : Controller
    {
        private readonly IMediator _mediatr;

        public CategoriesController(IMediator mediatr, ICategoryService service)
        {
            _mediatr = mediatr;
        }

        // GET: Categories
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            //var categories = await _categoryService.GetCategoriesAsync();
            var categories = await _mediatr.Send(new GetAllCategoriesQuery());
            return Json(categories);
        }

        // GET: CategoryController/ get by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            //var category = await _categoryService.GetCateogryByIdAsync(id);
            var category = await _mediatr.Send(new GetCategoryByIdQuery { Id = id });
            return Json(category);
        }

        // POST: CategoryController/Details/5
        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult> Create([FromBody] Category category)
        {
            Boolean success = await _mediatr.Send(new CreateCategoryCommand { Category = category });
            if (success)
                return Json("Ok");
            else
                return BadRequest();
        }
    }
}
