using Calculo.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Calculo.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculationsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public CalculationsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<ActionResult<List<Calculation>>> Get()
        {
            return await context.Calculations.ToListAsync();
        }
    }
}
