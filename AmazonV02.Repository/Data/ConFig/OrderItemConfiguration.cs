using AmazonV02.Core.Entites.Order_Aggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonV02.Repository.Data.ConFig
{
	public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
	{
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			builder.OwnsOne(orderItem => orderItem.Product, x => x.WithOwner());
			builder.Property(OrderItem => OrderItem.Price)
				   .HasColumnType("decimal(18,2)");
		}
	}
}
