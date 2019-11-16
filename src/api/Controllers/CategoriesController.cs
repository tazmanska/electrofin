using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.Services;
using api.Models;
using api.Dtos;
using api.Enums;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class CategoriesController : BaseController<CategoryDto, CategoryService>
    {
        [HttpGet]
        [Route("")]
        public IActionResult Get(bool income = true)
        {
            var result = Service.GetAllCategories(income ? CategoryType.Income : CategoryType.Outcome);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Category category)
        {
            var result = Service.Create(new CategoryDto()
            {
                ParentCategoryId = category.ParentCategoryId,
                Type = category.Income ? CategoryType.Income : CategoryType.Outcome,
                Name = category.Name,
                Description = category.Description,
                Tags = category.Tags
            });

            return Created("api/categories/" + result.Id, result);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(int id, [FromBody] CategoryUpdate categoryUpdate)
        {
            var category = Service.Get(id);

            if (category == null)
            {
                return NotFound();
            }

            category.Name = categoryUpdate.Name;
            category.Description = categoryUpdate.Description;
            category.Tags = categoryUpdate.Tags;

            Service.Update(category);

            return Ok(category);
        }
    }
}