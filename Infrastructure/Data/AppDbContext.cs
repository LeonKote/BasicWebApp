using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
	public DbSet<Student> Students { get; set; }

	public AppDbContext(DbContextOptions options) : base(options)
	{
		Database.EnsureCreated();
	}
}
