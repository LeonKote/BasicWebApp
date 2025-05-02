using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Student
{
	public Guid Id { get; set; }

	[MaxLength(240)]
	public string Name { get; set; }

	[EmailAddress]
	public string Email { get; set; }

	[MaxLength(20)]
	public string Document { get; set; }

	[MaxLength(20)]
	public string Phone { get; set; }

	public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

	[MaxLength(512)]
	public string Photo { get; set; } = "/";
}
