namespace MediCore.Application.Features.Users.DTOs;

public class UserDto
{
    public int Id { get; set; }

    public Guid PublicId { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public int RoleId { get; set; }

    public string Role { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }
}