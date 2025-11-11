using EmployeeMngmtApi.Models;
using EmployeeMngmtApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMngmtApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            if (employee != null)
            {
                await _employeeRepository.AddEmployeeAsync(employee);
                // Return a Created response with the employee object  
                return Created(string.Empty, employee);

            }
            // Return BadRequest if the employee is null 
            return BadRequest("Employee data is null.");
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return Ok(employees);
        }
        

    }
    }
