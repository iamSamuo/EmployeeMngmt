using EmployeeMngmtApi.Data;
using EmployeeMngmtApi.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMngmtApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // configure database context and aslo 
            // specify the database provider

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("EmployeeDb"));
            // confire cors policy
            builder.Services.AddCors(options =>
          {
              options.AddPolicy("MyCors", builder =>
              {
                  builder.WithOrigins("http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
              });
          });
            //prepare the dependency injection for repository
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            // configure the application to use controllers
            builder.Services.AddControllers();
            // add swagger configuration on startup
            // helps finding the endpoints(controllers)
            builder.Services.AddEndpointsApiExplorer();
            // instantiate swagger ui middleware
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            // add swagger middleware only when in development environment
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                    // removes the default api prefix and replace with swagger
                    c.RoutePrefix = string.Empty;
                }
                );


         
            };
            app.UseCors("MyCors");
            // removed the default api route configuration
            // allow swagger to call the controllers directly
            app.MapControllers();

            app.Run();
        }
    }
}
