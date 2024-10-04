using CrosstabAnyPOC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosstabAnyPOC
{
    public static class Utility
    {



        public static void PrintEmployees(List<WorkDayEmployee> employees)
        {
            foreach (var employee in employees)
            {
                Console.WriteLine($"Name: {employee.Name}, Dept: {employee.DepartmentID}, JCod: {employee.JobCode}");
            }
        }


        public static void PrintMapping(List<JobToDepartmentMapping> mappings)
        {
            foreach (var mapping in mappings)
            {
                Console.WriteLine($"ID: {mapping.ID}, CostCenter: {mapping.CostCenterID}, JobCode: {mapping.JobCodeID}, " +
                                  $"TestingGroup: {mapping.TestingGroup}, EffectiveDate: {mapping.EffectiveDate.ToShortDateString()}, IsActive: {mapping.IsActive}");
            }
        }

    }
}
