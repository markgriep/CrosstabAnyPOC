using CrosstabAnyPOC.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosstabAnyPOC.Utilities
{
    public static class MockJobToDepartment
    {

        // Method to create a list of mock JobToDepartmentMappings
        public static List<JobCodeToDepartmentMapping> GetStaticMappings() => new List<JobCodeToDepartmentMapping>{
                new() {Id = 0,    JobCodeId = "001", CostCenterId = 990, TestingGroup = "T", EffectiveDate = new DateTime(2023,1,1),  IsActive = true },  // T
                new() { Id = 1,   JobCodeId = "002", CostCenterId = 990, TestingGroup = "T", EffectiveDate = new DateTime(2023,1,1),  IsActive = true },  // T
                new() { Id = 2,   JobCodeId = "003", CostCenterId = 990, TestingGroup = "T", EffectiveDate = new DateTime(2023,2,1),  IsActive = true },  // T
                new() { Id = 3,   JobCodeId = "423", CostCenterId = 990, TestingGroup = "T", EffectiveDate = new DateTime(2023,3,1),  IsActive = false }, // FFFFFFFFFFFFFF
                new() { Id = 4,   JobCodeId = "004", CostCenterId = 990, TestingGroup = "T", EffectiveDate = new DateTime(2023,4,1),  IsActive = true },  // T

                new() { Id = 5,   JobCodeId = "002", CostCenterId = 119, TestingGroup = "N", EffectiveDate = new DateTime(2023,5,1),  IsActive = true },  // N
                new() { Id = 6,   JobCodeId = "003", CostCenterId = 119, TestingGroup = "N", EffectiveDate = new DateTime(2023,6,1),  IsActive = true },  // N
                new() { Id = 7,   JobCodeId = "634", CostCenterId = 119, TestingGroup = "N", EffectiveDate = new DateTime(2023,7,1),  IsActive = false }, // FFFFFFFFFFFFFF
                new() { Id = 8,   JobCodeId = "004", CostCenterId = 119, TestingGroup = "N", EffectiveDate = new DateTime(2023,8,1),  IsActive = true },  // N
                new() { Id = 9,   JobCodeId = "009", CostCenterId = 119, TestingGroup = "N", EffectiveDate = new DateTime(2023,9,1),  IsActive = true },  // N
                
                new() { Id = 10,  JobCodeId = "901", CostCenterId = 500, TestingGroup = "D", EffectiveDate = new DateTime(2023,10,1), IsActive = true },  // D
                new() { Id = 12,  JobCodeId = "174", CostCenterId = 500, TestingGroup = "D", EffectiveDate = new DateTime(2023,10,1), IsActive = false }, // FFFFFFFFFFFFFF
                new() { Id = 13,  JobCodeId = "994", CostCenterId = 500, TestingGroup = "D", EffectiveDate = new DateTime(2023,10,1), IsActive = false }, // FFFFFFFFFFFFFF
                new() { Id = 14,  JobCodeId = "930", CostCenterId = 500, TestingGroup = "D", EffectiveDate = new DateTime(2023,10,1), IsActive = true },  // D
                new() { Id = 15,  JobCodeId = "377", CostCenterId = 500, TestingGroup = "D", EffectiveDate = new DateTime(2023,10,1), IsActive = true },  // D
                
                new() { Id = 10,  JobCodeId = "554", CostCenterId = 404, TestingGroup = "O", EffectiveDate = new DateTime(2023,10,1), IsActive = true },  // D
                new() { Id = 12,  JobCodeId = "559", CostCenterId = 404, TestingGroup = "O", EffectiveDate = new DateTime(2023,10,1), IsActive = false }, // FFFFFFFFFFFFFF
                new() { Id = 13,  JobCodeId = "558", CostCenterId = 404, TestingGroup = "O", EffectiveDate = new DateTime(2023,10,1), IsActive = false }, // FFFFFFFFFFFFFF
                new() { Id = 14,  JobCodeId = "553", CostCenterId = 404, TestingGroup = "O", EffectiveDate = new DateTime(2023,10,1), IsActive = true },  // D
                new() { Id = 15,  JobCodeId = "552", CostCenterId = 404, TestingGroup = "O", EffectiveDate = new DateTime(2023,10,1), IsActive = true },  // D

                new() { Id = 10,  JobCodeId = "035", CostCenterId = 111, TestingGroup = "D", EffectiveDate = new DateTime(2023,10,1), IsActive = true },  // D
                new() { Id = 12,  JobCodeId = "333", CostCenterId = 111, TestingGroup = "D", EffectiveDate = new DateTime(2023,10,1), IsActive = false }, // FFFFFFFFFFFFFF
                new() { Id = 13,  JobCodeId = "694", CostCenterId = 111, TestingGroup = "D", EffectiveDate = new DateTime(2023,10,1), IsActive = false }, // FFFFFFFFFFFFFF
                new() { Id = 14,  JobCodeId = "055", CostCenterId = 111, TestingGroup = "D", EffectiveDate = new DateTime(2023,10,1), IsActive = true },  // D
                new() { Id = 15,  JobCodeId = "001", CostCenterId = 111, TestingGroup = "D", EffectiveDate = new DateTime(2023,10,1), IsActive = true },  // D
                new() { Id = 16,  JobCodeId = "003", CostCenterId = 111, TestingGroup = "D", EffectiveDate = new DateTime(2023,1,1),  IsActive = true},
            };




    }
}
