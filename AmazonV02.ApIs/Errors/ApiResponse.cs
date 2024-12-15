
namespace AmazonV02.ApIs.Errors
{
	public class ApiResponse
	{
        public int StatusCode { get; set; }
        public string?  Message { get; set; }
        public ApiResponse(int statuscode, string? message=null)
        {
            StatusCode = statuscode;
            Message = message?? GetDefaultMessageStatusCode(statuscode);
        }

		private string? GetDefaultMessageStatusCode(int statuscode)
		{
			return statuscode switch
			{
				400 => "Bad Request You have Made",
				404 => "Resource Not Found",
				401 => "Authorized you have Made",
				500 => "Error let to Dark Side",
				_ => null
			};
		}
	}
}
