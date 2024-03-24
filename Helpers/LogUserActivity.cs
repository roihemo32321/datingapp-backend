using dating_backend.Extensions;
using dating_backend.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace dating_backend.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            // Check if the user is authenticated
            if (!resultContext.HttpContext.User.Identity.IsAuthenticated)
            {
                return;
            }

            // Get the user ID from the claims
            var userId = resultContext.HttpContext.User.GetUserId();

            // Get the user repository from the service provider
            var repo = resultContext.HttpContext.RequestServices.GetRequiredService<IUserRepository>();

            // Get the user by ID and update the last active date
            var user = await repo.GetUserByIdAsync(userId);
            if (user != null)
            {
                user.LastActive = DateTime.UtcNow;
                await repo.SaveAllAsync();
            }
        }
    }
}
