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
        [Display(Name = "DOT")]
        D
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
        public int TestNumber { get; set; }                                 // 

        public string TestOperatorName { get; set; } = string.Empty;
        
        public DateTime RequestDateTime { get; set; }



        // ENUM Drug, alcohol or both
        public TestType TestType { get; set; }

        // ENUM DOT, NonDOT or Transit
        public TestingGroup TestingGroup { get; set; }

        // ENUM Random, FollowUp, Post-Accident, etc
        public TestCategory TestCategory { get; set; }

        // ENUM manual or automatic
        public TestSubjectSelectionMethod TestSubjectSelectionMethod { get; set; }



        // Nullable properties, one will be used based on SelectionMethod
        public int NumberOfEmployeesToTest { get; set; }            // For MANUAL selection

        public decimal PercentageOfEmployeesToTest { get; set; }    // For AUTOMATIC selection

        public string? SelectionPattern { get; set; }               // To store the hashset for selecting employees

        public int EmployeePoolSize { get; set; }                   // For MANUAL selection

    }
}
