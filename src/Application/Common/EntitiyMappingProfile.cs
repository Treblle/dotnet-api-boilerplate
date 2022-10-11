namespace DotNet_API_Boilerplate.Core.Common;
using AutoMapper;
using DotNet_API_Boilerplate.Core.Common.Dto;
using DotNet_API_Boilerplate.Core.Common.Entities;

internal class EntitiyMappingProfile : Profile
{
    public EntitiyMappingProfile()
    {
        CreateMap<Entity, EntityDto>()
            .ReverseMap();
    }
}
