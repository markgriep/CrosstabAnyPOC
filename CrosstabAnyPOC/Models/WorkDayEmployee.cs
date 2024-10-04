namespace CrosstabAnyPOC.Models
{
    //#########################################################################
    public class WorkDayEmployee
    {
        public string Name { get; set; }      // Employee's Name
        public int DepartmentID { get; set; } // Department ID (integer)
        public string JobCode { get; set; }   // Job Code (alphanumeric)

        public WorkDayEmployee(string name, int departmentID, string jobCode)
        {
            Name = name;
            DepartmentID = departmentID;
            JobCode = jobCode;
        }

        // Method to create a list of mock employees
        public static List<WorkDayEmployee> GetMockEmployees()
        {
            return new List<WorkDayEmployee>
        {
            new WorkDayEmployee("Alice", 101, "DEV"),
            new WorkDayEmployee("Bob", 101, "DEV"),
            new WorkDayEmployee("Charlie", 909, "BUS Driver"),
            new WorkDayEmployee("Diana", 909, "BUS Driver"),
            new WorkDayEmployee("Edward", 800, "Lineman"),
            new WorkDayEmployee("Fiona", 800, "Lineman"),
            new WorkDayEmployee("George", 103, "FIN"),
            new WorkDayEmployee("Hannah", 104, "IT"),
            new WorkDayEmployee("Ian", 101, "DEV"),
            new WorkDayEmployee("Julia", 105, "MKT")
        };
        }

    }












} // End of namespace CrosstabAnyPOC
