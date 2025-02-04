﻿using System.ComponentModel.DataAnnotations;

namespace AmazonV02.ApIs.DTOS
{
	public class BasketItemDto
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public string ProductName { get; set; }
		[Required]
		public string PictureUrl { get; set; }
		[Required]
		[Range(1, double.MaxValue, ErrorMessage = "Price must be att leat 0.1")]
		public decimal Price { get; set; }
		[Required]
		[Range(1, int.MaxValue, ErrorMessage = "Quantity must be one at list")]
		public int Quantity { get; set; }
		[Required]
		public string Brand { get; set; }
		[Required]
		public string Type { get; set; }
	}
}