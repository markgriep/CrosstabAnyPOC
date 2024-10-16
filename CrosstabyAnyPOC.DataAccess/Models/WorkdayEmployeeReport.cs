using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CrosstabAnyPOC.DataAccess.Models
{


    /// <summary>
    /// This is the ROOT of the XML response from the Workday API. In WorkDay parlance, a "report" object
    /// </summary>
    [XmlRoot(ElementName = "Report_Data", Namespace = "urn:com.workday.report/RandomDrugTestSelectionPool")]
    public class WorkdayEmployeeReport
    {

        /// <summary>
        /// These are the sub elements, individual employees in the report.  a list of WORKDAY EMPLOYEE objects
        /// </summary>
        [XmlElement(ElementName = "Report_Entry", Namespace = "urn:com.workday.report/RandomDrugTestSelectionPool")]
        public List<WorkdayEmployee> Employees { get; set; } = new List<WorkdayEmployee>();

    }

}


