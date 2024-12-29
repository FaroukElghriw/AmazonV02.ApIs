using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonV02.Core.Entites.Order_Aggregate
{
	public class Order :BaseModel
	{
        public Order()
        {
            
        }
        public Order(string email , Address address , ICollection<OrderItem> items , DeliveryMethod deliveryMethod , decimal subtotal,string? paymenIntId) 
        {
            BuyerEmail = email;
            ShippingAddress = address;
            Items = items;
            DeliveryMethod = deliveryMethod;
            SubTotal = subtotal;
            PaymentIntId = paymenIntId;
            
        }
        public string BuyerEmail { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public DateTimeOffset DateTimeOffset { get; set; }  = DateTimeOffset.Now;
        public Address ShippingAddress { get; set; }

        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();

        public decimal  SubTotal { get; set; }
        public DeliveryMethod  DeliveryMethod { get; set; }
        public decimal GetTotal()
              => SubTotal + DeliveryMethod.Cost ;
        public string? PaymentIntId { get; set; } = string.Empty;

    }
}
