using AutoMapper;
using WebProgram.Data.Entities.Identity;
using WebProgram.Areas.Admin.Models.Users;

namespace WebProgram.Areas.Admin.Mapper;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<UserEntity, UserItemViewModel>()
            .ForMember(x => x.Image, opt => opt.MapFrom(x => x.Image))
            .ReverseMap();

    }
}
