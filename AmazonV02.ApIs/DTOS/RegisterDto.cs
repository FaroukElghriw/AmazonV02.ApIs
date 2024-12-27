using System.ComponentModel.DataAnnotations;

namespace AmazonV02.ApIs.DTOS
{
	public class RegisterDto
	{
		[Required]
        public string DisplayName { get; set; }
		[Required]
		[Phone]
        public string Phone { get; set; }
		[Required]
		[EmailAddress]
        public string Email { get; set; }
		[Required]
        public string Password { get; set; }
    }
}
