using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class StudentRequest
{
	[MaxLength(240)]
	[FromForm(Name = "name")]
	public string Name { get; set; }

	[EmailAddress]
	[FromForm(Name = "email")]
	public string Email { get; set; }

	[MaxLength(20)]
	[FromForm(Name = "document")]
	public string Document { get; set; }

	[MaxLength(20)]
	[FromForm(Name = "phone")]
	public string Phone { get; set; }

	[MaxLength(512)]
	[FromForm(Name = "photo")]
	public string Photo { get; set; }
}
