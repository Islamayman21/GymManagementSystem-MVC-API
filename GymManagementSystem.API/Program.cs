using GymMs.DAL.GymMs.DAL.Context;
using Microsoft.EntityFrameworkCore;
namespace GymManagementSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // DbContext
            builder.Services.AddDbContext<GymDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add controllers + swagger
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // CORS - allow your MVC project (adjust origin)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalMVC", policy =>
                {
                    policy.WithOrigins("https://localhost:7176") // MVC URL
                          .AllowAnyHeader().AllowAnyMethod();
                });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowLocalMVC");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
