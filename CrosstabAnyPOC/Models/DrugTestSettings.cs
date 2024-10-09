using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosstabAnyPOC.Models
{

    public enum TestSubjectSelectionMethod
    {
        [Display(Name = "Automatic")]
        Automatic,
        [Display(Name = "Manual")]
        Manual
    }



    internal class DrugTestSettings
    {

        public int TestNumber { get; set; }

        public string TestOperatorName { get; set; } = string.Empty;
        
        public DateTime RequestDateTime { get; set; }
        
        public TestType TestType { get; set; } 
        //public string TestType { get; set; } = string.Empty;
        
        public string Group { get; set; } = string.Empty;
        
        public TestSubjectSelectionMethod TestSubjectSelectionMethod { get; set; }


        // Nullable properties, one will be used based on SelectionMethod
        public int NumberOfEmployeesToTest { get; set; }           // For MANUAL selection

        public decimal PercentageOfEmployeesToTest { get; set; }    // For AUTOMATIC selection

        public string? SelectionPattern { get; set; }               // To store the hashset for selecting employees
        public int EmployeePoolSize { get; set; }           // For MANUAL selection

    }


}
