using dating_backend.Entities;
using Microsoft.AspNetCore.Identity;

namespace dating_backend.Services
{
    public class RoleInitializerService
    {
        private readonly RoleManager<Role> _roleManager;

        public RoleInitializerService(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task InitializeRoles()
        {
            string[] roles = ["Member", "Admin", "Moderator"];

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new Role { Name = role });
                }
            }
        }
    }
}
