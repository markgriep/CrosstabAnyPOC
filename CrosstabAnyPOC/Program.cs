using Colorful;
using CrosstabAnyPOC.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Reflection;
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

            var transit = TestingGroup.T;
            var nonTransit = TestingGroup.N;
            var dot = TestingGroup.D;

            // make the "choice" It will be done in the UI
            var grp = transit; // "T" for Transit


            BigPrint("Employees for");
            BigPrint("the pool");
            BigPrint($"{GetEnumDisplayName(grp)}");



            // Create a ___POOL___
            // Simple Match employees with the active mappings
            var SelectionPool = _employees.Where(emp =>
                _mappings.Any(map =>
                    map.IsActive && // Only match with active mappings
                    map.CostCenterID == emp.DepartmentID &&
                    map.TestingGroup == grp.ToString() &&
                    map.JobCodeID == emp.JobCode)).ToList();








            // Print matched employees in the ___POOL___
            Console.WriteLine("Matched Employees:");
            foreach (var emp in SelectionPool)
            {
                Console.WriteLine($"{emp.Name} - DepartmentID: {emp.DepartmentID}, JobCode: {emp.JobCode}");
            }






            // am i going to have to have the test number at this point?  Proabaly ...


            BigPrint("random name");
            
            // Invoke to generate a single random full name
            string singleName = NameUtility.GenerateRandomFullName();
            Console.WriteLine($"Generated Single Name: {singleName}");


            BigPrint("Some names");


            // Invoke to generate a list of unique full names
            int numberOfNames = 100; // specify the number of unique names you want
            List<string> nameList = NameUtility.GenerateUniqueFullNames(numberOfNames);
            Console.WriteLine("Generated List of Names:");
            foreach (var name in nameList)
            {
                Console.WriteLine(name);
            }
















            //// Sanity checks
            //Utility.PrintMapping(_mappings);
            //Utility.PrintEmployees(_employees);

            Console.ReadKey();

        }// end of main()

        private static void BigPrint(string str)
        {
            FigletFont font = FigletFont.Load("figlet/Straight.flf");
            Figlet figlet = new Figlet(font);

            Console.WriteLine(figlet.ToAscii(str), ColorTranslator.FromHtml("#8AFFEF"));
        }


        public static string GetEnumDisplayName(Enum value)
        {
            return value.GetType().GetField(value.ToString())?
                       .GetCustomAttribute<DisplayAttribute>()?
                       .Name ?? value.ToString();
        }


    }
} // End of namespace 
