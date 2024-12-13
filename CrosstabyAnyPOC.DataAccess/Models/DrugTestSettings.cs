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
        public int TestNumber { get; set; }

        public string TestOperatorName { get; set; } = string.Empty;

        public DateTime RequestDateTime { get; set; }

        public TestType TestType { get; set; }

        public TestingGroup TestingGroup { get; set; }

        public TestCategory TestCategory { get; set; }

        public TestSubjectSelectionMethod TestSubjectSelectionMethod { get; set; }


        public decimal PercentageOfEmployeesToDrugTest { get; set; }

        public decimal PercentageOfEmployeesToAlcoholTest { get; set; }

        public int NumberOfEmployeesToDrugTest { get; set; }

        public int NumberOfEmployeesToAlcoholTest { get; set; }


        public int EmployeePoolSize { get; set; }


        public List<int>    DrugTestHashset { get; set; } = new List<int>();
        public List<int> AlcoholTestHashset { get; set; } = new List<int>();


        public string? DrugSelectionPattern { get; set; }

        public string? AlcoholSelectionPattern { get; set; }
    }
}
