﻿using Colorful;
using CrosstabAnyPOC.DataAccess.Models;
using CrosstabAnyPOC.Utilities;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;

using Console = Colorful.Console;

namespace CrosstabAnyPOC
{
    internal class Program
    {
        static void Main()
        {
            #region UI Setup  ---------------------------------------------------------------------------------------------------------------------
            Console.SetWindowSize(180, 25);
            Console.SetBufferSize(180, 1255);           // Match buffer to avoid scroll bars
            #endregion



            RunSelectionManager();



            #region Finalize  --------------------------------------------------------------------------------------------------------------------------

            Console.WriteLine("### FINISHED...  press any key to continue.");
            Console.ReadKey();

            #endregion

        }






        /// <summary>
        /// This method takes advantage of the SelectionManager class to run the selection process
        /// </summary>
        private static void RunSelectionManager()
        {
            // Get a MOCK list of employees
            List<WorkdayEmployee> _employees = MockEmployeeHelper.GetMockEmployees(1000);


            // Get a MOCK list of mappings
            List<JobCodeToDepartmentMapping> _mappings = MockJobToDepartment.GetStaticMappings();


            // Instantiate a new settings object with the setting given below it
            DrugTestSettings _settings = new DrugTestSettings();

            _settings.TestNumber = 2468;
            _settings.TestOperatorName = "Mark G";
            _settings.RequestDateTime = System.DateTime.Now;
            _settings.TestType = TestType.Drug;
            _settings.TestingGroup = TestingGroup.N;

            _settings.TestCategory = TestCategory.Random;
            _settings.TestSubjectSelectionMethod = TestSubjectSelectionMethod.Automatic;
            _settings.PercentageOfEmployeesToDrugTest = 0.55M;
            _settings.PercentageOfEmployeesToAlcoholTest = 0.25M;



            // create a MOCK  (BOTH Include/exclude objects)
            var x = new MockIncludeExclude();
            var y = x.NotEligibleList;
            var z = x.SpecialAssignmentsList;



            SelectionManager sm = new SelectionManager(_settings);   // Create a new SelectionManager object


            // sm.PrintSelectionPool();                              // should be empty


            sm.PopulateSelectionPool(_employees, _mappings);         // OPT 1  Populate the selection pool
            //sm.PopulateSelectionPool(_employees, 600);             // OPT 2  Populate the selection pool with a hard number

            //sm.PrintEmployees();                                   // should show ALL employees that were just passed in

  //          sm.PrintSelectionPool();

            x.NotEligibleList.Add(sm.SelectionPool[0].EmployeeId);   // Add the first employee to the not eligible list

            for (int i = 2; i < 12; i++)
            {
                x.NotEligibleList.Add(sm.SelectionPool[1].EmployeeId);      // Add the first employee to the not eligible list
            }




            sm.RemoveNotEligibleEmployeesFromPool(x.NotEligibleList);  // Remove not eligible employees from the selection pool
//            sm.PrintNotEligible();
//            sm.PrintSelectionPool();

            sm.AddSpecialAssignmentEmployeesToPool(x.SpecialAssignmentsList);  // Add special assignments to the selection pool
  //          sm.PrintSpecialAssignments();


            sm.PrintSelectionPool();


            sm.PopulateSettings();                                      // Populate the settings object
            sm.PrintDrugTestSettings();


            sm.PopulateSelectedForTesting();                            // Populate the selected for testing list
            sm.PrintSelectedForTesting();

        }





        ///// <summary>
        ///// This LONG method runs the selection process explicityly, one step at a time
        ///// </summary>
        //private static void RunStepByStepA()
        //{
        //    #region Variables  --------------------------------------------------------------------------------------------------------------------------

        //    var _mappings = MockJobToDepartment.GetStaticMappings();                    //.GetMockMappings();
        //    var _employees = MockEmployeeHelper.GetMockEmployees(50);                   // Generate random list of N employees

        //    var _settings = new DrugTestSettings                                        // Configure some settings for the test
        //    {
        //        TestNumber = 1,
        //        TestOperatorName = "Mark G",
        //        RequestDateTime = DateTime.Now,

        //        TestType = TestType.Drug,
        //        TestingGroup = TestingGroup.N,
        //        TestCategory = TestCategory.Random,
        //        TestSubjectSelectionMethod = TestSubjectSelectionMethod.Automatic,

