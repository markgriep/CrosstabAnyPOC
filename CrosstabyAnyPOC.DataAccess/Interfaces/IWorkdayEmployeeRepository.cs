using RandomDrugTest.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomDrugTest.DataAccess.Interfaces
{
    public interface IWorkdayEmployeeRepository
    {
        Task<IEnumerable<WorkdayEmployee>> GetWorkdayEmployeesFromApiAsync();          // Get ALL employees from API
        

        Task StoreEmployeesInDbAsync(IEnumerable<WorkdayEmployee> employees);   // Store list of employees in tranisent table


        Task ClearAllEmployeesAsync();
    }
}
