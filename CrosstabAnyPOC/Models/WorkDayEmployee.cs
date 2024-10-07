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
                //                    Name          DeptID       JobCode
                new WorkDayEmployee("Alice",        990,           "001"),
                new WorkDayEmployee("X-Bob",        990,           "009"),
                new WorkDayEmployee("Charlie",      990,           "003"),

                new WorkDayEmployee("Diana",        119,           "005"),
                new WorkDayEmployee("Edward",       119,           "005"),
                new WorkDayEmployee("Fiona",        119,           "005"),
                new WorkDayEmployee("X-George",     119,           "009"),

                new WorkDayEmployee("Hannah",       111,           "005"),
                new WorkDayEmployee("Ian",          111,           "005"),
                new WorkDayEmployee("Jonah",        111,           "005"),
                new WorkDayEmployee("Kevin",        111,           "005"),

                new WorkDayEmployee("Lan",          005,           "022"),
                new WorkDayEmployee("X-Man",        005,           "029"),
                new WorkDayEmployee("X-Nud",        005,           "028"),
                new WorkDayEmployee("Odd",          005,           "023")
            };
        }




    }












} // End of namespace CrosstabAnyPOC
