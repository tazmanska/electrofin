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
    public class TagsController : BaseController<TagDto, TagService>
    {
        [HttpGet]
        public IActionResult Get(string tag)
        {
            DataListDto<TagDto> result = null;

            if (!string.IsNullOrWhiteSpace(tag))
            {
                tag = tag.Trim().ToLowerInvariant();
                result = Service.GetAll(x => x.NormalizedName.Contains(tag));
            }
            else
            {
                result = Service.GetAll();
            }

            return Ok(result.Data.Select(x => x.Name));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Tag tag)
        {
            var result = Service.Create(new TagDto()
            {
                Name = tag.Name,
                NormalizedName = tag.Name.ToLowerInvariant()
            });

            return Created("api/tags/" + result.Id, result);
        }
    }
}