using CrosstabAnyPOC.Models;

namespace CrosstabAnyPOC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var _mappings = JobToDepartmentMapping.GetMockMappings();
            var _employees = WorkDayEmployee.GetMockEmployees();






            // Simple Match employees with the active mappings
            var SelectionPool = _employees.Where(emp =>
                _mappings.Any(map =>
                    map.IsActive && // Only match with active mappings
                    map.CostCenterID == emp.DepartmentID &&
                    map.JobCodeID == emp.JobCode)).ToList();






            // Print matched employees
            Console.WriteLine("Matched Employees:");
            foreach (var emp in SelectionPool)
            {
                Console.WriteLine($"{emp.Name} - DepartmentID: {emp.DepartmentID}, JobCode: {emp.JobCode}");
            }








            //// Sanity checks
            //Utility.PrintMapping(_mappings);
            //Utility.PrintEmployees(_employees);

            Console.ReadKey();

        }// end of main()
    }
} // End of namespace 
