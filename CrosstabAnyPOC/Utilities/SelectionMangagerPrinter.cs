using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace CrosstabAnyPOC.Utilities
{
    public static class SelectionMangagerPrinter
    {
        public static void PrintSelection(this SelectionManager selectionManager)
        {
            var x = Console.WindowWidth;

            Console.WriteLine(  );
            Console.WriteLine(new string('=', x));
            Console.WriteLine("Selection Pool:");
            Console.WriteLine($"{"ID",-8}  {"Name",-30} {"Dept",-5} {"Job",-5} {"Title",-25}");



            if (selectionManager.SelectionPool == null)
            {
                Console.WriteLine("  Empty");
                return;
            }


            Console.WriteLine($"Count: {selectionManager.SelectionPool.Count}");
            foreach (var emp in selectionManager.SelectionPool.OrderBy(n => n.EmployeeName))
            {
                Console.WriteLine($"{emp.EmployeeId, -8}  {emp.EmployeeName,-30} {emp.Department,-5} {emp.JobCode,-5} {emp.JobTitle, -25}");
            }
        }




        public static void PrintEmployees(this SelectionManager selectionManager)
        {
            var x = Console.WindowWidth;

            Console.WriteLine(  );
            Console.WriteLine(new string('=', x));
            Console.WriteLine("ALL EMPLOYEES:");
            Console.WriteLine($"{"ID",-8}  {"Name",-30} {"Dept",-5} {"Job",-5} {"Title",-25}");



            if (selectionManager.CurrentEmployees == null)
            {
                Console.WriteLine("  No Employees");
                return;
            }


            Console.WriteLine($"Count: {selectionManager.CurrentEmployees.Count}");
            int index = 1;
            foreach (var emp in selectionManager.CurrentEmployees.OrderBy(n => n.EmployeeName))
            {
                Console.WriteLine($"{index++, -4} {emp.EmployeeId,-8}  {emp.EmployeeName,-30} {emp.Department,-5} {emp.JobCode,-5} {emp.JobTitle,-25}");
            }
        }





        public static void PrintSpecialAssignments(this SelectionManager selectionManager)
        {
            var x = Console.WindowWidth;

            Console.WriteLine(  );
            Console.WriteLine(new string('=', x));
            Console.WriteLine("Special Assignments:");
            Console.WriteLine($"{"idx",-4}  {"EmpID",-8} {"Grp",-4}");


            if (selectionManager.SpecialAssignments == null)
            {
                Console.WriteLine(" No SpecAssg");
                return;
            }


            Console.WriteLine($"Count: {selectionManager.SpecialAssignments.Count}");
            int index = 1;
            foreach (var emp in selectionManager.SpecialAssignments.OrderBy(n => n.EmployeeId))
            {
                Console.WriteLine($"{index++,-4} {emp.EmployeeId,-8}  {emp.SpecialAssignmentGroup,-4}");
            }
        }





        public static void PrintNotEligible(this SelectionManager selectionManager)
        {
            var x = Console.WindowWidth;
            Console.WriteLine(  );
            Console.WriteLine(new string('=', x));
            Console.WriteLine("Not Eligibles:");
            Console.WriteLine($"{"idx",-4}  {"EmpID",-8}");


            if (selectionManager.NotEligibleEmployees  == null)
            {
                Console.WriteLine(" No non eligibles");
                return;
            }


            Console.WriteLine($"Count: {selectionManager.NotEligibleEmployees.Count}");
            int index = 1;
            foreach (var emp in selectionManager.NotEligibleEmployees.OrderBy(n => n))
            {
                Console.WriteLine($"{index++,-4} {emp,-8}");
            }
        }



        public static void PrintDrugTestSettings(this SelectionManager sm)
        {
            var x = Console.WindowWidth;
            Console.WriteLine();
            Console.WriteLine(new string('=', x));
            Console.WriteLine("Drug Test Settings:");


            Console.WriteLine($"{"Test Number:"     , 22} {sm.DrugTestSettings.TestNumber,-10}");
            Console.WriteLine($"{"operator:"        , 22} {sm.DrugTestSettings.TestOperatorName,-10}");
            Console.WriteLine($"{"Date:"            , 22} {sm.DrugTestSettings.RequestDateTime,-10}");
            Console.WriteLine($"{"Test Type:"       , 22} {sm.DrugTestSettings.TestType,-10}");
            Console.WriteLine($"{"Testing Group:"   , 22} {sm.DrugTestSettings.TestingGroup,-10}");
            Console.WriteLine($"{"Category:"        , 22} {sm.DrugTestSettings.TestCategory,-10}");
            Console.WriteLine($"{"Selection Method:", 22} {sm.DrugTestSettings.TestSubjectSelectionMethod.ToString(),-10}");

            Console.WriteLine($"{"% drug:"          , 22} {sm.DrugTestSettings.PercentageOfEmployeesToDrugTest,-10}");
            Console.WriteLine($"{"# drug test:"       , 22} {sm.DrugTestSettings.NumberOfEmployeesToDrugTest,-10}");
            Console.WriteLine($"{"% alcohol:"          , 22} {sm.DrugTestSettings.PercentageOfEmployeesToAlcoholTest,-10}");
            Console.WriteLine($"{"# alcohol:"       , 22} {sm.DrugTestSettings.NumberOfEmployeesToAlcoholTest,-10}");
                                                      
            Console.WriteLine($"{"Pool size:"     , 22} {sm.DrugTestSettings.EmployeePoolSize,-10}");
            Console.WriteLine($"{"Drug pattern:"    , 22} {sm.DrugTestSettings.DrugSelectionPattern,-10}");
            Console.WriteLine($"{"Alcohol pattern:" , 22} {sm.DrugTestSettings.AlcoholSelectionPattern,-10}");




        }

    }
}
