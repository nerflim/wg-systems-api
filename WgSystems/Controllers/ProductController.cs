using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WgSystems.Data;
using WgSystems.Models;

namespace WgSystems.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private WgSystemsDbContext wgSystemsDbContext;

        public ProductController(WgSystemsDbContext _wgSystemsDbContext)
        {
            wgSystemsDbContext = _wgSystemsDbContext;
        }

        // GET: api/Product
        [Authorize]
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return wgSystemsDbContext.Product;
        }

        // GET: api/Product/5
        [Authorize]
        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult Get(int id)
        {
            var product = wgSystemsDbContext.Product.Find(id);

            if (product == null)
            {
                return NotFound("Product Not Found");
            }

            return Ok(product);
        }

        // POST: api/Product
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {
            // Check if Category exist
            var category = wgSystemsDbContext.Category.Find(product.CategoryId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (category == null)
            {
                return NotFound("Category Not Found");
            }

            wgSystemsDbContext.Product.Add(product);
            wgSystemsDbContext.SaveChanges(true);

            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/Product/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Product product)
        {
            if (!ModelState.IsValid || id!=product.Id)
            {
                return BadRequest(ModelState);
            }
            wgSystemsDbContext.Product.Update(product);
            wgSystemsDbContext.SaveChanges(true);

            return Ok("Product Updated!");
        }

        // DELETE: api/ApiWithActions/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Check if Product exist
            var product = wgSystemsDbContext.Product.Find(id);
            
            if(product == null)
            {
                return NotFound("Product Not Found");
            }

            wgSystemsDbContext.Product.Remove(product);
            wgSystemsDbContext.SaveChanges(true);

            return Ok("Product Deleted!");
        }
    }
}
