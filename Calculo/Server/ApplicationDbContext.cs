using Calculo.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Spark.Core.Server;
using Spark.Core.Shared.Entities;

namespace Calculo.Server
{
    public class ApplicationDbContext : SparkDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Operation>().HasKey(x => new { x.ID });
            //modelBuilder.Entity<Category>().HasKey(x => new { x.ID });
            //modelBuilder.Entity<BusinessEntity>().HasKey(x => new { x.ID });
            //modelBuilder.Entity<Calculation>().HasKey(x => new { x.ID });

            modelBuilder.Entity<Operation>()
                .HasOne(o => o.Category)
                .WithMany(c => c.Operations);

            modelBuilder.Entity<Operation>()
               .HasOne(o => o.BusinessEntity)
               .WithMany(c => c.Operations);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Operation> Operations { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BusinessEntity> BusinessEntities { get; set; }
        public DbSet<Calculation> Calculations { get; set; }
        public DbSet<Rating<BusinessEntity>> BusinessEntityRatings { get; set; }
    }
}
