namespace Treblle_Core_API_Boilerplate.Core.Common.Dto;
public abstract record EntityDto
{
    public Guid Uuid { get; init; }
    public DateTime DateCreated { get; init; }
    public DateTime DateModified { get; init; }
}
