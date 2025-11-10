using EmployeeMngmtApi.Data;
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
            var app = builder.Build();
            app.UseCors("MyCors");

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
