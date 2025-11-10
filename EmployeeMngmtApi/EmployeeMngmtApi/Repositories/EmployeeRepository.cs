using EmployeeMngmtApi.Data;
using EmployeeMngmtApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMngmtApi.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private readonly AppDbContext _context;
        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddEmployeeAsync(Employee employee)
        {
            try
            {
                await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding employee", ex);
            }
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employeeToDelete = await _context.Employees.FindAsync(id);
            if (employeeToDelete == null)
            {
                throw new KeyNotFoundException($"Employee with id {id} was not found.");
            }
            try
            {
                _context.Employees.Remove(employeeToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting employee", ex);

            }
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            try
            {
                return await _context.Employees.ToListAsync();

            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving employees", ex);
            }
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);

            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving employee by id", ex);
            }

        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            try
            {

                _context.Employees.Update(employee);
               await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating employee", ex);
            }
        }
    }
}
