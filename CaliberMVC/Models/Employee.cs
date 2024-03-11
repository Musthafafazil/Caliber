using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CaliberMVC.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string ExperienceLevel { get; set; }

        public string Gender { get; set; }

        public string Address { get; set; }

        public bool IsDeleted { get; set; }

    }
}