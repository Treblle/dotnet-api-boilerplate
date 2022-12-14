namespace DotNet_API_Boilerplate.Core.Users.Queries.GetUsers;
using MediatR;
using DotNet_API_Boilerplate.Core.Users.Dto;

public class GetUsersQuery : IRequest<List<UserDto>>
{
}
