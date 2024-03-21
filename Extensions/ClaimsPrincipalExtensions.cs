using System.Security.Claims;

namespace dating_backend.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserName(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name)?.Value; // Getting our user from the JWT Token we provided in the ClaimsPrincipal of our application.
        }

        public static int? GetUserId(this ClaimsPrincipal user)
        {
            var userIdValue = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdValue, out int userId))
            {
                return userId;
            }
            return null;
        }

    }
}
