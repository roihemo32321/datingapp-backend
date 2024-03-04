using System.Security.Claims;

namespace dating_backend.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserName(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Getting our user from the JWT Token we provided in the ClaimsPrincipal of our application.
        }
    }
}
