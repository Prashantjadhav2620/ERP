using ERPAPI.Entity;
using ERPAPI.Entity.DBContext;
using ERPAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Threading.Tasks;

namespace ERPAPI.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: api/employee/GetEmployeeList
        [HttpGet("GetEmployeeList")]
        public async Task<IActionResult> GetEmployee()
        {
            try
            {
                Log.Information("GetEmployee Called");
                var employees = await _employeeService.GetEmployee();

                if (employees == null || employees.Count == 0)
                {
                    Log.Information("No employees found.");
                    return NotFound("Data not found");
                }
                Log.Information("Employees retrieved successfully.");
                return Ok(new { success = true, message = "Employees retrieved successfully", data = employees });
            }
            catch (Exception ex)
            {
                Log.Information(ex, "Error retrieving employees.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/employee/GetEmployeeById/{id}
        [HttpGet("GetEmployeeById/{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeById(id);

                if (employee == null)
                {
                    Log.Information("Employee not found.");
                    return NotFound("Employee not found");
                }
                Log.Information("Employee retrieved successfully.");
                return Ok(new { success = true, message = "Employee retrieved successfully", data = employee });
            }
            catch (Exception ex)
            {
                Log.Information(ex, "Error retrieving employee.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/employee/SaveEmployee
        [HttpPost("AddUpdateEmployee")]
        public async Task<IActionResult> AddUpdateEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (employee.Id == 0)
                {
                    await _employeeService.AddUpdateEmployee(employee);
                    Log.Information("Employee added successfully.");
                    return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, new { success = true, message = "Employee added successfully", data = employee });
                }
                else
                {
                    await _employeeService.AddUpdateEmployee(employee);
                    Log.Information("Employee updated successfully.");
                    return Ok(new { success = true, message = "Employee updated successfully", data = employee });
                }
            }
            catch (Exception ex)
            {
                Log.Information(ex, "Error adding/updating employee.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/employee/DeleteEmployee/{id}
        [HttpDelete("DeleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                Log.Information("Employee not found.");
                return NotFound("Employee not found");
            }

            try
            {
                await _employeeService.DeleteEmployee(employee);
                Log.Information("Employee deleted successfully.");
                return Ok(new { success = true, message = "Employee deleted successfully", id });
            }
            catch (Exception ex)
            {
                Log.Information(ex, "Error deleting employee.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}




