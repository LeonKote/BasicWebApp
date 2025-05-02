using Domain.Models;

namespace Domain.Interfaces;

public interface IStudentRepository
{
	Task AddStudentAsync(Student student);
	Task<List<Student>> GetStudentListAsync();
	Task<Student?> GetStudentByIdAsync(Guid studentId);
	Task UpdateStudentAsync(Student student);
	Task DeleteStudentAsync(Student student);
}
