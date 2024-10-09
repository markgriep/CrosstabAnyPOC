using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosstabAnyPOC.Models
{




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


    /// <summary>
    /// DO I REALLY NEED THIS??
    /// </summary>
    internal class PoolSettings
    {

        //public int TestNumber { get; set; }
        //public string TestOperatorName { get; set; } = string.Empty;
        //public DateTime RequestDateTime { get; set; }
        //public string TestType { get; set; } = string.Empty;
        //public string Group { get; set; } = string.Empty;
        //public TestingGroup TestingGroup { get; set; }


      

    }


}
