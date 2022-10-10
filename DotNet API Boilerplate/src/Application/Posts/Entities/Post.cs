namespace Treblle_Core_API_Boilerplate.Core.Posts.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using Treblle_Core_API_Boilerplate.Core.Common.Entities;
using Treblle_Core_API_Boilerplate.Core.Users.Entities;

public record Post : Entity
{
    public string Title { get; set; }
    public string Content { get; set; }
    [ForeignKey("User")]
    public string UserId { get; set; }
    public User User { get; set; }
}
