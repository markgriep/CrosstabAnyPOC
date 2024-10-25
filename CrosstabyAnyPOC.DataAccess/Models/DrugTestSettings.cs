using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosstabAnyPOC.DataAccess.Models
{



    public enum TestSubjectSelectionMethod      // AUTO, MANUAL
    {
        [Display(Name = "Automatic")]
        Automatic,
        [Display(Name = "Manual")]
        Manual
    }



    public enum TestingGroup                    // TRANSIT, NON-TRANSIT, OTHER-DOT
    {
        [Display(Name = "Transit")]
        T,
        [Display(Name = "Non-Tranist")]
        N,
        [Display(Name = "DOT-Other")]  
        O
    }


    public enum TestType                        // DRUG, ALCOHOL, BOTH
    {
        Drug,
        Alcohol,
        Both,
    }


    public enum TestCategory                    // RANDOM, FOLLOW-UP, POST-ACCIDENT, etc
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
        public int TestNumber { get; set; }                                             //  421

        public string TestOperatorName { get; set; } = string.Empty;                    //  JDOE
        
        public DateTime RequestDateTime { get; set; }                                   // 2021-07-01 12:00:00

        public TestType TestType { get; set; }                                          // ENUM - Drug, alcohol or both

        public TestingGroup TestingGroup { get; set; }                                  // ENUM - Transit, NonTransit, Other-DOT

        public TestCategory TestCategory { get; set; }                                  // ENUM - Random, FollowUp, Post-Accident, etc

        public TestSubjectSelectionMethod TestSubjectSelectionMethod { get; set; }      // ENUM - manual or automatic


        public decimal PercentageOfEmployeesToDrugTest { get; set; }                    // DECIMAL % DRUG selection

        public decimal PercentageOfEmployeesToAlcoholTest { get; set; }                 // DECIMAL % ALC. selection

        public int NumberOfEmployeesToDrugTest { get; set; }                            // INT actual number to Drug test, -OR- set via code based on percentage

        public int NumberOfEmployeesToAlcoholTest { get; set; }                         // INT actual number to Alco test, -OR- set via code based on percentage



        public int EmployeePoolSize { get;   }                                            // INT R/O For MANUAL selection

        public string? DrugSelectionPattern { get;  }                                   // STRING R/O, store hashset for DRUG selecting employees

        public string? AlcoholSelectionPattern { get; }                                // STRING  R/O, store hashset for ALC. selecting employees
    }
}
