using AmazonV02.Core.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AmazonV02.Repository.Data
{
	public class AmazonDbContext:DbContext
	{
		public AmazonDbContext(DbContextOptions<AmazonDbContext> options) : base(options) { }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}

		public DbSet<Product> Products { get; set; }
		public DbSet<ProductBrand> Brnands { get; set; }
		public DbSet<ProductType> Types { get; set; }
	}
}
