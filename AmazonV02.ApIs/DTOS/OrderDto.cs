using System.ComponentModel.DataAnnotations;

namespace AmazonV02.ApIs.DTOS
{
	public class OrderDto
	{
		[Required]
        public string Basketid { get; set; }
		[Required]
        public int DeliveryMethodId	 { get; set; }
        public AddressDto ShippingAddress { get; set; }
    }
}
