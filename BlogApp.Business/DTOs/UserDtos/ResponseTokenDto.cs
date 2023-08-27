namespace BlogApp.Business.DTOs.UserDtos;

public record ResponseTokenDto
{
    public string Token { get; set; }
    public string UserName { get; set; }
    public DateTime Expires { get; set; }
}
