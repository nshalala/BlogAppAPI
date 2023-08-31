namespace BlogApp.Business.DTOs.BlogDtos
{
	public record AuthorDto
	{
        public string  Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string? ImageUrl { get; set; }
    }
}
