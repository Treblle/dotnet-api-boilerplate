namespace DotNet_API_Boilerplate.Core.Common.Entities;
using System.ComponentModel.DataAnnotations;

public abstract record Entity
{
    [Key]
    public int Id { get; set; }
    public Guid Uuid { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }
}
