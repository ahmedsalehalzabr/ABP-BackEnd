using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Services;

namespace ABPCourse.Demo1.Bases
{
   // [Authorize] الكل ايكون اثرايز
    public class BaseApplicationService : ApplicationService
    {
        public BaseApplicationService()
        {
            
        }
    }
}
