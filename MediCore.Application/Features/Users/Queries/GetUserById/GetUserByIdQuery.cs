using MediatR;
using MediCore.Application.Features.Users.DTOs;

namespace MediCore.Application.Features.Users.Queries.GetUserById;

public class GetUserByIdQuery : IRequest<UserDto>
{
    public int Id { get; set; }

    public GetUserByIdQuery(int id)
    {
        Id = id;
    }
}