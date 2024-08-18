using ABPCourse.Demo1.Categories;
using AutoMapper;

namespace ABPCourse.Demo1.Mappings
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CreateUpdateCategoryDto, Category>();
        }
    }
}
