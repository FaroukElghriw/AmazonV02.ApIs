using AmazonV02.Core.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonV02.Repository.Data.ConFig
{
	
		internal class ProductConfiguration : IEntityTypeConfiguration<Product>
		{
			public void Configure(EntityTypeBuilder<Product> builder)
			{
				builder.HasOne(P => P.ProductBrand).WithMany();


				builder.HasOne(P => P.ProductType).WithMany();

				builder.Property(p => p.Price)
					.HasColumnType("decimal(18,2)");

			}
		}
	
}
