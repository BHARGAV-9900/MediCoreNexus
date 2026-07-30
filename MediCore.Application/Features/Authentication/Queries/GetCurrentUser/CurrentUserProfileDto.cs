namespace MediCore.Application.Features.Authentication.Queries.GetCurrentUser;

public class CurrentUserProfileDto
{
    public Guid PublicId { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;
}