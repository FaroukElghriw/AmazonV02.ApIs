using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AmazonV02.ApIs.Errors
{
	public class ApiValidationErrorResponse:ApiResponse
	{
        public IEnumerable<string> Errors { get; set; }

        public ApiValidationErrorResponse():base(400)
        {
            Errors = new List<string>();
        }
    }
}
