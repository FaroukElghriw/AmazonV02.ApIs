using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonV02.Core.Entites.Order_Aggregate
{
	public class OrderItem:BaseModel
	{
        public OrderItem()
        {
            
        }
        public OrderItem(decimal price, int quantity, ProductItemOrdered product)
        {
            Price = price;
            Quantity = quantity;
            Product = product;
        }
        public decimal Price { get; set; }

        public ProductItemOrdered Product { get; set; }
        public int Quantity { get; set; }
    }
}
