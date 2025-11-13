using EmployeeMngmtApi.Models;
using EmployeeMngmtApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                return CreatedAtAction(nameof(GetEmployeeById), new {id = employee.Id});
            }
            // Return BadRequest if the employee is null 
            return BadRequest("Employee data is null.");
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee != null)
            {
                return Ok(employee);
            }
            return NotFound($"Employee with Id = {id} not found.");
        }
        // get all employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return Ok(employees);
        }
        [HttpPut]
        public async Task<ActionResult<Employee>> UpdateEmployee(Employee employee)
        {
            try
            {
                await _employeeRepository.UpdateEmployeeAsync(employee);
                return Ok(employee);
            }
            catch (DbUpdateConcurrencyException)
            {
                // Employee doesn't exist
                return NotFound($"Employee with Id = {employee.Id} not found.");
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            var existingEmployee = await _employeeRepository.GetByIdAsync(id);
            if (existingEmployee != null)
            {
                bool isDeleted = await _employeeRepository.DeleteEmployeeAsync(id);
                if (isDeleted)
                {
                    return Ok($"Employee with Id = {id} deleted.");
                }
                return StatusCode(500, "A problem happened while handling your request.");
            }
            return NotFound($"Employee with Id = {id} not found.");

        }
    }
    }
