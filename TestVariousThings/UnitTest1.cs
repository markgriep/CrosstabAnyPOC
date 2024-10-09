using CrosstabAnyPOC;
using xunit;


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
                x.Add(SelectionManager.GetRandomHashset(100).ToList());
            }


        }




    }
}