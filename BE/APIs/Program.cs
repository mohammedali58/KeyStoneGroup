using Application;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using WebAPI.Middlewares;

namespace APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                            policy => policy.WithOrigins("http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            var app = builder.Build();

            //Use Middlewares
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseCors("AllowSpecificOrigin");


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                using (var scope = app.Services.CreateScope())
                {
                    var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
                    await initializer.InitializeAsync();
                    await initializer.SeedDataAsync();
                    
                }
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
