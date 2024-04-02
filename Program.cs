using dating_backend.Extensions;
using dating_backend.Middleware;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices(builder.Configuration); // Custom extension for app services method to make our file clean.
builder.Services.AddIdentityServices(builder.Configuration); // // Custom extension for identity services method to make our file clean.

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>(); // Using the middleware we created for error handling.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Services.ApplyMigrations(); // Apply migrations method to apply migrations to the database.
app.Services.InitializeRoles(); // Initialize roles method to seed roles to the database.
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
