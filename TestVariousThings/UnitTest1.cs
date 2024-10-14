using CrosstabAnyPOC;
using CrosstabAnyPOC.Models;
using System.Diagnostics;



namespace TestVariousThings
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

        }


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
            var x = WorkDayEmployee.GenerateUniqueEmployeeIDs(200);
            Assert.True(x.Count == 20);
        }



        [Fact]
        public void TestGettingRandomEmployeesWithIds_ShouldGetSomething()
        {
            var x = WorkDayEmployee.GetMockEmployees(20);



            Debug.WriteLine("#######################################################");
            Debug.WriteLine(x.Count);

            Debug.WriteLine(x[0].Name);
            Debug.WriteLine(x[0].JobCode);
            Debug.WriteLine(x[0].EmployeeId);

            Assert.True(x.Count == 20);


        }






    }
}