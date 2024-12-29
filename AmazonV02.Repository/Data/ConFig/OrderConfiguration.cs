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
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.OwnsOne(O => O.ShippingAddress, x => x.WithOwner());
			builder.Property(O => O.Status)
				.HasConversion(
				Ostatus => Ostatus.ToString(),
				Ostatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), Ostatus)

				);
			builder.Property(O => O.SubTotal)
				.HasColumnType("decimal(18,2)");
		}
	}
}
