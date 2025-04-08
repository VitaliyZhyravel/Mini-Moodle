using Microsoft.AspNetCore.Mvc.Filters;
using Mini_Moodle.Models.Domain;

namespace Mini_Moodle.Filters;

public class ActionFilterValidateQueryParameters : IAsyncActionFilter
{
    readonly List<string> availableParam = new List<string>
    {
        nameof(Course.Title),
        nameof(Course.Description)
    };

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {

        if (context.ActionArguments.ContainsKey("filterBy"))
        {
            var queryParam = Convert.ToString(context.ActionArguments["filterBy"]);

            if (queryParam == null || !availableParam.Any(x => x == queryParam))
            {
                context.ActionArguments["filterBy"] = nameof(Course.Title);
            }

        }
        if (context.ActionArguments.ContainsKey("sortBy"))
        {
            var queryParam = Convert.ToString(context.ActionArguments["sortBy"]);

            if (queryParam == null || !availableParam.Any(x => x == queryParam))
            {
                context.ActionArguments["sortBy"] = nameof(Course.Title);
            }
        }
       
        await next();
    }
}
