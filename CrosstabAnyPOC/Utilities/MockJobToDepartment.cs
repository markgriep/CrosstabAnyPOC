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
        public static List<JobCodeToDepartmentMapping> GetMockMappings() => new List<JobCodeToDepartmentMapping>{
                new() { Id = 990, CostCenterId = 001, TestingGroup = "T", EffectiveDate = new DateTime(2023,1,1), IsActive = true},
                new() { Id = 1,   CostCenterId = 990, TestingGroup = "T", EffectiveDate = new DateTime(2023,1,1),  IsActive = true },  // T
                new() { Id = 2,   CostCenterId = 990, TestingGroup = "T", EffectiveDate = new DateTime(2023,2,1),  IsActive = true },  // T
                new() { Id = 3,   CostCenterId = 990, TestingGroup = "T", EffectiveDate = new DateTime(2023,3,1),  IsActive = false }, // FFFFFFFFFFFFFF
                new() { Id = 4,   CostCenterId = 990, TestingGroup = "T", EffectiveDate = new DateTime(2023,4,1),  IsActive = true },  // T
                new() { Id = 5,   CostCenterId = 119, TestingGroup = "N", EffectiveDate = new DateTime(2023,5,1),  IsActive = true },  // N
                new() { Id = 6,   CostCenterId = 119, TestingGroup = "N", EffectiveDate = new DateTime(2023,6,1),  IsActive = true },  // N
                new() { Id = 7,   CostCenterId = 119, TestingGroup = "N", EffectiveDate = new DateTime(2023,7,1),  IsActive = false }, // FFFFFFFFFFFFFF
                new() { Id = 8,   CostCenterId = 119, TestingGroup = "N", EffectiveDate = new DateTime(2023,8,1),  IsActive = true },  // N
                new() { Id = 9,   CostCenterId = 119, TestingGroup = "N", EffectiveDate = new DateTime(2023,9,1),  IsActive = true },  // N
                new() { Id = 10,  CostCenterId = 500, TestingGroup = "D", EffectiveDate = new DateTime(2023,10,1), IsActive = true },  // D
                new() { Id = 12,  CostCenterId = 500, TestingGroup = "D", EffectiveDate = new DateTime(2023,10,1), IsActive = false }, // FFFFFFFFFFFFFF
                new() { Id = 13,  CostCenterId = 500, TestingGroup = "D", EffectiveDate = new DateTime(2023,10,1), IsActive = false }, // FFFFFFFFFFFFFF
                new() { Id = 14,  CostCenterId = 500, TestingGroup = "D", EffectiveDate = new DateTime(2023,10,1), IsActive = true },  // D
                new() { Id = 15,  CostCenterId = 500, TestingGroup = "D", EffectiveDate = new DateTime(2023,10,1), IsActive = true },  // D
                new() { Id = 10,  CostCenterId = 404, TestingGroup = "D", EffectiveDate = new DateTime(2023,10,1), IsActive = true },  // D
                new() { Id = 12,  CostCenterId = 404, TestingGroup = "D", EffectiveDate = new DateTime(2023,10,1), IsActive = false }, // FFFFFFFFFFFFFF
                new() { Id = 13,  CostCenterId = 404, TestingGroup = "D", EffectiveDate = new DateTime(2023,10,1), IsActive = false }, // FFFFFFFFFFFFFF
                new() { Id = 14,  CostCenterId = 404, TestingGroup = "D", EffectiveDate = new DateTime(2023,10,1), IsActive = true },  // D
                new() { Id = 15,  CostCenterId = 404, TestingGroup = "D", EffectiveDate = new DateTime(2023,10,1), IsActive = true },  // D
                new() { Id = 10,  CostCenterId = 789, TestingGroup = "D", EffectiveDate = new DateTime(2023,10,1), IsActive = true },  // D
                new() { Id = 12,  CostCenterId = 789, TestingGroup = "D", EffectiveDate = new DateTime(2023,10,1), IsActive = false }, // FFFFFFFFFFFFFF
                new() { Id = 13,  CostCenterId = 789, TestingGroup = "D", EffectiveDate = new DateTime(2023,10,1), IsActive = false }, // FFFFFFFFFFFFFF
                new() { Id = 14,  CostCenterId = 789, TestingGroup = "D", EffectiveDate = new DateTime(2023,10,1), IsActive = true },  // D
                new() { Id = 15,  CostCenterId = 789, TestingGroup = "D", EffectiveDate = new DateTime(2023,10,1), IsActive = true },  // D

            };




    }
}
