﻿namespace BlogApp.Business.DTOs.UserDtos;

public record UserDetailDto
{
	public string Name { get; set; }
	public string Surname { get; set; }
	public string UserName { get; set; }
	public string Email { get; set; }
	public string Role { get; set; }
}
