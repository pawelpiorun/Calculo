using AutoMapper;
using Calculo.Shared.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spark.Core.Server.Storage;
using Spark.Core.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Calculo.Server.Controllers
{
    [ApiController]
    [Route("api/businessentities")]
    public class BusinessEntitiesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IFileStorageService fileStorageService;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;

        public BusinessEntitiesController(ApplicationDbContext context,
            IFileStorageService fileStorageService,
            IMapper mapper,
            UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.fileStorageService = fileStorageService;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<int>> Post(BusinessEntity entity)
        {
            if (!string.IsNullOrEmpty(entity.Logo))
            {
                var image = Convert.FromBase64String(entity.Logo);
                using var ms = new MemoryStream(image);
                entity.Logo = await fileStorageService.SaveFile(ms, ".jpg", "businessentities");
            }

            context.Add(entity);
            await context.SaveChangesAsync();
            return entity.ID;
        }

        [HttpGet]
        public async Task<ActionResult<List<BusinessEntity>>> Get()
        {
            return await context.BusinessEntities.ToListAsync();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<BusinessEntity>> Get(int id)
        {
            return await context.BusinessEntities.FirstOrDefaultAsync(e => e.ID == id);
        }

        [HttpGet]
        [Route("{id}/rated")]
        public async Task<ActionResult<RatedEntityDTO<BusinessEntity>>> GetRated(int id)
        {
            int vote = 0;
            var average = 0.0d;

            bool isRated = false;
            if (await context.BusinessEntityRatings.AnyAsync(r => r.RatedEntityID == id))
            {
                average = await context.BusinessEntityRatings.Where(r => r.RatedEntityID == id)
                    .AverageAsync(r => r.Rate);
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var user = await userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
                    var userId = user.Id;

                    var userVoteDB = await context.BusinessEntityRatings
                        .FirstOrDefaultAsync(r => r.RatedEntityID == id && r.UserID == userId);

                    if (userVoteDB != null)
                    {
                        vote = userVoteDB.Rate;
                        isRated = true;
                    }
                }
            }

            var entity = await context.BusinessEntities.FirstOrDefaultAsync(e => e.ID == id);
            var dto = new RatedEntityDTO<BusinessEntity>()
            {
                RatedEntity = entity,
                Rating = vote,
                AverageRating = average,
                IsRated = isRated
            };
             return dto;
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Put(BusinessEntity entity)
        {
            var entityDB = await context.BusinessEntities.FirstOrDefaultAsync(e => e.ID == entity.ID);
            if (entityDB is null)
                return NotFound();

            entityDB = mapper.Map(entity, entityDB);
            if (!string.IsNullOrEmpty(entity.Logo))
            {
                var logo = Convert.FromBase64String(entity.Logo);
                using var ms = new MemoryStream(logo);
                entityDB.Logo = await fileStorageService.EditFile(ms, ".jpg", "businessentities", entityDB.Logo);
            }

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await context.BusinessEntities.FirstOrDefaultAsync(e => e.ID == id);
            if (entity is null)
                return NotFound();

            context.Remove(entity);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
