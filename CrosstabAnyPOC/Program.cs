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
            #region Variables





            // Enumerations for Testing Groups
            var transit = TestingGroup.T;
            var nonTransit = TestingGroup.N;
            var dot = TestingGroup.D;

            // make the "choice" It will be done in the UI
            //var grp = transit; // "T" for Transit
            //var grp = nonTransit; // "T" for Transit
            var grp = dot; // "T" for Transit




            //// Enumerations for Test Types
            //var drugTest = TestType.Drug;
            //var alcoholTest = TestType.Alcohol;
            //var bothTests =   TestType.Both;

            
            //var tst = drugTest; 
            ////var tst = alcoholTest; 
            ////var tst = bothTests; 





            var _mappings = JobToDepartmentMapping.GetMockMappings();
            var _employees = WorkDayEmployee.GenerateEmployeeList(12340);     // Generate a list of N employees



            // these are the settings for the test
            var _settings = new DrugTestSettings                                               
            {
                TestNumber = 1,
                TestOperatorName = "Mark G",
                RequestDateTime = DateTime.Now,
                TestType = TestType.Both,
                Group = "All Employees",
                TestSubjectSelectionMethod = TestSubjectSelectionMethod.Automatic,
                PercentageOfEmployeesToTest = 0.1M,  // X percent 
                NumberOfEmployeesToTest = 0,         // 0 employees
            };






            #endregion


            #region ACTION



            // Create a ___POOL___
            // Simple Match employees with the active mappings
            var SelectionPool = _employees.Where(emp =>
                _mappings.Any(map =>
                    map.IsActive &&                                     // ONLY match active mappings                           -AND-
                    map.CostCenterID == emp.DepartmentID &&             // CostCenter (departments) match each other            -AND-
                    map.TestingGroup == grp.ToString() &&               // testing group matches one of the enums.  (n, t, d)   -AND-
                    map.JobCodeID == emp.JobCode &&                     // Jobcodes match each other                            -AND-
                    true))                                              // always true place holder so I can insert others above
                    .ToList();



            // get total in the pool
            var totalInPool = SelectionPool.Count;


           
            if (_settings.TestSubjectSelectionMethod == TestSubjectSelectionMethod.Automatic)                    // if Automatic, 
            {
                _settings.NumberOfEmployeesToTest = (int)Math.Ceiling(_settings.PercentageOfEmployeesToTest * totalInPool);  // calculate the number of employees to test
            }
                                                                                                                 // otherwise number of employees is already set


            // make a call to get a random hashset            
            HashSet<int> randomNumbers =  SelectionManager.GetRandomHashset((int)_settings.NumberOfEmployeesToTest, totalInPool);

        
            _settings.SelectionPattern = string.Join(",", randomNumbers);               // store the hashset as comma separated string


            // create a linq query that selects the employees from the pool that match the random numbers
            var selectedEmployees = SelectionPool.Where((emp, index) => randomNumbers.Contains(index)).ToList();



















            #endregion


            #region PRINT DISPLAY






            BigPrint("The     P O O L");
            BigPrint($"{GetEnumDisplayName(grp)}");
            BigPrint($"{SelectionPool.Count}");

            // Print matched employees in the ___POOL___
            Console.WriteLine("Matched Employees:");
            foreach (var emp in SelectionPool)
            {
              
               Console.WriteLine($"{emp.Name,-25} Dept: {emp.DepartmentID,-5} JobCode: {emp.JobCode,-5}");    // Print employee details with leading zeros intact
            }





            
            BigPrint("SELECTED From the pool");
            BigPrint($"{selectedEmployees.Count}");

            // now loop through and print the selected employees
            foreach (var emp in selectedEmployees)
            {
                Console.WriteLine($"{emp.Name,-25} Dept: {emp.DepartmentID,-5} JobCode: {emp.JobCode,-5}");    // Print employee details with leading zeros intact
            }





            // print all the _settings

            BigPrint(" S E T T I N G S");
            int labelWidth = 35; // Set to ensure all labels align

            Console.WriteLine($"{"Test Number:".PadLeft(labelWidth)} {_settings.TestNumber}");                      // Right justify label, left justify value
            Console.WriteLine($"{"Test Operator:".PadLeft(labelWidth)} {_settings.TestOperatorName}");              // Right justify label, left justify value
            Console.WriteLine($"{"Request Date:".PadLeft(labelWidth)} {_settings.RequestDateTime}");                // Right justify label, left justify value
            Console.WriteLine($"{"Test Type:".PadLeft(labelWidth)} {_settings.TestType}");                          // Right justify label, left justify value
            Console.WriteLine($"{"Group:".PadLeft(labelWidth)} {_settings.Group}");                                 // Right justify label, left justify value
            Console.WriteLine($"{"Test Subject Selection Method:".PadLeft(labelWidth)} {_settings.TestSubjectSelectionMethod}"); // Right justify label, left justify value
            Console.WriteLine($"{"Percentage of Employees to Test:".PadLeft(labelWidth)} {_settings.PercentageOfEmployeesToTest}"); // Right justify label, left justify value
            Console.WriteLine($"{"Number of Employees to Test:".PadLeft(labelWidth)} {_settings.NumberOfEmployeesToTest}"); // Right justify label, left justify value

           

            // _settings.NumberOfEmployeesToTest = (int)(_settings.PercentageOfEmployeesToTest * totalInPool);
            
            Console.WriteLine($"\nPool  {totalInPool}");
            Console.WriteLine($"   X  {_settings.PercentageOfEmployeesToTest}");


            Console.WriteLine($"   =  {totalInPool * _settings.PercentageOfEmployeesToTest:0.00}");

            //print the random numbers string
            Console.WriteLine($"\nRandom Numbers:{_settings.SelectionPattern}");





            //BigPrint("ALL EMPLOYEES");

            //foreach (var employee in _employees)                                             // Loop through each employee
            //{
            //    Console.WriteLine($"{employee.Name,-25} Dept: {employee.DepartmentID,-5} JobCode: {employee.JobCode,-5}");    // Print employee details with leading zeros intact
            //}








            //   BigPrint("random name");

            //   // Invoke to generate a single random full name
            //   string singleName = NameUtility.GenerateRandomFullName();
            //  // Console.WriteLine($"Generated Single Name: {singleName}");


            //   BigPrint("Some names");


            //   // Invoke to generate a list of unique full names
            //   int numberOfNames = 100; // specify the number of unique names you want
            //   List<string> nameList = NameUtility.GenerateUniqueFullNames(numberOfNames);
            // //  Console.WriteLine("Generated List of Names:");
            //   foreach (var name in nameList)
            //   {
            ////       Console.WriteLine(name);
            //   }




            //// Sanity checks
            //Utility.PrintMapping(_mappings);
            //Utility.PrintEmployees(_employees);



            #endregion


            Console.ReadKey();


        }  // ----------- main()


        #region HELPERS


        private static void BigPrint(string str)
        {
            //FigletFont font = FigletFont.Load("figlet/Stick Letters.flf");
            //FigletFont font = FigletFont.Load("figlet/JS Stick Letters.flf");
            //FigletFont font = FigletFont.Load("figlet/Cybermedium.flf");
            //FigletFont font = FigletFont.Load("figlet/Graceful.flf");

            FigletFont font = FigletFont.Load("figlet/Small.flf");

            Figlet figlet = new(font);
            
            Console.WriteLine(figlet.ToAscii(str), ColorTranslator.FromHtml("#8AFFEF"));

        }


        public static string GetEnumDisplayName(Enum value)
        {
            return value.GetType().GetField(value.ToString())?
                       .GetCustomAttribute<DisplayAttribute>()?
                       .Name ?? value.ToString();
        }


        #endregion

    }//----------- class
} //------------namespace 
