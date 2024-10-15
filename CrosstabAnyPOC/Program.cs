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

            var _mappings = JobToDepartmentMapping.GetMockMappings();                   // Generate random mappings
            var _employees = WorkDayEmployee.GetMockEmployees(700);                     // Generate random list of N employees


         
            var _settings = new DrugTestSettings                                        // Configure some settings for the test                                             
            {
                TestNumber                  = 1,
                TestOperatorName            = "Mark G",
                RequestDateTime             = DateTime.Now,

                TestType                    = TestType.Both,
                TestingGroup                = TestingGroup.N,
                TestCategory                = TestCategory.Random,
                TestSubjectSelectionMethod  = TestSubjectSelectionMethod.Automatic,
                
                PercentageOfEmployeesToTest = 0.1M,                                     // X percent 
                NumberOfEmployeesToTest     = 0,                                            // 0 employees
            };


            #endregion


            #region ACTION

            
            
            
            
            // STEP 1, __GET POOL__

            // In: _mappings list, _employees list , _settings object
            // Create a ___POOL___
            // Simple Match employees with the active mappings
            var SelectionPool = _employees.Where(emp =>
                _mappings.Any(map =>
                    map.IsActive &&                                         // ONLY match active mappings                           -AND-
                    map.CostCenterID == emp.DepartmentID &&                 // CostCenter (departments) match each other            -AND-
                    map.TestingGroup == TestingGroup.T.ToString() &&        // testing group matches one of the enums.  (n, t, d)   -AND-
                    map.JobCodeID == emp.JobCode &&                         // Jobcodes match each other                            -AND-
                    true))                                                  // always true place holder so I can insert others above
                    .ToList();
            // If I wanted to pull this linkq query out what would I need to do?

            System.Console.WriteLine(SelectionPool.Count);

            var threePeopleToDelete = SelectionPool.Take(3).Select(emp => emp.EmployeeId).ToList();  // get the first 3 names from the pool





            SelectionPool = SelectionPool.Where(emp => !threePeopleToDelete.Contains(emp.EmployeeId)).ToList();
            System.Console.WriteLine(SelectionPool.Count);

            // delete people  Just simply delete the people that are in the Exclude table








            // Insert people that both match the employee ID and the "RDT Group"

            System.Console.WriteLine("insert people");














            // get total in the pool
            //int  totalInPool = SelectionPool.Count;
            _settings.EmployeePoolSize = SelectionPool.Count;

           
            if (_settings.TestSubjectSelectionMethod == TestSubjectSelectionMethod.Automatic)                    // if Automatic, 
            {
                _settings.NumberOfEmployeesToTest = (int)Math.Ceiling(_settings.PercentageOfEmployeesToTest * _settings.EmployeePoolSize);  // calculate the number of employees to test
            }
                                                                                                                 // otherwise number of employees is already set


            // Based on pool zize, make a call to get a random hashset            
            HashSet<int> randomNumbers =  SelectionManager.GetRandomHashset((int)_settings.NumberOfEmployeesToTest, _settings.EmployeePoolSize);

        
            _settings.SelectionPattern = string.Join(",", randomNumbers);               // store the hashset as comma separated string



            // STEP 2, __GET FROM POOL__   (SELECT Random EMPLOYEES)

            // create a linq query that selects the employees from the pool that match the random numbers
            var selectedEmployees = SelectionPool.Where((emp, index) => randomNumbers.Contains(index)).ToList();



















            #endregion


            #region PRINT UI interaction






            BigPrint("The  POOL"); 
            BigPrint($"{GetEnumDisplayName(_settings.TestingGroup)}");
            BigPrint($"{SelectionPool.Count}");

            // Print matched employees in the ___POOL___
            Console.WriteLine("Matched Employees:");
            var n = 0;
            foreach (var emp in SelectionPool)
            {      
               
               Console.WriteLine($"{n++, -4} {emp.EmployeeId,-12} {emp.Name,-25} Dept: {emp.DepartmentID,-5} JobCode: {emp.JobCode,-5}");    // Print employee details with leading zeros intact
            }





            
            BigPrint("");
            BigPrint("SELECTED From the pool");
            BigPrint($"{selectedEmployees.Count}");

            // now loop through and print the selected employees
            foreach (var emp in selectedEmployees)
            {
                Console.WriteLine($"{emp.Name,-25} Dept: {emp.DepartmentID,-5} JobCode: {emp.JobCode,-5}");    // Print employee details with leading zeros intact
            }





            // print all the _settings

            BigPrint("");
            BigPrint("SETTINGS");
            int labelWidth = 35; // Set to ensure all labels align

            Console.WriteLine($"{"Test Number:".PadLeft(labelWidth)} {_settings.TestNumber}");                      // Right justify label, left justify value
            Console.WriteLine($"{"Test Operator:".PadLeft(labelWidth)} {_settings.TestOperatorName}");              // Right justify label, left justify value
            Console.WriteLine($"{"Request Date:".PadLeft(labelWidth)} {_settings.RequestDateTime}");                // Right justify label, left justify value
            
            Console.WriteLine($"{"Test Type:".PadLeft(labelWidth)} {_settings.TestType}");                          // Right justify label, left justify value
            Console.WriteLine($"{"Group:".PadLeft(labelWidth)} {_settings.TestingGroup}");                          // Right justify label, left justify value
            Console.WriteLine($"{"Category:".PadLeft(labelWidth)} {_settings.TestCategory}");                       // Right justify label, left justify value
            Console.WriteLine($"{"Selection Method:".PadLeft(labelWidth)} {_settings.TestSubjectSelectionMethod}"); // Right justify label, left justify value
            

            Console.WriteLine($"{"Pool size:".PadLeft(labelWidth)} {_settings.EmployeePoolSize}"); // Right justify label, left justify value
            Console.WriteLine($"{"Percentage of Employees to Test:".PadLeft(labelWidth)} {_settings.PercentageOfEmployeesToTest}"); // Right justify label, left justify value
            Console.WriteLine($"{"Number of Employees to Test:".PadLeft(labelWidth)} {_settings.NumberOfEmployeesToTest}"); // Right justify label, left justify value
            Console.WriteLine($"{"Random Numbers:".PadLeft(labelWidth)} {_settings.SelectionPattern}\n");

           


            // _settings.NumberOfEmployeesToTest = (int)(_settings.PercentageOfEmployeesToTest * totalInPool);

            Console.WriteLine($"\nPool  {_settings.EmployeePoolSize}");
            Console.WriteLine($"   X  {_settings.PercentageOfEmployeesToTest}");


            Console.WriteLine($"   =  {_settings.EmployeePoolSize * _settings.PercentageOfEmployeesToTest:0.00}");






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


            Console.ReadKey();

            #endregion




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
