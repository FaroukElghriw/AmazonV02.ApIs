using AmazonV02.ApIs.Errors;
using AmazonV02.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmazonV02.ApIs.Controllers
{
	
	public class BuggyController :ApiBaseController
	{
		private readonly AmazonDbContext _amazonDb;

		public BuggyController(AmazonDbContext amazonDb)
        {
			_amazonDb = amazonDb;
		}
		//NotFound
		[HttpGet("NotFound")]
		public ActionResult GetNotFoundRequest()
		{
			var p = _amazonDb.Products.Find(100);
			if(p is null) return NotFound(new ApiResponse(404));

			return Ok(p);
		}
		// exception servererroe
		[HttpGet("ServerError")]
		public ActionResult GetServerError()
		{
			var p = _amazonDb.Products.Find(100);
			var pdto = p.ToString(); // null ref execption

			return Ok(pdto);
		}
		//BadRequest
		[HttpGet("BadRqquest")]
		public ActionResult GetBadRequest() 
		{
			

			return BadRequest(new ApiResponse(400));
		}
		[HttpGet("BadRqquest/{id}")]
		public ActionResult GetBadRequest(int id)
		{
			return Ok();
		}
	}
}
