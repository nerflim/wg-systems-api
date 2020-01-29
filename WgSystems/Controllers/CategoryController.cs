using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WgSystems.Data;
using WgSystems.Models;

namespace WgSystems.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private WgSystemsDbContext wgSystemsDbContext;

        public CategoryController(WgSystemsDbContext _wgSystemsDbContext)
        {
            wgSystemsDbContext = _wgSystemsDbContext;
        }

        // GET: api/Category
        [Authorize]
        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return wgSystemsDbContext.Category.Include(c => c.Products);
        }

        // GET: api/Category/5
        [Authorize]
        [HttpGet("{id}", Name = "GetCategory")]
        public IActionResult Get(int id)
        {
            var category = wgSystemsDbContext.Category.Include(c => c.Products).SingleOrDefault(c => c.Id == id);
            if(category == null)
            {
                return NotFound("Category Not Found");
            }

            return Ok(category);
        }

        // POST: api/Category
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody]Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            wgSystemsDbContext.Category.Add(category);
            wgSystemsDbContext.SaveChanges(true);

            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/Category/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Category category)
        {
            if (!ModelState.IsValid || id != category.Id)
            {
                return BadRequest(ModelState);
            }
            wgSystemsDbContext.Category.Update(category);
            wgSystemsDbContext.SaveChanges(true);

            return Ok("Category Updated!");
        }

        // DELETE: api/ApiWithActions/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Check if Category exist
            var category = wgSystemsDbContext.Category.Find(id);
            // Get all products from category
            var products = wgSystemsDbContext.Product.Where(item => item.CategoryId == id);

            if (category == null)
            {
                return NotFound("Category Not Found");
            }

            wgSystemsDbContext.Category.Remove(category);
            wgSystemsDbContext.Product.RemoveRange(products);
            wgSystemsDbContext.SaveChanges(true);

            return Ok("Category Deleted!");
        }
    }
}
