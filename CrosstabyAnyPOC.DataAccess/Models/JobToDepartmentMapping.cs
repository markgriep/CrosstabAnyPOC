//using System;

//namespace CrosstabAnyPOC.DataAccess.Models
//{
//    //#####################################################

//    public class JobToDepartmentMapping
//    {
//        public int ID { get; set; }              // unique id for the mapping
//        public int CostcenterId { get; set; }    // department id (cost center)
//        public string JobcodeId { get; set; }    // job code (alphanumeric)

//        public string TestingGroup { get; set; } // testing group (string)
//        public bool IsActive { get; set; }       // whether the mapping is active

//        public DateTime EffectiveDate { get; set; } // effective date of the mapping


//    }
//}




//        //public jobtodepartmentmapping(int id, int costcenterid, string jobcodeid,
//        //                              string testinggroup, datetime effectivedate, bool isactive)
//        //{
//        //    id = id;
//        //    costcenterid = costcenterid;
//        //    jobcodeid = jobcodeid;
//        //    testinggroup = testinggroup;
//        //    effectivedate = effectivedate;
//        //    isactive = isactive;
//        //}

//        //// method to create a list of mock jobtodepartmentmappings
//        //public static list<jobtodepartmentmapping> getmockmappings()
//        //{

//        //    return new list<jobtodepartmentmapping>
//        //    {
//        //        //                        id    dept       jobcode    testinggroup     effectivedate                isactive

//        //        new jobtodepartmentmapping(1,   990,       "001",      "t",    new datetime(2023, 1, 1),      true),
//        //        new jobtodepartmentmapping(2,   990,       "003",      "t",    new datetime(2023, 2, 1),      true),
//        //        new jobtodepartmentmapping(3,   990,       "009",      "t",    new datetime(2023, 3, 1),      false), // ffffffffffffff
//        //        new jobtodepartmentmapping(4,   990,       "002",      "t",    new datetime(2023, 4, 1),      true),

//        //        new jobtodepartmentmapping(5,   119,       "002",      "n",    new datetime(2023, 5, 1),      true),
//        //        new jobtodepartmentmapping(6,   119,       "003",      "n",    new datetime(2023, 6, 1),      true),
//        //        new jobtodepartmentmapping(7,   119,       "009",      "n",    new datetime(2023, 7, 1),      false), // ffffffffffffff
//        //        new jobtodepartmentmapping(8,   119,       "005",      "n",    new datetime(2023, 8, 1),      true),
//        //        new jobtodepartmentmapping(9,   119,       "006",      "n",    new datetime(2023, 9, 1),      true),

//        //        new jobtodepartmentmapping(10,  500,       "022",      "d",    new datetime(2023, 10, 1),     true),
//        //        new jobtodepartmentmapping(12,  500,       "029",      "d",    new datetime(2023, 10, 1),     false), // ffffffffffffff
//        //        new jobtodepartmentmapping(13,  500,       "028",      "d",    new datetime(2023, 10, 1),     false), // ffffffffffffff
//        //        new jobtodepartmentmapping(14,  500,       "023",      "d",    new datetime(2023, 10, 1),     true),
//        //        new jobtodepartmentmapping(15,  500,       "021",      "d",    new datetime(2023, 10, 1),     true),


//        //       new jobtodepartmentmapping(10,  404,       "022",      "d",    new datetime(2023, 10, 1),     true),
//        //       new jobtodepartmentmapping(12,  404,       "029",      "d",    new datetime(2023, 10, 1),     false), // ffffffffffffff
//        //       new jobtodepartmentmapping(13,  404,       "028",      "d",    new datetime(2023, 10, 1),     false), // ffffffffffffff
//        //       new jobtodepartmentmapping(14,  404,       "023",      "d",    new datetime(2023, 10, 1),     true),
//        //       new jobtodepartmentmapping(15,  404,       "021",      "d",    new datetime(2023, 10, 1),     true),


//        //       new jobtodepartmentmapping(10,  789,       "022",      "d",    new datetime(2023, 10, 1),     true),
//        //       new jobtodepartmentmapping(12,  789,       "029",      "d",    new datetime(2023, 10, 1),     false), // ffffffffffffff
//        //       new jobtodepartmentmapping(13,  789,       "028",      "d",    new datetime(2023, 10, 1),     false), // ffffffffffffff
//        //       new jobtodepartmentmapping(14,  789,       "023",      "d",    new datetime(2023, 10, 1),     true),
//        //       new jobtodepartmentmapping(15,  789,       "021",      "d",    new datetime(2023, 10, 1),     true),



//        //    };




//        //}



