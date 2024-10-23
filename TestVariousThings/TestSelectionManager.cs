using CrosstabAnyPOC;
using CrosstabAnyPOC.Utilities;
using CrosstabAnyPOC.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrosstabyAnyPOC.DataAccess.Models.DTOs;

namespace TestVariousThings
{
    public class TestSelectionManager
    {



        [Fact]
        public void TestHashSetCreatorRandomeness_ShouldBeEvenlyDistributed()
        {

            var hashset = SelectionManager.GetRandomHashset(20, 200);

            // get duplicate count
            int duplicatesCount = hashset
                  .GroupBy(x => x)
                  .Where(g => g.Count() > 1)
                  .Count();

            Assert.True(duplicatesCount == 0);

        }








        [Theory]
        [InlineData(TestingGroup.T  ,new string[] { "Alan-AT", "Bob-AT" })]
        [InlineData(TestingGroup.N  ,new string[] { "Diana-AN", "Fiona-AN", "Edward-AN" })]
        [InlineData(TestingGroup.O  ,new string[] { "Hannah-AO", "Jonah-AO" })]
        public void TestGettingStaticEmployeesAndJobDepartmentMapping_ShouldMatchExpectedNames(TestingGroup _testingGroup, string[] _expectedNames)
        {
            var _mappings = MockJobToDepartment.GetStaticMappings();                                // Get some known-value of mappings
            var _employees = MockEmployeeHelper.GetStaticEmployees();                               // Get some known-value of employees

            var _settings = new DrugTestSettings                                                    // setup some testing prameters
            {
                TestingGroup = _testingGroup,                                                       // Only need this one esetting 
            };

            var selectionManager = new SelectionManager(_settings);                                 // pass in settings
            selectionManager.PopulateSelectionPool(_employees, _mappings);                          // pass in employees and mappings

            var selectedEmployees = selectionManager.GetSelectionPool().Select(e => e.EmployeeName).OrderDescending();
           
            Assert.Equal(_expectedNames.OrderDescending(), selectedEmployees);
        }






        [Fact]
        public void AddSpecialAssignmentsToSelectionPool_OnlyMatchingGroupCodeAdded()
        {
            // Arrange
            var _mappings = MockJobToDepartment.GetStaticMappings();                               // Get some known-value of mappings
            var _employees = MockEmployeeHelper.GetStaticEmployees();                              // Get some known-value of employees

            List<WorkdayEmployee> _selectionPool = new List<WorkdayEmployee>();                    // Initialize the selection pool

            var _settings = new DrugTestSettings                                                   // setup some testing prameters
            {
                TestingGroup = TestingGroup.O,                                                     // Only need this one. Set to 'O' (DOT-Other)
            };

            var selectionManager = new SelectionManager(_settings);                                // pass in settings
            selectionManager.PopulateSelectionPool(_employees, _mappings);                         // pass in employees and mappings

            var specialAssignmentEmployees = GetSpecialAssignmentMockList();                       // Get mock SpecialAssignment list



            selectionManager.AddSpecialAssignmentsToSelectionPool(specialAssignmentEmployees);     // Call the method to test

                                                                                                   // Assert
            Assert.Contains(_selectionPool, emp => emp.EmployeeId == 003);                       // Ensure "003" was added
            Assert.DoesNotContain(_selectionPool, emp => emp.EmployeeId == 001);                 // Ensure "001" (GroupCode "T") was not added
            Assert.DoesNotContain(_selectionPool, emp => emp.EmployeeId == 002);                 // Ensure "002" (GroupCode "N") was not added

        }






        #region HELPER METHODS


        // Helper method to create mock SpecialAssignment list
        private List<SpecialAssignment> GetSpecialAssignmentMockList()
        {
            return new List<SpecialAssignment>
            {
                new SpecialAssignment { EmployeeId = 999097, SpecialAssignmentGroup = "T" },  // GroupCode "T"
                new SpecialAssignment { EmployeeId = 999098, SpecialAssignmentGroup = "N" },  // GroupCode "N"
                new SpecialAssignment { EmployeeId = 999099, SpecialAssignmentGroup = "O" },   // GroupCode "O"


                // these are duplicated, thus shouldn't be added
                new SpecialAssignment { EmployeeId = 999097, SpecialAssignmentGroup = "T" },  // GroupCode "T"
                new SpecialAssignment { EmployeeId = 999098, SpecialAssignmentGroup = "N" },  // GroupCode "N"
                new SpecialAssignment { EmployeeId = 999099, SpecialAssignmentGroup = "O" },   // GroupCode "O"

                new SpecialAssignment { EmployeeId = 999097, SpecialAssignmentGroup = "T" },  // GroupCode "T"
                new SpecialAssignment { EmployeeId = 999098, SpecialAssignmentGroup = "N" },  // GroupCode "N"
                new SpecialAssignment { EmployeeId = 999099, SpecialAssignmentGroup = "O" },   // GroupCode "O"
                
                new SpecialAssignment { EmployeeId = 999097, SpecialAssignmentGroup = "T" },  // GroupCode "T"
                new SpecialAssignment { EmployeeId = 999098, SpecialAssignmentGroup = "N" },  // GroupCode "N"
                new SpecialAssignment { EmployeeId = 999099, SpecialAssignmentGroup = "O" },   // GroupCode "O"
                
                new SpecialAssignment { EmployeeId = 999097, SpecialAssignmentGroup = "T" },  // GroupCode "T"
                new SpecialAssignment { EmployeeId = 999098, SpecialAssignmentGroup = "N" },  // GroupCode "N"
                new SpecialAssignment { EmployeeId = 999099, SpecialAssignmentGroup = "O" },   // GroupCode "O"
            };
        }

    


        #endregion


    }
}
