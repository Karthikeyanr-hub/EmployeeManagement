using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementWebAPI.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDBContext db = new EmployeeDBContext();
        public async Task Add(Employee employee)
        {
            employee.Id = Guid.NewGuid().ToString();
            db.Employees.Add(employee);
            try
            {
                await db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task<Employee> GetEmployee(string id)
        {
            try
            {
                Employee employee = await db.Employees.FindAsync(id);
                if (employee == null)
                {
                    return null;
                }
                return employee;
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<Employee>> GetEmployees()
        {
            try
            {
                return await db.Employees.ToListAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task Update(Employee employee)
        {
            try
            {
                db.Entry(employee).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task Delete(string id)
        {
            try
            {
                Employee employee = await db.Employees.FindAsync(id);
                db.Employees.Remove(employee);
                await db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        private bool EmployeeExists(string id)
        {
            return db.Employees.Count(e => e.Id == id) > 0;
        }

        public async Task LogException(Exception ex)
        {
            try
            {
                var exceptionLog = new ExceptionLogs();
                exceptionLog.Id = Guid.NewGuid().ToString();
                exceptionLog.ErrorMsg = ex.ToString();
                db.ExceptionLogs.Add(exceptionLog);
                await db.SaveChangesAsync();
            }
            catch
            {
            }

        }

    }
}