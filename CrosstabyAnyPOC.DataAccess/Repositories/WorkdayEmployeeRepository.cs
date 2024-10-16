//using CrosstabAnyPOC.DataAccess.Data;
using CrosstabAnyPOC.DataAccess.Interfaces;
using CrosstabAnyPOC.DataAccess.Models;
//using RandomDrugTest.Core.Interfaces;

using System.Net.Http.Json;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using System.Diagnostics;

namespace CrosstabAnyPOC.DataAccess.Repositories
{
    public class WorkdayEmployeeRepository : IWorkdayEmployeeRepository
    {

        #region Variables
        private readonly IHttpClientManager _httpClientManager; // Injected HTTP client manager
        private readonly ISecretsManager _secretsManager;       // Injected secrets manager
        private readonly IProxyManager _proxyManager;           // Injected proxy manager
        private readonly RandomDrugTestContext _context;
        #endregion



        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        /// <param name="httpClientManager"></param>
        /// <param name="secretsManager"></param>
        /// <param name="proxyManager"></param>
        public WorkdayEmployeeRepository(
            IHttpClientManager httpClientManager, 
            ISecretsManager secretsManager, 
            IProxyManager proxyManager, 
            RandomDrugTestContext rdtContext)
        {
            _httpClientManager = httpClientManager;
            _secretsManager = secretsManager;
            _proxyManager = proxyManager;
            _context = rdtContext;
        }


       
        public async Task<IEnumerable<WorkdayEmployee>> GetWorkdayEmployeesFromApiAsync()
        {
            // Step 1: Retrieve credentials from the secrets manager (example using local config)
            string creds = _secretsManager.GetCredentialsFromLocalConfig("wdusername", "wdpassword");



            // Step 2: Call the API using the HTTP client manager, passing the credentials
            HttpResponseMessage response = await
                _httpClientManager.GetApiResponseAsync(
                    _secretsManager.GetSecretsFromLocalConfig("peopleuri"),
                    _secretsManager.GetCredentialsFromLocalConfig("wdusername", "wdpassword")
                    );

            // Step 3: Check if the response is successful and deserialize the result
            if (response.IsSuccessStatusCode)
            {
                var XmlResponse = await response.Content.ReadAsStringAsync();        
                return DeserializeWorkdayEmployees(XmlResponse);                            // Send back the result
            }

            return Enumerable.Empty<WorkdayEmployee>();                                     // Return an empty list if the API call fails
        }


        /// <summary>
        /// DESERIALIZE XML into Employees list. PRIVATE, only for in this class
        /// </summary>
        /// <param name="xmlContent"></param>
        /// <returns></returns>
        private IEnumerable<WorkdayEmployee> DeserializeWorkdayEmployees(string xmlContent)
        {
            var serializer = new XmlSerializer(typeof(WorkdayEmployeeReport));                  // set a seralizer

            try
            {
                using (var reader = new StringReader(xmlContent))                               // read the content into a string reader
                {
                    var result = serializer.Deserialize(reader) as WorkdayEmployeeReport;       // call the deseralizer as a "report"
                    return result?.Employees ?? new List<WorkdayEmployee>();                    // Split out the Employees part of the report and return it
                }
            }
            catch (InvalidOperationException ex)
            {

                throw;
            }
        }


        /// <summary>
        /// STORE
        /// </summary>
        /// <param name="employees"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task StoreEmployeesInDbAsync(IEnumerable<WorkdayEmployee> employees)
        {
            try
            {
                var w = await _context.WorkdayEmployees.CountAsync();

                await _context.WorkdayEmployees.ExecuteDeleteAsync();               // Clear out existing, just in case
                var x = await _context.WorkdayEmployees.CountAsync();
                await _context.AddRangeAsync(employees.ToList());                   // add new from what was passed in
                var y = await _context.WorkdayEmployees.CountAsync();
                _context.SaveChanges();                                             // save it.
                var z = await _context.WorkdayEmployees.CountAsync();

                Debug.WriteLine($"##### {w} - {x} - {y} - {z} #####");

            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            
        }


        /// <summary>
        /// CLEAR TABLE
        /// </summary>
        /// <returns></returns>
        public async Task ClearAllEmployeesAsync()
        {
            try
            {
                _context.WorkdayEmployees.RemoveRange(_context.WorkdayEmployees);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(  ex.Message);
            }

        }

    }
}
