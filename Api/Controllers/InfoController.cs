using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/info")]
public class InfoController : ControllerBase
{
	[HttpGet]
	public IActionResult GetInfo()
	{
		return Ok(new { result = Environment.MachineName });
	}
}
