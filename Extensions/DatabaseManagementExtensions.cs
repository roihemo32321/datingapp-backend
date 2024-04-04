using dating_backend.Data;
using dating_backend.Services;
using Microsoft.EntityFrameworkCore;

namespace dating_backend.Extensions
{
    public static class DatabaseManagementExtensions
    {
        public static async Task ResetConnectionsTableAsync(this IServiceProvider services)
        {
            using var serviceScope = services.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<DataContext>();

            // Assuming your Connections table is named "Connections"
            await context.Database.ExecuteSqlRawAsync("DELETE FROM [Connections]");
        }

        public static void InitializeRoles(this IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var roleInitializerService = scope.ServiceProvider.GetRequiredService<RoleInitializerService>();
                roleInitializerService.InitializeRoles().Wait();
            }
        }

        public static void ApplyMigrations(this IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DataContext>();
                context.Database.Migrate();
            }
        }
    }
}
