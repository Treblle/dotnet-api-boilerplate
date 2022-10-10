namespace Treblle_Core_API_Boilerplate.Core.Users.Queries.GetUsers;
using MediatR;
using Treblle_Core_API_Boilerplate.Core.Users.Dto;

public class GetUsersQuery : IRequest<List<UserDto>>
{
}
