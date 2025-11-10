using EmployeeMngmtApi.Models;

namespace EmployeeMngmtApi.Repositories
{
    public interface IEmployeeRepository
    {
        // functions for CRUD operations
        // (make sure it is async to allow multithreading)
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?>GetByIdAsync(int id);
        Task AddEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task<bool> DeleteEmployeeAsync(int id);
    }
}
