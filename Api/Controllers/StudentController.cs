using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/students")]
public class StudentController : ControllerBase
{
	private readonly IStudentService studentService;

	public StudentController(IStudentService studentService)
	{
		this.studentService = studentService;
	}

	[HttpPost]
	public async Task<IActionResult> AddStudent([FromForm] StudentRequest studentRequest, IFormFile file)
	{
		var result = await studentService.AddStudentAsync(studentRequest, file);
		if (result.IsSuccess)
			return Ok(new { result = result.Value });
		return BadRequest(new { error = result.Error });
	}

	[HttpGet]
	public async Task<IActionResult> GetStudentList()
	{
		var result = await studentService.GetStudentListAsync();
		if (result.IsSuccess)
			return Ok(new { result = result.Value });
		return BadRequest(new { error = result.Error });
	}

	[HttpGet("{studentId}")]
	public async Task<IActionResult> GetStudentById(Guid studentId)
	{
		var result = await studentService.GetStudentByIdAsync(studentId);
		if (result.IsSuccess)
			return Ok(new { result = result.Value });
		return BadRequest(new { error = result.Error });
	}

	[HttpPut("{studentId}")]
	public async Task<IActionResult> UpdateStudent(Guid studentId, [FromForm] StudentRequest studentRequest, IFormFile file)
	{
		var result = await studentService.UpdateStudentAsync(studentId, studentRequest, file);
		if (result.IsSuccess)
			return Ok(new { result = result.Value });
		return BadRequest(new { error = result.Error });
	}

	[HttpDelete("{studentId}")]
	public async Task<IActionResult> DeleteStudent(Guid studentId)
	{
		var result = await studentService.DeleteStudentAsync(studentId);
		if (result.IsSuccess)
			return Ok(new { result = result.Value });
		return BadRequest(new { error = result.Error });
	}
}
