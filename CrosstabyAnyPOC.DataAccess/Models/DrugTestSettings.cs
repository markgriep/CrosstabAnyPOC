using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosstabAnyPOC.DataAccess.Models
{

    public enum TestSubjectSelectionMethod
    {
        [Display(Name = "Automatic")]
        Automatic,
        [Display(Name = "Manual")]
        Manual
    }


    public enum TestingGroup
    {
        [Display(Name = "Transit")]
        T,
        [Display(Name = "Non-Tranist")]
        N,
        [Display(Name = "DOT-Other")]  
        O
    }


    public enum TestType
    {
        Drug,
        Alcohol,
        Both,
    }


    public enum TestCategory
    {
        Random,
        FollowUp,
        PostAccident,
        PreEmployment,
        ReasonableSuspision,
        Retest,
        ReturnToDuty,
    }



    public class DrugTestSettings
    {
        public int TestNumber { get; set; }                                             // 

        public string TestOperatorName { get; set; } = string.Empty;
        
        public DateTime RequestDateTime { get; set; }

        public TestType TestType { get; set; }                                          // ENUM Drug, alcohol or both

        public TestingGroup TestingGroup { get; set; }                                  // ENUM Transit, NonTransit, Other-DOT

        public TestCategory TestCategory { get; set; }                                  // ENUM Random, FollowUp, Post-Accident, etc

        public TestSubjectSelectionMethod TestSubjectSelectionMethod { get; set; }      // ENUM manual or automatic
                                                                                       
        public int NumberOfEmployeesToTest { get; set; }                                // Actual number to select, or dependent on percentage

        public decimal PercentageOfEmployeesToDrugTest { get; set; }                    // For AUTOMATIC DRUG selection

        public decimal PercentageOfEmployeesToAlcoholTest { get; set; }                 // For AUTOMATIC ALC. selection


        // Read only properties
        public int EmployeePoolSize { get; set; }                                       // For MANUAL selection

        public string? DrugSelectionPattern { get; set; }                               // To store the hashset for DRUG selecting employees

        public string? AlcoholSelectionPattern { get; set; }                            // To store the hashset for ALC. selecting employees
    }
}
