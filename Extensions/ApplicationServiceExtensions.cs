using dating_backend.Data;
using dating_backend.Helpers;
using dating_backend.Interfaces;
using dating_backend.Services;
using Microsoft.EntityFrameworkCore;

namespace dating_backend.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            // Here we setting our db context to our builder, dbContext provides us features to connect to our database using EF.
            services.AddDbContext<DataContext>(opt => opt.UseSqlite(config.GetConnectionString("DefaultConnection")));
            services.AddCors();  // Adding cors to our application to allow origins to send methods and headers.
            services.AddScoped<ITokenService, TokenService>(); // Adding the Token by scoped version.
            services.AddScoped<IUserRepository, UserRepository>(); // Adding User Repository to the scope of the http request.
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // Adding AutoMapper Service.
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings")); // Configure Cloudinary to our application using the helper settings we created.
            services.AddScoped<IPhotoService, PhotoService>(); // Adding our cloudinary photo service.
            services.AddScoped<LogUserActivity>();
            return services;
        }
    }
}
