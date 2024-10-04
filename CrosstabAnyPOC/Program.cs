using CrosstabAnyPOC.Models;

namespace CrosstabAnyPOC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var _mappings = JobToDepartmentMapping.GetMockMappings();
            var _employees = WorkDayEmployee.GetMockEmployees();

            // Sanity checks
            Utility.PrintMapping(_mappings);
            Utility.PrintEmployees(_employees);

            Console.ReadKey();

        }// end of main()
    }
} // End of namespace 
