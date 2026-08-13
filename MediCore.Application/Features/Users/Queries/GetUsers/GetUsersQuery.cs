using MediatR;
using MediCore.Application.Features.Users.DTOs;

namespace MediCore.Application.Features.Users.Queries.GetUsers;

public class GetUsersQuery : IRequest<IEnumerable<UserDto>>
{
}