namespace CrosstabAnyPOC.Models
{
    //#########################################################################
    public class WorkDayEmployee
    {
        public int EmployeeId { get; set; }      // EmployeeID (INTEGER)
        public string Name { get; set; }            // Employee's Name
        public int DepartmentID { get; set; }       // Department ID (integer)
        public string JobCode { get; set; }         // Job Code (alphanumeric)

        public WorkDayEmployee(string name, int departmentID, string jobCode, int employeeId)
        {
            Name = name;
            DepartmentID = departmentID;
            JobCode = jobCode;
            EmployeeId = employeeId;
        }



        // HELPERS


        // Method to create a list of mock employees

        public static List<WorkDayEmployee> GetMockEmployees()
        {
            return new List<WorkDayEmployee>
            {
                //                    Name          DeptID       JobCode
                new WorkDayEmployee("Alice",        990,           "001",  891234),
                new WorkDayEmployee("X-Bob",        990,           "009",  890567),
                new WorkDayEmployee("Charlie",      990,           "003",  890345),
                                                                           
                new WorkDayEmployee("Diana",        119,           "002",  892345),
                new WorkDayEmployee("Edward",       119,           "003",  890211),
                new WorkDayEmployee("X-Fiona",      119,           "004",  890156),
                new WorkDayEmployee("X-George",     119,           "009",  890567),
                                                                           
                new WorkDayEmployee("Hannah",       111,           "005",  890401),
                new WorkDayEmployee("Ian",          111,           "005",  890222),
                new WorkDayEmployee("Jonah",        111,           "005",  890134),
                new WorkDayEmployee("Kevin",        111,           "005",  890987),
                                                                           
                new WorkDayEmployee("Lan",          005,           "022",  890250),
                new WorkDayEmployee("X-Man",        005,           "029",  890123),
                new WorkDayEmployee("X-Nud",        005,           "028",  890275),

                
                new WorkDayEmployee("Odd",          005,           "023",  892001),
            };
        }
















        //TODO
        //    See if the code will generate employee ids

            //https://chatgpt.com/c/67055bf3-16b4-800e-9ce8-c449a6df4506



        public static List<WorkDayEmployee> GetMockEmployees(int numberOfEmployees)
        {
            List<WorkDayEmployee> employeeList = new List<WorkDayEmployee>();

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
                employeeList.Add(new WorkDayEmployee(name, departmentID, jobCode, employeeID));
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
                    < 0.50 => random.Next(2000, 2551),      // 50% chance for range 2000-2250 (most likely)
                    < 0.75 => random.Next(2551, 2700),      // 25% chance for range 2251-2399
                    < 0.875 => random.Next(2701, 2950),     // 12.5% chance for range 2401-2549
                    < 0.955 => random.Next(2951, 3700),     // 8% chance for range 2551-2699
                    _ => random.Next(3701, 5999)            // 4.5% chance for range 2701-2998 (least likely)
                };

                int employeeID = 890000 + randomValue;     // Generate the final employee ID (e.g., 892345)
                employeeIDs.Add(employeeID);               // Add to HashSet for uniqueness
            }

            return employeeIDs;
        }


    }
} // End of namespace CrosstabAnyPOC
