using CrosstabAnyPOC;
using CrosstabAnyPOC.DataAccess.Models;
using CrosstabAnyPOC.Utilities;
using System.Diagnostics;



namespace TestVariousThings
{
    public class UnitTest1
    {
     

        [Fact] public void TestHashSetCreatorRandomeness_ShouldBeEvenlyDistributed()
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
        public void TestDistrbutionOfRandomeness_ShouldBeEvenlyDistributed()
        {
            var x = new List<List<int>>();

            for (int i = 0; i < 100; i++)
            {
                x.Add(SelectionManager.GetRandomHashset(100, 1000).ToList());
            }


        }




        [Fact]
        public void TestGettingRandomishEmployeeIds_ShouldGetSomething()
        {
            var x = MockEmployeeHelper.GenerateUniqueEmployeeIDs(200);
            Assert.True(x.Count == 200);
        }



        [Fact]
        public void TestGettingRandomEmployeesWithIds_ShouldGetSomething()
        {
            var x = MockEmployeeHelper.GetMockEmployees(20);

            Debug.WriteLine("#######################################################");
            Debug.WriteLine(x.Count);

            Debug.WriteLine(x[0].EmployeeName);
            Debug.WriteLine(x[0].JobCode);
            Debug.WriteLine(x[0].EmployeeId);

            Assert.True(x.Count == 20);
        }

    }
}