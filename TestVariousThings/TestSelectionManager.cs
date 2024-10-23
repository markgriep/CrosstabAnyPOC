using CrosstabAnyPOC;
using CrosstabAnyPOC.Utilities;
using CrosstabAnyPOC.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

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
        public void AddSpecialAssignmentsToSelectionPoolSimple_OnlyMatchingIdIsAdded()
        {
            // Arrange
            var _mappings = MockJobToDepartment.GetStaticMappings();                               // Get some known-value of mappings
            var _employees = MockEmployeeHelper.GetStaticEmployees();                              // Get some known-value of employees

            List<WorkdayEmployee> _localSelectionPool = new List<WorkdayEmployee>();               // Initialize a NEW selection pool

            var _settings = new DrugTestSettings                                                   // setup some testing prameters
            {
                TestingGroup = TestingGroup.O,                                                     // Only need this one. Set to 'O' (DOT-Other)
            };

            var selectionManager = new SelectionManager(_settings);                                // pass in settings
            selectionManager.PopulateSelectionPool(_employees, _mappings);                         // pass in employees and mappings

            var specialAssignmentEmployees = GetSpecialAssignmentMockList();                       // Get mock SpecialAssignment list

            selectionManager.AddSpecialAssignmentsToSelectionPool(specialAssignmentEmployees);     // Call the method to test


            _localSelectionPool = selectionManager.GetSelectionPool();                             // Assign the selection pool to the local
                                                                                                   

            Assert.Contains(_localSelectionPool, emp => emp.EmployeeId == 100000);                 // Ensure n was added

            Assert.DoesNotContain(_localSelectionPool, emp => emp.EmployeeId == 777777);           // Ensure "001" (GroupCode "T") was not added
            Assert.DoesNotContain(_localSelectionPool, emp => emp.EmployeeId == 999999);           // Ensure "002" (GroupCode "N") was not added
            Assert.DoesNotContain(_localSelectionPool, emp => emp.EmployeeId == 890567);           // Ensure "002" (GroupCode "N") was not added
            Assert.DoesNotContain(_localSelectionPool, emp => emp.EmployeeId == 890211);           // Ensure "002" (GroupCode "N") was not added

            Assert.Single(_localSelectionPool, emp => emp.EmployeeId == 890134);                   // Assert that only one EmployeeId "890567" exists
        }




        [Theory]
        // positionally:   Group    _SHOULD_  exist                  Should  _ N O T _ exist          
        [InlineData(TestingGroup.T, new[] { 777777 }, new[] {         999999, 100000,         890211, 890134 })]
        [InlineData(TestingGroup.N, new[] { 999999 }, new[] { 777777,         100000, 890567,         890134 })]
        [InlineData(TestingGroup.O, new[] { 100000 }, new[] { 777777, 999999,         890567, 890211         })]
        public void AddSpecialAssignmentsToSelectionPool_VariousScenarios(
                        TestingGroup testingGroup,                                                  // Pass the group code to test
                        int[] existsIds,                                                            // IDs that should exist in the pool
                        int[] notExistsIds)                                                         // IDs that should not exist in the pool
        {

            var _mappings = MockJobToDepartment.GetStaticMappings();                                // get and assign some mock objects
            var _employees = MockEmployeeHelper.GetStaticEmployees();

            List<WorkdayEmployee> _localSelectionPool = new List<WorkdayEmployee>();                // instantiate a new selection pool

            var _settings = new DrugTestSettings                                                    // instantiate a new DrugTestSettings object
            {
                TestingGroup = testingGroup,                                                        // Use the passed TestingGroup
            };

            var selectionManager = new SelectionManager(_settings);                                 // instantiate and setup a new SelectionManager
            selectionManager.PopulateSelectionPool(_employees, _mappings);                          // kick off the main method to populate the selection pool

            var specialAssignmentEmployees = GetSpecialAssignmentMockList();                        // get and assign some mock objects

            selectionManager.AddSpecialAssignmentsToSelectionPool(specialAssignmentEmployees);      //CODE UNDER TEST

            _localSelectionPool = selectionManager.GetSelectionPool();                              // assign the results


            foreach (var id in existsIds)                                                           // loop through the IDs that _SHOULD_ exist
            {
                Assert.Contains(_localSelectionPool, emp => emp.EmployeeId == id);
            }

            foreach (var id in notExistsIds)                                                        // loop through the IDs that _SHOULD_NOT_!_ exist
            {
                Assert.DoesNotContain(_localSelectionPool, emp => emp.EmployeeId == id);
            }
        }












        #region HELPER METHODS


        // Helper method to create mock SpecialAssignment list
        private List<SpecialAssignment> GetSpecialAssignmentMockList()
        {
            return new List<SpecialAssignment>
            {
                 // Each of these _SHOULD_ be added
                new SpecialAssignment { EmployeeId = 777777, SpecialAssignmentGroup = "T" },  // GroupCode "T"
                new SpecialAssignment { EmployeeId = 999999, SpecialAssignmentGroup = "N" },  // GroupCode "N"
                new SpecialAssignment { EmployeeId = 100000, SpecialAssignmentGroup = "O" },  // GroupCode "O"

                // these exist in the list (or should), thus should _NOT_ be added
                new SpecialAssignment { EmployeeId = 890567, SpecialAssignmentGroup = "T" },  // GroupCode "T"
                new SpecialAssignment { EmployeeId = 890211, SpecialAssignmentGroup = "N" },  // GroupCode "N"
                new SpecialAssignment { EmployeeId = 890134, SpecialAssignmentGroup = "O" },   // GroupCode "O"
            };
        }

    


        #endregion


    }
}
