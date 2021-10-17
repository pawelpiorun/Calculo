using Calculo.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spark.Core.Server.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculo.Server.Controllers
{
    public class BusinessEntitiesRatingController : RatingController<BusinessEntity>
    {
        public BusinessEntitiesRatingController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
            : base(context, userManager)
        {

        }
    }
}