        //        PercentageOfEmployeesToDrugTest = 0.25M,                                 // DRUG test X percent (PER YEAR)

        //        PercentageOfEmployeesToAlcoholTest = 0.1M,                              // ALCOHOL test x percent  (PER YEAR)

        //        NumberOfEmployeesToDrugTest = 0,                                        // OR - # employees
        //    };

        //    #endregion


        //    #region ACTIONS     --------------------------------------------------------------------------------------------------------------------------

        //    // STEP 1, __GET POOL__

        //    // In: _mappings list, _employees list , _settings object
        //    // Create a ___POOL___
        //    // Simple Match employees with the active mappings
        //    var SelectionPool = _employees.Where(emp =>
        //        _mappings.Any(map =>
        //            map.IsActive &&                                         // ONLY match active mappings                           -AND-
        //            map.CostCenterId.ToString() == emp.Department.ToString() &&                 // CostCenter (departments) match each other            -AND-
        //            map.TestingGroup == TestingGroup.T.ToString() &&        // testing group matches one of the enums.  (n, t, d)   -AND-
        //            map.JobCodeId == emp.JobCode &&                         // Jobcodes match each other                            -AND-
        //            true)).Select(ee => new { EmployeeID = ee.EmployeeId })                                                  // always true place holder so I can insert others above
        //            .ToList();


        //    System.Console.WriteLine(SelectionPool.Count);
        //    var threePeopleToDelete = SelectionPool.Take(3).Select(emp => emp).ToList();  // get the first 3 from the pool





        //    SelectionPool = SelectionPool.Where(emp => !threePeopleToDelete.Contains(emp)).ToList();
        //    System.Console.WriteLine(SelectionPool.Count);






        //    // create a list of 3 employeeIDs
        //    var newIds = Enumerable.Range(990001, 100).Select(id => new { EmployeeID = id }).ToList();
        //    SelectionPool.AddRange(newIds);
        //    System.Console.WriteLine(SelectionPool.Count);






        //    // get total in the pool
        //    //int  totalInPool = SelectionPool.Count;
        //    //TODO MAKE THIS R/O in the source.  Shouldn't be directly settable
        //    _settings.EmployeePoolSize = SelectionPool.Count;


        //    if (_settings.TestSubjectSelectionMethod == TestSubjectSelectionMethod.Automatic)                    // if Automatic,
        //    {
        //        _settings.NumberOfEmployeesToDrugTest = (int)Math.Ceiling(_settings.PercentageOfEmployeesToDrugTest * _settings.EmployeePoolSize);  // calculate the number of employees to test
        //    }
        //    // otherwise number of employees is already set


        //    // Based on pool zize, make a call to get a random hashset
        //    HashSet<int> randomNumbers = SelectionManager.GetRandomHashset((int)_settings.NumberOfEmployeesToDrugTest, _settings.EmployeePoolSize);


        //    // TODO This should be done in the SelectionManager not externally here.
        //    _settings.DrugSelectionPattern = string.Join(",", randomNumbers);               // store the hashset as comma separated string



        //    // STEP 2, __GET FROM POOL__   (SELECT Random EMPLOYEES)

        //    // create a linq query that selects the employees from the pool that match the random numbers
        //    var selectedEmployees = SelectionPool.Where((emp, index) => randomNumbers.Contains(index)).ToList();

        //    #endregion


        //    #region PRINT UI interaction  -------------------------------------------------------------------------------------------------------------------

        //    Printing.BigPrint("Drug Test Selection"); // Print the title

        //    Printing.BigPrint("The  POOL");
        //    Printing.BigPrint($"{Printing.GetEnumDisplayName(_settings.TestingGroup)}");
        //    Printing.BigPrint($"{SelectionPool.Count}");


        //    // Print matched employees in the ___POOL___
        //    Console.WriteLine("Matched Employees:");
        //    var n = 0;
        //    foreach (var emp in SelectionPool)
        //    {
        //        Console.WriteLine($"{n++,-4} {emp.EmployeeID,-12} ");    // Print employee details with leading zeros intact
        //        //Console.WriteLine($"{n++, -4} {emp.EmployeeID,-12} {emp.,-25} Dept: {emp.DepartmentID,-5} JobCode: {emp.JobCode,-5}");    // Print employee details with leading zeros intact
        //    }







