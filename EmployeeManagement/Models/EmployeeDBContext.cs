using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace EmployeeManagement.Models
{

    public class EmployeeDBContext : DbContext
    {
        public EmployeeDBContext() : base("name=DBConnection")
        {
        }
        public DbSet<Employee> Employees { get; set; }
    }
}