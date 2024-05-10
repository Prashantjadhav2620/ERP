using ERPAPI.Entity;
using ERPAPI.Entity.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDBContext _applicationDBContext;

        public EmployeeService(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        public async Task<List<Employee>> GetEmployee()
        {
            return await _applicationDBContext.Employees
                                                .Where(e => !e.IsDeleted) 
                                                .ToListAsync();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _applicationDBContext.Employees
                                                .Where(e => e.Id == id && !e.IsDeleted) 
                                                .FirstOrDefaultAsync();
        }

        public async Task AddUpdateEmployee(Employee employee)
        {
            if (employee.Id == 0)
            {
                _applicationDBContext.Employees.Add(employee);
            }
            else
            {
                _applicationDBContext.Entry(employee).State = EntityState.Modified;
            }
            await _applicationDBContext.SaveChangesAsync();
        }

        public async Task DeleteEmployee(Employee employee)
        {
            _applicationDBContext.Employees.Remove(employee);
            await _applicationDBContext.SaveChangesAsync();
        }

        public bool EmployeeExists(int id)
        {
            return _applicationDBContext.Employees.Any(e => e.Id == id);
        }
    }
}
