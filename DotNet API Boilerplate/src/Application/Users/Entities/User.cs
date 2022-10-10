namespace Treblle_Core_API_Boilerplate.Core.Users.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Treblle_Core_API_Boilerplate.Core.Posts.Entities;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [InverseProperty("User")]
    public ICollection<Post> Posts { get; set; }
}
