namespace Treblle_Core_API_Boilerplate.Core.Users.Queries.GetUserById;
using MediatR;
using System.ComponentModel.DataAnnotations;
using Treblle_Core_API_Boilerplate.Core.Users.Dto;

public class GetUserByIdQuery : IRequest<UserDto>
{
    [Required]
    public Guid Id { get; init; }
}
