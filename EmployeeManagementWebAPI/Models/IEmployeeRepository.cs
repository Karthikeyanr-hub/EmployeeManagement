using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagementWebAPI.Models
{
    public interface IEmployeeRepository
    {
        Task Add(Employee employee);
        Task Update(Employee employee);
        Task Delete(string id);
        Task<Employee> GetEmployee(string id);
        Task<List<Employee>> GetEmployees();
        Task LogException(Exception ex);
    }
}