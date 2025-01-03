using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AmazonV02.Core.Entites
{
	public class CustomerBasket
	{
        public string Id { get; set; }
        public List<BasketItem> Items { get; set; }= new List<BasketItem>();
        public CustomerBasket(string id)
        {
            Id = id;
        }
        public string PaymentIntendId { get; set; }
        public string ClinetSecret { get; set; }
        public decimal ShippingCost   { get; set; }
        public int? DeliveryMethodId { get; set; }
    }
}