        //    Printing.BigPrint("");
        //    Printing.BigPrint("SELECTED From the pool");
        //    Printing.BigPrint($"{selectedEmployees.Count}");

        //    // now loop through and print the selected employees
        //    foreach (var emp in selectedEmployees)
        //    {
        //        Console.WriteLine($"{emp.EmployeeID}");    // Print employee details with leading zeros intact
        //                                                   //   Console.WriteLine($"{emp.Name,-25} Dept: {emp.DepartmentID,-5} JobCode: {emp.JobCode,-5}");    // Print employee details with leading zeros intact
        //    }





        //    // print all the _settings

        //    Printing.BigPrint("");
        //    Printing.BigPrint("SETTINGS");
        //    int labelWidth = 35; // Set to ensure all labels align

        //    Console.WriteLine($"{"Test Number:".PadLeft(labelWidth)} {_settings.TestNumber}");                      // Right justify label, left justify value
        //    Console.WriteLine($"{"Test Operator:".PadLeft(labelWidth)} {_settings.TestOperatorName}");              // Right justify label, left justify value
        //    Console.WriteLine($"{"Request Date:".PadLeft(labelWidth)} {_settings.RequestDateTime}");                // Right justify label, left justify value

        //    Console.WriteLine($"{"Test Type:".PadLeft(labelWidth)} {_settings.TestType}");                          // Right justify label, left justify value
        //    Console.WriteLine($"{"Group:".PadLeft(labelWidth)} {_settings.TestingGroup}");                          // Right justify label, left justify value
        //    Console.WriteLine($"{"Category:".PadLeft(labelWidth)} {_settings.TestCategory}");                       // Right justify label, left justify value
        //    Console.WriteLine($"{"Selection Method:".PadLeft(labelWidth)} {_settings.TestSubjectSelectionMethod}"); // Right justify label, left justify value


        //    Console.WriteLine($"{"Pool size:".PadLeft(labelWidth)} {_settings.EmployeePoolSize}"); // Right justify label, left justify value
        //    Console.WriteLine($"{"Percentage of Employees to Test:".PadLeft(labelWidth)} {_settings.PercentageOfEmployeesToDrugTest}"); // Right justify label, left justify value
        //    Console.WriteLine($"{"Number of Employees to Test:".PadLeft(labelWidth)} {_settings.NumberOfEmployeesToDrugTest}"); // Right justify label, left justify value
        //    Console.WriteLine($"{"Random Numbers:".PadLeft(labelWidth)} {_settings.DrugSelectionPattern}\n");




        //    // _settings.NumberOfEmployeesToTest = (int)(_settings.PercentageOfEmployeesToTest * totalInPool);

        //    Console.WriteLine($"\nPool  {_settings.EmployeePoolSize}");
        //    Console.WriteLine($"   X  {_settings.PercentageOfEmployeesToDrugTest}");


        //    Console.WriteLine($"   =  {_settings.EmployeePoolSize * _settings.PercentageOfEmployeesToDrugTest:0.00}");

        //    #endregion


        //    #region Junk/old code   --------------------------------------------------------------------------------------------------------------------------


        //    //BigPrint("ALL EMPLOYEES");

        //    //foreach (var employee in _employees)                                             // Loop through each employee
        //    //{
        //    //    Console.WriteLine($"{employee.Name,-25} Dept: {employee.DepartmentID,-5} JobCode: {employee.JobCode,-5}");    // Print employee details with leading zeros intact
        //    //}








        //    //   BigPrint("random name");

        //    //   // Invoke to generate a single random full name
        //    //   string singleName = NameUtility.GenerateRandomFullName();
        //    //  // Console.WriteLine($"Generated Single Name: {singleName}");


        //    //   BigPrint("Some names");


        //    //   // Invoke to generate a list of unique full names
        //    //   int numberOfNames = 100; // specify the number of unique names you want
        //    //   List<string> nameList = NameUtility.GenerateUniqueFullNames(numberOfNames);
        //    // //  Console.WriteLine("Generated List of Names:");
        //    //   foreach (var name in nameList)
        //    //   {
        //    ////       Console.WriteLine(name);
        //    //   }




        //    //// Sanity checks
        //    //Utility.PrintMapping(_mappings);
        //    //Utility.PrintEmployees(_employees);

        //    #endregion

        //}


















    }
}
