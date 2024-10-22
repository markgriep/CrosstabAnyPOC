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




        [Fact]
        public void TestCountOfEmployees_ShouldBeApproximately120()
        {
            var _mappings = MockJobToDepartment.GetMockMappings();
            var _employees = MockEmployeeHelper.GetMockEmployees(); 


            var _settings = new DrugTestSettings                                        // Configure some settings for the test                                             
            {
                TestNumber = 1,
                TestOperatorName = "Mark G",
                RequestDateTime = DateTime.Now,

                TestType = TestType.Both,
                TestingGroup = TestingGroup.N,
                TestCategory = TestCategory.Random,
                TestSubjectSelectionMethod = TestSubjectSelectionMethod.Automatic,

                PercentageOfEmployeesToTest = 0.1M,                                     // X percent 
                NumberOfEmployeesToTest = 0,                                        // 0 employees
            };

            var x = new SelectionManager(new DrugTestSettings());


            x.PopulateSelectionPool(_employees,_mappings);



        }
    }
}
