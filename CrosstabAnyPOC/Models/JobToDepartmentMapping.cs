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
                //                        ID    Dept       JobCode    TestingGroup     EffectiveDate                IsActive

                new JobToDepartmentMapping(1,   990,       "001",      "T",    new DateTime(2023, 1, 1),      true),
                new JobToDepartmentMapping(2,   990,       "003",      "T",    new DateTime(2023, 2, 1),      true),
                new JobToDepartmentMapping(3,   990,       "009",      "T",    new DateTime(2023, 3, 1),      false), // FFFFFFFFFFFFFF
                new JobToDepartmentMapping(4,   990,       "002",      "T",    new DateTime(2023, 4, 1),      true),

                new JobToDepartmentMapping(5,   119,       "002",      "N",    new DateTime(2023, 5, 1),      true),
                new JobToDepartmentMapping(6,   119,       "003",      "N",    new DateTime(2023, 6, 1),      true),
                new JobToDepartmentMapping(7,   119,       "009",      "N",    new DateTime(2023, 7, 1),      false), // FFFFFFFFFFFFFF
                new JobToDepartmentMapping(8,   119,       "005",      "N",    new DateTime(2023, 8, 1),      true),
                new JobToDepartmentMapping(9,   119,       "006",      "N",    new DateTime(2023, 9, 1),      true),

                new JobToDepartmentMapping(10,  005,       "022",      "D",    new DateTime(2023, 10, 1),     true),
                new JobToDepartmentMapping(12,  005,       "029",      "D",    new DateTime(2023, 10, 1),     false), // FFFFFFFFFFFFFF
                new JobToDepartmentMapping(13,  005,       "028",      "D",    new DateTime(2023, 10, 1),     false), // FFFFFFFFFFFFFF
                new JobToDepartmentMapping(14,  005,       "023",      "D",    new DateTime(2023, 10, 1),     true),
                new JobToDepartmentMapping(15,  005,       "021",      "D",    new DateTime(2023, 10, 1),     true),
            };




        }





    }












} // End of namespace CrosstabAnyPOC
