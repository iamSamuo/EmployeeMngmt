using EmployeeMngmtApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMngmtApi.Data
{
    public class AppDbContext: DbContext
    {
        // adding a model to a database context to create, update and delete
        public DbSet<Employee> Employees { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {
              
        }
    }
}
