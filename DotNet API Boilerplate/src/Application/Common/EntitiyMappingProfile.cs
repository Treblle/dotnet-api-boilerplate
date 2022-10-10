namespace Treblle_Core_API_Boilerplate.Core.Common;
using AutoMapper;
using Treblle_Core_API_Boilerplate.Core.Common.Dto;
using Treblle_Core_API_Boilerplate.Core.Common.Entities;

internal class EntitiyMappingProfile : Profile
{
    public EntitiyMappingProfile()
    {
        CreateMap<Entity, EntityDto>()
            .ReverseMap();
    }
}
