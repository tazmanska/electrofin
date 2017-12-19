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
    public class CategoriesController : Controller
    {
        private CategoryService _categoryServices = new CategoryService();

        [HttpGet]
        [Route("")]
        public IActionResult Get(bool income = true)
        {
            var result = _categoryServices.GetAllCategories(income ? CategoryType.Income : CategoryType.Outcome);

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var result = _categoryServices.Get(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Category category)
        {
            var result = _categoryServices.Create(new CategoryDto()
            {
                ParentCategoryId = category.ParentCategoryId,
                Type = category.Income ? CategoryType.Income : CategoryType.Outcome,
                Name = category.Name,
                Description = category.Description
            });

            return Created("api/categories/" + result.Id, result);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            var account = _categoryServices.Get(id);

            if (account == null)
            {
                return NotFound();
            }

            _categoryServices.Remove(id);

            return NoContent();
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(int id, [FromBody] CategoryUpdate categoryUpdate)
        {
            var category = _categoryServices.Get(id);

            if (category == null)
            {
                return NotFound();
            }

            category.Name = categoryUpdate.Name;
            category.Description = categoryUpdate.Description;

            _categoryServices.Update(category);

            return Ok(category);
        }
    }
}