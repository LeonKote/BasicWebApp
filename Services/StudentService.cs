using Amazon.S3;
using Amazon.S3.Model;
using Domain.Errors;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Services;

public class StudentService : IStudentService
{
	private readonly IStudentRepository studentRepository;
	private readonly IAmazonS3 s3Client;
	private readonly string bucketName;

	public StudentService(IStudentRepository studentRepository, IAmazonS3 s3Client, IConfiguration config)
	{
		this.studentRepository = studentRepository;
		this.s3Client = s3Client;
		this.bucketName = config["AWS:BucketName"]!;
	}

	public async Task<Result<string>> AddStudentAsync(StudentRequest studentRequest, IFormFile? file)
	{
		var student = new Student
		{
			Name = studentRequest.Name,
			Email = studentRequest.Email,
			Document = studentRequest.Document,
			Phone = studentRequest.Phone,
			Photo = file == null ? "media/photo/nophoto.png" : await UploadPhotoAsync(file)
		};

		await studentRepository.AddStudentAsync(student);
		return Result<string>.Success("Ok");
	}

	public async Task<Result<List<Student>>> GetStudentListAsync()
	{
		return Result<List<Student>>.Success(await studentRepository.GetStudentListAsync());
	}

	public async Task<Result<Student?>> GetStudentByIdAsync(Guid studentId)
	{
		return Result<Student?>.Success(await studentRepository.GetStudentByIdAsync(studentId));
	}

	public async Task<Result<string>> UpdateStudentAsync(Guid studentId, StudentRequest studentRequest, IFormFile? file)
	{
		var student = await studentRepository.GetStudentByIdAsync(studentId);
		if (student == null)
			return Result<string>.Failure("Student does not exist");

		student.Name = studentRequest.Name;
		student.Email = studentRequest.Email;
		student.Document = studentRequest.Document;
		student.Phone = studentRequest.Phone;

		if (file != null)
		{
			await DeletePhotoAsync(student.Photo);
			
			student.Photo = await UploadPhotoAsync(file);
		}

		await studentRepository.UpdateStudentAsync(student);
		return Result<string>.Success("Ok");
	}

	public async Task<Result<string>> DeleteStudentAsync(Guid studentId)
	{
		var student = await studentRepository.GetStudentByIdAsync(studentId);
		if (student == null)
			return Result<string>.Failure("Student does not exist");

		await DeletePhotoAsync(student.Photo);

		await studentRepository.DeleteStudentAsync(student);
		return Result<string>.Success("Ok");
	}

	private async Task<string> UploadPhotoAsync(IFormFile file)
	{
		var urlPath = $"media/photo/{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

		using var stream = new MemoryStream();
		await file.CopyToAsync(stream);

		await s3Client.PutObjectAsync(new PutObjectRequest
		{
			BucketName = bucketName,
			Key = urlPath,
			InputStream = stream
		});

		return urlPath;
	}

	private async Task DeletePhotoAsync(string photo)
	{
		if (photo == "media/photo/nophoto.png")
			return;

		await s3Client.DeleteObjectAsync(new DeleteObjectRequest
		{
			BucketName = bucketName,
			Key = photo
		});
	}
}
