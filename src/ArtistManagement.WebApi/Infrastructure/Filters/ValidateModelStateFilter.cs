using ArtistManagement.WebApi.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ArtistManagement.WebApi.Infrastructure.Filters
{
    public class ValidateModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errorResponse = new BadRequestResponse(context.ModelState);

                context.Result = new BadRequestObjectResult(errorResponse);
            }
        }
    }
}
