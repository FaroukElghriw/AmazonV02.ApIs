using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonV02.Core.Entites
{
	public class BasketItem
	{
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }

        public string Brand { get; set; }
        public string Type { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
