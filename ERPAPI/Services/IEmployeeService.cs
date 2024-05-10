using ERPAPI.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPAPI.Services
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetEmployee();
        Task<Employee> GetEmployeeById(int id);
        Task AddUpdateEmployee(Employee employee);
        Task DeleteEmployee(Employee employee);
        bool EmployeeExists(int id);
    }
}
