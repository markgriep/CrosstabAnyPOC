using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace CrosstabAnyPOC.DataAccess.Models
{
    
    public class WorkdayEmployee
    {

      
        public int Id { get; set; } // Primary Key


         public string EmployeeName { get; set; } = string.Empty;


        public int EmployeeId { get; set; }


        public string Department { get; set; } = string.Empty;

        public string JobCode { get; set; } = string.Empty;

        public string JobTitle { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; } 







    }
}




