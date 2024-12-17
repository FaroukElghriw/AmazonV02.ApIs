using System.ComponentModel.DataAnnotations;

namespace AmazonV02.ApIs.DTOS
{
	public class CustomerBasketDto
	{
		[Required]
		public string Id { get; set; } // as this will be Guid
		public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
       
    }
}
