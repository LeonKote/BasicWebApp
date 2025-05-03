using Domain.Errors;
using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Domain.Interfaces;

public interface IStudentService
{
	Task<Result<string>> AddStudentAsync(StudentRequest studentRequest, IFormFile? file);
	Task<Result<List<Student>>> GetStudentListAsync();
	Task<Result<Student?>> GetStudentByIdAsync(Guid studentId);
	Task<Result<string>> UpdateStudentAsync(Guid studentId, StudentRequest studentRequest, IFormFile? file);
	Task<Result<string>> DeleteStudentAsync(Guid studentId);
}
