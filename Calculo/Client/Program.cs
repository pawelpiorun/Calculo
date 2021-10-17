using Blazored.LocalStorage;
using Calculo.Client.Interfaces;
using Calculo.Client.Repository;
using Calculo.Shared.Entities;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Spark.Core.Client.Auth;
using Spark.Core.Client.Extensions;
using Spark.Core.Client.Repository;
using Spark.Core.Client.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Calculo.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddSingleton(sp => 
                new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            ConfigureServices(builder.Services);

            await builder.Build().RunAsync();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSparkCoreClient();
            
            // categories
            services.AddScoped<IRepository<Category>, CategoriesRepository>();
            services.AddScoped<IRepository<Category, int>, CategoriesRepository>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();

            // business entities
            services.AddScoped<IRepository<BusinessEntity>, BusinessEntitiesRepository>();
            services.AddScoped<IRepository<BusinessEntity, int>, BusinessEntitiesRepository>();
            services.AddScoped<IBusinessEntitiesRepository, BusinessEntitiesRepository>();

            // rating
            services.AddScoped<IRatingRepository<BusinessEntity>, RatingRepository<BusinessEntity>>();

            // operations
            services.AddScoped<IRepository<Operation>, OperationsRepository>();
            services.AddScoped<IRepository<Operation, int>, OperationsRepository>();
            services.AddScoped<IOperationsRepository, OperationsRepository>();

            // calculations
            services.AddScoped<IRepository<Calculation>, CalculationsRepository>();
            services.AddScoped<IRepository<Calculation, int>, CalculationsRepository>();
            services.AddScoped<ICalculationsRepository, CalculationsRepository>();
        }
    }
}
