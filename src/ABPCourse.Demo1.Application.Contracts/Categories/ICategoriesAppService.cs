using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ABPCourse.Demo1.Categories
{
    public interface ICategoriesAppService : ICrudAppService
        <CategoryDto, int ,PagedAndSortedResultRequestDto,CreateUpdateCategoryDto>
    {
    }
}
