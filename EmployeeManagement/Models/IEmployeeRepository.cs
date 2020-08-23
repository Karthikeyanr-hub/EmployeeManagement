using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EmployeeManagement.Models
{
    public interface IEmployeeRepository
    {
        Task Add(Employee employee);
        Task Update(Employee employee);
        Task Delete(string id);
        Task<Employee> GetEmployee(string id);
        Task<List<Employee>> GetEmployees();
    }
}