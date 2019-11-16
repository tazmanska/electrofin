using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.Services;
using api.Models;
using api.Dtos;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public abstract class BaseController<TModel, TService> : Controller 
        where TModel: BaseDto 
        where TService: BaseService<TModel>, new()
    {
        protected TService Service = new TService();

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var result = Service.Get(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            var dto = Service.Get(id);

            if (dto == null)
            {
                return NotFound();
            }

            Service.Remove(id);

            return NoContent();
        }
    }
}