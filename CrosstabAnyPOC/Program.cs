using Colorful;
using CrosstabAnyPOC.Models;
using System.Drawing;
using Console = Colorful.Console;

namespace CrosstabAnyPOC
{
    internal class Program
    {
        static void Main()
        {
            var _mappings = JobToDepartmentMapping.GetMockMappings();
            var _employees = WorkDayEmployee.GetMockEmployees();


           var _settings = new DrugTestSettings
           {
               TestNumber = 1,
               TestOperatorName = "Mark G",
               RequestDateTime = DateTime.Now,
               TestType = "Random",
               Group = "All Employees",
               TestSubjectSelectionMethod = TestSubjectSelectionMethod.Automatic,
               PercentageOfEmployeesToTest = 0.54  // 54 percent 
           };





            // this will give a ___POOL___
            // Simple Match employees with the active mappings
            var SelectionPool = _employees.Where(emp =>
                _mappings.Any(map =>
                    map.IsActive && // Only match with active mappings
                    map.CostCenterID == emp.DepartmentID &&
                    map.TestingGroup == "N" &&
                   
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




            FigletFont font = FigletFont.Load("chunky.flf");
            Figlet figlet = new Figlet(font);

            Console.WriteLine(figlet.ToAscii("Belvedere"), ColorTranslator.FromHtml("#8AFFEF"));
            Console.WriteLine(figlet.ToAscii("ice"), ColorTranslator.FromHtml("#FAD6FF"));
            Console.WriteLine(figlet.ToAscii("cream."), ColorTranslator.FromHtml("#B8DBFF"));



            Console.ReadKey();

        }// end of main()
    }
} // End of namespace 
