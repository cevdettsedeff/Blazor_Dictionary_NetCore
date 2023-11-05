using Microsoft.AspNetCore.Mvc.Filters;

namespace BlazorDictionary.Api.WebApi.Infrastructure.ActionFilters
{
    public class ValidationModalStateFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if(!context.ModelState.IsValid)
            {
                var message = context.ModelState.Values.SelectMany(x => x.Errors)
                    .Select(y => !string.IsNullOrEmpty(y.ErrorMessage)
                    ? y.ErrorMessage : y.Exception?.Message).Distinct().ToList();

                return;
            }

            await next();
        }
    }
}
