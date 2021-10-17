using Calculo.Shared.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculo.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public CategoriesController(ApplicationDbContext dbContext)
        {
            this.context = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Category category)
        {
            context.Add(category);
            await context.SaveChangesAsync();
            return category.ID;
        }

        [HttpGet]
        [Route("search/{propertyName}/{searchText}")]
        public async Task<ActionResult<List<Category>>> Search(string propertyName, string searchText)
        {
            return await Task.Run<ActionResult<List<Category>>>(() =>
            {
                if (string.IsNullOrEmpty(searchText)) return new List<Category>();

                var property = typeof(Category).GetProperty(propertyName);
                if (property is null || property.PropertyType != typeof(string))
                    return BadRequest();

                return context.Categories.AsEnumerable<Category>().Where(cat =>
                    (property.GetValue(cat) as string).Contains(searchText)).Take(5).ToList();
            });
        }

        [HttpGet]
        [Route("search/{searchText}")]
        public async Task<ActionResult<List<Category>>> SearchByName(string searchText)
        {
            if (string.IsNullOrEmpty(searchText)) return new List<Category>();

            return await context.Categories.Where(cat =>
                cat.Name.Contains(searchText)).Take(5).ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> Get()
        {
            return await context.Categories.ToListAsync();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            var category = await context.Categories.FirstOrDefaultAsync(c => c.ID == id);
            if (category is null)
                return NotFound();

            return category;
        }



        [HttpPut]
        public async Task<ActionResult<int>> Put(Category category)
        {
            context.Attach(category).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return category.ID;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var category = await context.Categories.FirstOrDefaultAsync(c => c.ID == id);
            if (category is null)
                return NotFound();

            context.Remove(category);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
