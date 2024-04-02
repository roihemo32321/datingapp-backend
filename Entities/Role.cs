using Microsoft.AspNetCore.Identity;

namespace dating_backend.Entities
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
