using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PermissionAuthDemo.Shared.Constants;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PermissionAuthDemo.Server.Controllers
{
    [Route("api/brands")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        // GET: api/brands
        [Authorize(Policy = Permissions.Brands.View)]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "brand 1", "brand 2" };
        }

        // GET api/brands/5
        [Authorize(Policy = Permissions.Brands.View)]
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return $"brand {id}";
        }

        // POST api/brands
        [Authorize(Policy = Permissions.Brands.Create)]
        [HttpPost]
        public string Post([FromBody] string value)
        {
            return $"brand created";
        }

        // PUT api/brands/5
        [Authorize(Policy = Permissions.Brands.Edit)]
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] string value)
        {
            return $"brand updated";
        }

        // DELETE api/brands/5
        [Authorize(Policy = Permissions.Brands.Delete)]
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return $"brand deleted";
        }
    }
}
