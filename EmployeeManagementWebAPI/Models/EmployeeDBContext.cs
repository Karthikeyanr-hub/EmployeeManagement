using System.Data.Entity;

namespace EmployeeManagementWebAPI.Models
{

    public class EmployeeDBContext : DbContext
    {
        public EmployeeDBContext() : base("name=DBConnection")
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ExceptionLogs> ExceptionLogs { get; set; }
    }
}