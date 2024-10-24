using CrosstabAnyPOC.DataAccess.Models;
using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosstabAnyPOC.Utilities
{
    public static class MockEmployeeHelper
    {


        /// <summary>
        /// hacky hard coded employee list
        /// </summary>
        /// <returns></returns>
        public static List<WorkdayEmployee> GetStaticEmployees()
        {
            return new List<WorkdayEmployee> {
            new WorkdayEmployee { EmployeeName = "Alan-Transit",   JobCode = "001", Department = "990", EmployeeId = 890567 }, // SELECTED
            new WorkdayEmployee { EmployeeName = "Bob-Transit",    JobCode = "002", Department = "990", EmployeeId = 897569 }, // SELECTED
            new WorkdayEmployee { EmployeeName = "Charlie-Transit" ,JobCode = "099", Department = "990", EmployeeId = 890345 },

            new WorkdayEmployee { EmployeeName = "Diana-Non",  JobCode = "002", Department = "119", EmployeeId = 892645 }, // SELECTED
            new WorkdayEmployee { EmployeeName = "Edward-Non", JobCode = "003", Department = "119", EmployeeId = 890211 }, // SELECTED
            new WorkdayEmployee { EmployeeName = "Fiona-Non",  JobCode = "004", Department = "119", EmployeeId = 890756 }, // SELECTED
            new WorkdayEmployee { EmployeeName = "George-Non",  JobCode = "019", Department = "119", EmployeeId = 890867 },

            new WorkdayEmployee { EmployeeName = "Hannah-Other", JobCode = "554", Department = "404", EmployeeId = 890401 }, // SELECTED
            new WorkdayEmployee { EmployeeName = "Ian-Other",     JobCode = "005", Department = "404", EmployeeId = 890222 },
            new WorkdayEmployee { EmployeeName = "Jonah-Other",  JobCode = "552", Department = "404", EmployeeId = 890134 }, // SELECTED
            new WorkdayEmployee { EmployeeName = "Kevin-Other",   JobCode = "003", Department = "404", EmployeeId = 890987 },

            new WorkdayEmployee { EmployeeName = "Lois-X",    JobCode = "022", Department = "005", EmployeeId = 890050 },
            };
        }






        /// <summary>
        /// dynamic, somewhat realistic list of mock employees
        /// </summary>
        /// <param name="numberOfEmployees"></param>
        /// <returns></returns>
        public static List<WorkdayEmployee> GetMockEmployees(int numberOfEmployees)
        {
            List<WorkdayEmployee> employeeList = new List<WorkdayEmployee>();

            // Generate unique employee IDs before the for loop
            HashSet<int> employeeIDs = GenerateUniqueEmployeeIDs(numberOfEmployees);
            int[] employeeIDsArray = employeeIDs.ToArray(); // Convert HashSet to array for indexed access



            for (int i = 0; i < numberOfEmployees; i++)
            {
                string name = NameUtility.GenerateRandomFullName();                     // Generate a unique full name
                int departmentID = GetRandomDepartment();                               // Get a random department ID

                // Alternate between random job code generators for variety
                string jobCode = (i % 2 == 0) ? GetRandomJobCode() : GetRandomWeightedJobCode();


                // Assign employee ID and create new WorkDayEmployee
                int employeeID = employeeIDsArray[i];

                // Create a new WorkDayEmployee and add to the list
                //employeeList.Add(new WorkdayEmployee(name, departmentID, jobCode, employeeID));
                employeeList.Add(new WorkdayEmployee { 
                    Id = i + 1,
                    EmployeeName = name, 
                    EmployeeId = employeeID, 
                    Department = departmentID.ToString(), 
                    JobCode = jobCode 
                });
            }

            return employeeList;                                                        // Return the complete list of employees
        }


        private static int GetRandomDepartment()
        {
            List<int> departmentIDs = new List<int> { 990, 119, 111, 500, 404, 200, 222, 123, 456, 789 };
            Random random = new Random();
            return departmentIDs[random.Next(departmentIDs.Count)];
        }


        private static string GetRandomJobCode()
        {
            List<string> jobCodes = new List<string> { "001", "009", "003", "002", "004", "005", "022", "029", "028", "023" };
            Random random = new Random();
            return jobCodes[random.Next(jobCodes.Count)];
        }


        private static string GetRandomWeightedJobCode()
        {
            // Define job codes with associated weights
            var jobCodeWeights = new Dictionary<string, int>
            {
                { "001", 5 },                                           // Higher weight, more likely to be chosen
                { "009", 1 },                                           // Lower weight, less likely to be chosen
                { "003", 3 },                                           // Moderate weight
                { "002", 2 },                                           // Moderate weight
                { "004", 4 },                                           // Higher weight, more likely to be chosen
                { "005", 7 },                                           // Highest weight, most likely to be chosen
                { "022", 1 },                                           // Lower weight, less likely to be chosen
                { "029", 2 },                                           // Moderate weight
                { "028", 1 },                                           // Lower weight, less likely to be chosen
                { "023", 1 }                                            // Lower weight, less likely to be chosen
            };

            // Calculate the total weight
            int totalWeight = jobCodeWeights.Values.Sum();             // Sum of all weights

            // Generate a random number between 0 and the total weight
            Random random = new Random();                              // Create random number generator
            int randomNumber = random.Next(0, totalWeight);            // Generate random number

            // Select the job code based on cumulative weights
            int cumulativeWeight = 0;                                  // Start cumulative weight at 0
            foreach (var jobCode in jobCodeWeights)                    // Iterate over job codes
            {
                cumulativeWeight += jobCode.Value;                     // Add current job code's weight to cumulative weight
                if (randomNumber < cumulativeWeight)                   // If random number falls within this range
                {
                    return jobCode.Key;                                // Return the corresponding job code
                }
            }

            // Fallback in case something goes wrong (shouldn't happen with correct setup)
            return jobCodeWeights.Keys.First();                        // Return the first job code as fallback
        }


        public static HashSet<int> GenerateUniqueEmployeeIDs(int count)
        {
            HashSet<int> employeeIDs = new HashSet<int>();
            Random random = new Random();

            while (employeeIDs.Count < count)
            {
                // Generate random value based on weighted probability
                int randomValue = random.NextDouble() switch
                    {
                        < 0.10 => random.Next(3701, 9551),      // 10% chance for range 3701-9551
                        < 0.30 => random.Next(2501, 3700),      // 20% chance for range 2501-3700
                        _ => random.Next(2000, 2500)            // 70% chance for range 2000-2500
                    };

                int employeeID = 890000 + randomValue;     // Generate the final employee ID (e.g., 892345)
                employeeIDs.Add(employeeID);               // Add to HashSet for uniqueness
            }

            return employeeIDs;
        }


    }
}
