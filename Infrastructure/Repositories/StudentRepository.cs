using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class StudentRepository : IStudentRepository
{
	private readonly AppDbContext context;

	public StudentRepository(AppDbContext context)
	{
		this.context = context;
	}

	public async Task AddStudentAsync(Student student)
	{
		await context.Students.AddAsync(student);
		await context.SaveChangesAsync();
	}

	public async Task<List<Student>> GetStudentListAsync()
	{
		return await context.Students.ToListAsync();
	}

	public async Task<Student?> GetStudentByIdAsync(Guid studentId)
	{
		return await context.Students.FirstOrDefaultAsync(x => x.Id == studentId);
	}

	public async Task UpdateStudentAsync(Student student)
	{
		context.Students.Update(student);
		await context.SaveChangesAsync();
	}

	public async Task DeleteStudentAsync(Student student)
	{
		context.Students.Remove(student);
		await context.SaveChangesAsync();
	}
}
