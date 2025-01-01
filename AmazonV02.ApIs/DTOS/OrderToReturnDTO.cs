using AmazonV02.Core.Entites.Order_Aggregate;

namespace AmazonV02.ApIs.DTOS
{
	public class OrderToReturnDTO
	{
		public int Id { get; set; }
		public string BuyerEmail { get; set; }
		public DateTimeOffset OrderData { get; set; }
		public string Status { get; set; }
		public Address ShippingAddress { get; set; }
		// public DeliveryMethod DeliveryMethod { get; set; }
		public string DeliveryMethod { get; set; }
		public decimal DeliveryMethodCost { get; set; }
		public ICollection<OrderItemDTO> Items { get; set; }
		public decimal Total { get; set; }

		public string PaymentIntenId { get; set; } = string.Empty;
	}
}
