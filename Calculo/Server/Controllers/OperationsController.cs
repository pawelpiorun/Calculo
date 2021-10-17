using Calculo.Server.Extensions;
using Calculo.Shared.DTOs;
using Calculo.Shared.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spark.Core.Server.Extensions;
using Spark.Core.Shared.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculo.Server.Controllers
{
    [ApiController]
    [Route("api/operations")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OperationsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public OperationsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Operation operation)
        {
            // Temporary workaround, operation category coming in here is a different instance than in db
            var category = context.Categories.FirstOrDefault(c => c.ID == operation.Category.ID);
            operation.Category = category;

            context.Operations.Add(operation);
            await context.SaveChangesWithIdentityInsertAsync<Category>();
            return operation.ID;
        }

        [HttpGet]
        public async Task<ActionResult<List<Operation>>> Get([FromQuery] PaginationDTO pagination)
        {
            var queryable = context.Operations.AsQueryable();
            await HttpContext.InsertPaginationParametersInResponse<Operation>(queryable, pagination.ItemsPerPage);

            
            return await queryable.Paginate(pagination)
                .Include(op => op.Category)
                .ToListAsync();
        }

        [HttpGet]
        [Route("iedto")]
        public async Task<ActionResult<IncomesAndExpensesDTO>> GetIEDTO()
        {
            var incomes = await context.Operations
                .Include(op => op.Category)
                .Where(x => x.Value >= 0)
                .Take(ieDtoLimit).OrderByDescending(x => x.Date).ToListAsync();

            var expenses = await context.Operations
                .Include(op => op.Category)
                .Where(x => x.Value < 0)
                .Take(ieDtoLimit).OrderByDescending(x => x.Date).ToListAsync();

            var response = new IncomesAndExpensesDTO();
            response.Incomes = incomes;
            response.Expenses = expenses;

            return response;
        }
        private static readonly int ieDtoLimit = 30;

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Operation>> GetOperation(int id)
        {
            var operation = await context.Operations
                .Include(x => x.Category)
                .FirstOrDefaultAsync(op => op.ID == id);
            if (operation is null)
                return NotFound();

            return operation;
        }

        [HttpPut]
        public async Task<ActionResult<int>> Put(Operation operation)
        {
            context.Attach(operation).State = EntityState.Modified;
            await context.SaveChangesWithIdentityInsertAsync<Category>();
            return operation.ID;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var operation = await context.Operations.FirstOrDefaultAsync(op => op.ID == id);
            if (operation is null)
                return NotFound();

            context.Remove(operation);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("filter")]
        public async Task<ActionResult<List<Operation>>> Filter(OperationFilterDTO filter)
        {
            var queryable = context.Operations.AsQueryable();
            if (!string.IsNullOrWhiteSpace(filter.Title))
                queryable = queryable.Where(op => op.Title.Contains(filter.Title));

            if (!filter.Income)
                queryable = queryable.Where(op => op.Value < 0);

            if (!filter.Expense)
                queryable = queryable.Where(op => op.Value >= 0);

            if (filter.CategoryID != 0)
                queryable = queryable.Where(op => op.Category.ID == filter.CategoryID);

            await HttpContext.InsertPaginationParametersInResponse(queryable, filter.ItemsPerPage);

            return await queryable.Paginate(filter.Pagination).ToListAsync();
        }
    }
}
