using CrosstabAnyPOC;
using CrosstabAnyPOC.Utilities;
using CrosstabAnyPOC.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        [InlineData(TestingGroup.N  ,new string[] { "Diana-AO", "Fiona-AO", "Edward-AO" })]
        [InlineData(TestingGroup.O  ,new string[] { "Hannah-AT", "Jonah-AT" })]
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






    }
}
