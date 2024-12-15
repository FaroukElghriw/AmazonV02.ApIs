using AmazonV02.ApIs.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmazonV02.ApIs.Controllers
{
	[Route("errors/{code}")]
	[ApiExplorerSettings(IgnoreApi =true)]
	[ApiController]
	public class ErrorController : ControllerBase
	{
		public ActionResult Error(int code)
		{
			return Ok(new ApiResponse(code));
		}
	}
}
