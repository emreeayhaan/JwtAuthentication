using AutoMapper;
using Data.Dto;
using Data.Models;

namespace Business.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //Admin
            CreateMap<UserModels, UserRegisterModel>().ReverseMap();
            CreateMap<UserModels, UserDto>().ReverseMap();
            //User
            CreateMap<AdminModels, AdminRegisterModel>().ReverseMap();
            CreateMap<AdminModels, AdminDto>().ReverseMap();
        }
    }
}
