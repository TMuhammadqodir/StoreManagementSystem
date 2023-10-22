using AutoMapper;
using StoreManagementSystem.Domain.Entities;
using StoreManagementSystem.Service.DTOs.Stories;
using StoreManagementSystem.Service.DTOs.StoreManagers;
using StoreManagementSystem.Service.DTOs.StoryManagers;

namespace StoreManagementSystem.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //story
        CreateMap<Store, StoreCreationDto>().ReverseMap();
        CreateMap<Store, StoreUpdateDto>().ReverseMap();
        CreateMap<Store, StoreResultDto>().ReverseMap();

        //storyManager
        CreateMap<StoreManager, StoreManagerCreationDto>().ReverseMap();
        CreateMap<StoreManager, StoreManagerUpdateDto>().ReverseMap();
        CreateMap<StoreManager, StoreManagerResultDto>().ReverseMap();
    }
}
