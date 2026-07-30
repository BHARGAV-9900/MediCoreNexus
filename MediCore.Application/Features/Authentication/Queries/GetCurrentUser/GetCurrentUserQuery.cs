using MediatR;

namespace MediCore.Application.Features.Authentication.Queries.GetCurrentUser;

public class GetCurrentUserQuery : IRequest<CurrentUserProfileDto>
{
}