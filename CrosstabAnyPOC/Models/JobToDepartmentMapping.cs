namespace CrosstabAnyPOC.Models
{
    //#####################################################

    public class JobToDepartmentMapping
    {
        public int ID { get; set; }              // Unique ID for the mapping
        public int CostCenterID { get; set; }    // Department ID (Cost Center)
        public string JobCodeID { get; set; }    // Job Code (alphanumeric)
        public string TestingGroup { get; set; } // Testing Group (string)
        public DateTime EffectiveDate { get; set; } // Effective Date of the mapping
        public bool IsActive { get; set; }       // Whether the mapping is active

        public JobToDepartmentMapping(int id, int costCenterID, string jobCodeID,
                                      string testingGroup, DateTime effectiveDate, bool isActive)
        {
            ID = id;
            CostCenterID = costCenterID;
            JobCodeID = jobCodeID;
            TestingGroup = testingGroup;
            EffectiveDate = effectiveDate;
            IsActive = isActive;
        }

        // Method to create a list of mock JobToDepartmentMappings
        public static List<JobToDepartmentMapping> GetMockMappings()
        {
            return new List<JobToDepartmentMapping>
        {
            new JobToDepartmentMapping(1, 909, "BUS Driver", "Group A", new DateTime(2022, 1, 1), true),
            new JobToDepartmentMapping(2, 800, "Lineman", "Group B", new DateTime(2022, 2, 1), true),
            new JobToDepartmentMapping(3, 103, "FIN", "Group C", new DateTime(2022, 3, 1), false),
            new JobToDepartmentMapping(4, 104, "IT", "Group A", new DateTime(2022, 4, 1), false),
            new JobToDepartmentMapping(5, 105, "MKT", "Group D", new DateTime(2022, 5, 1), false),
            new JobToDepartmentMapping(6, 101, "DEV", "Group A", new DateTime(2022, 6, 1), false),
            new JobToDepartmentMapping(7, 102, "HR", "Group B", new DateTime(2022, 7, 1), false),
            new JobToDepartmentMapping(8, 103, "FIN", "Group C", new DateTime(2022, 8, 1), false),
            new JobToDepartmentMapping(9, 104, "IT", "Group A", new DateTime(2022, 9, 1), false),
            new JobToDepartmentMapping(10, 105, "MKT", "Group D", new DateTime(2022, 10, 1), false)
        };
        }


     


    }












} // End of namespace CrosstabAnyPOC
