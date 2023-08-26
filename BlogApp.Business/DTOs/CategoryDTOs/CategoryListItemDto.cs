namespace BlogApp.Business.DTOs.CategoryDTOs;

public record CategoryListItemDto
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string LogoUrl { get; set; }
	public bool IsDeleted { get; set; }
}
