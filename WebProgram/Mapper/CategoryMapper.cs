using AutoMapper;
using WebProgram.Data.Entities;
using WebProgram.Models.Category;

namespace WebProgram.Mapper
{
    public class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            CreateMap<CategoryEntity, CategoryItemViewModel>()
                .ForMember(x => x.Image, opt => opt.MapFrom(x => x.ImageUrl));
            CreateMap<CategoryCreateViewModel, CategoryEntity>()
                .ForMember(x => x.ImageUrl, opt => opt.Ignore());


            CreateMap<CategoryEditViewModel, CategoryEntity>()
                .ReverseMap();
        }
    }
}