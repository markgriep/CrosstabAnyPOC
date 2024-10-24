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

            Console.WriteLine(new string('=', x));
            Console.WriteLine("Selection Pool:");
            Console.WriteLine($"{"ID",-8}  {"Name",-30} {"Dept",-5} {"Job",-5} {"Title",-25}");



            if (selectionManager.GetSelectionPool() == null)
            {
                Console.WriteLine("  Empty");
                return;
            }


            Console.WriteLine($"Count: {selectionManager.GetSelectionPool().Count}");
            foreach (var emp in selectionManager.GetSelectionPool().OrderBy(n => n.EmployeeName))
            {
                Console.WriteLine($"{emp.EmployeeId, -8}  {emp.EmployeeName,-30} {emp.Department,-5} {emp.JobCode,-5} {emp.JobTitle, -25}");
            }
        }


    





    }
}
