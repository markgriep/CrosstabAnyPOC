using CrosstabAnyPOC.DataAccess.Models;
using CrosstabyAnyPOC.DataAccess.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosstabAnyPOC
{
    public static class Utility
    {



        public static void PrintEmployees(List<WorkdayEmployee> employees)
        {
            foreach (var employee in employees)
            {
                //Console.WriteLine($"Name: {employee.Name}, Dept: {employee.DepartmentID}, JCod: {employee.JobCode}");
                Console.WriteLine($" JCod: {employee.JobCode}");
            }
        }


        public static void PrintMapping(List<JobCodeToDepartmentMapping> mappings)
        {
            foreach (var mapping in mappings)
            {
                Console.WriteLine($"ID: {mapping.Id}, CostCenter: {mapping.CostCenterId}, JobCode: {mapping.JobCodeId}, " +
                                  $"TestingGroup: {mapping.TestingGroup}, EffectiveDate: {mapping.EffectiveDate.ToShortDateString()}, IsActive: {mapping.IsActive}");
            }
        }

    }
}
