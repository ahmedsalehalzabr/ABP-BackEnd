using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Volo.Abp.Application.Services;
using Volo.Abp.Validation;

namespace ABPCourse.Demo1.Bases
{
   // [Authorize] الكل ايكون اثرايز
    public class BaseApplicationService : ApplicationService
    {
        public BaseApplicationService()
        {
            
        }
        public AbpValidationException GetValidationException(
                                      FluentValidation.Results.ValidationResult validationResult)
        {
            var massage = string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage));
            var errors = validationResult.Errors
                .Select(x => new System.ComponentModel.DataAnnotations.ValidationResult(x.ErrorMessage, [x.PropertyName])).ToList();

            return new AbpValidationException(massage, errors);
        }
    }
}
