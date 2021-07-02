using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PermissionAuthDemo.Shared.Constants;
using System.Collections.Generic;
using System.Threading;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PermissionAuthDemo.Server.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // GET: api/products
        [Authorize(Policy = Permissions.Products.View)]
        [HttpGet]
        public IEnumerable<string> Get(CancellationToken token)
        {
            return new string[] { "product 1", "product 2" };
        }

        // GET api/products/5
        [Authorize(Policy = Permissions.Products.View)]
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return $"product {id}";
        }

        // POST api/products
        [Authorize(Policy = Permissions.Products.Create)]
        [HttpPost]
        public string Post([FromBody] string value)
        {
            return $"product created";
        }

        // PUT api/products/5
        [Authorize(Policy = Permissions.Products.Edit)]
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] string value)
        {
            return $"product updated";
        }

        // DELETE api/products/5
        [Authorize(Policy = Permissions.Products.Delete)]
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return $"product deleted";
        }
    }
}
