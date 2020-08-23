using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeManagementWebAPI.Models
{
    public class ExceptionLogs
    {
        [Key]
        public string Id { get; set; }
        public string ErrorMsg { get; set; }
        public string CreatedDate { get; set; }
    }
}