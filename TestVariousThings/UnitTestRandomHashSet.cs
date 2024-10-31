using CrosstabAnyPOC;
using CrosstabAnyPOC.DataAccess.Models;
using CrosstabAnyPOC.Utilities;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Diagnostics;



namespace TestVariousThings
{
    public class UnitTestRandomHashSet
    {
        // elementsneeded, upperboundindex
        [Theory]
        [InlineData(0, 10)]   // we'd need zero?!?! elements
        [InlineData(-1, 10)]  // Negative number of elements??
        [InlineData(5, -1)]   // upper bound is negative (< 0)
        [InlineData(0, -1)]   // both messed up
        public void GetRandomHashSet_InvalidParameters_ThrowsArgumentException(int numberOfElementsNeeded, int upperBoundIndex)
        {
            Assert.Throws<ArgumentException>(() => SelectionManager.GetRandomHashSet(numberOfElementsNeeded, upperBoundIndex));
        }


        [Theory]
        [InlineData(11, 9)]   // 11 needed but only goes to index 9 (count=10
        [InlineData(6, 4)]    // similar.  want 6 but only 5 available
        [InlineData(2, 0)]    // need 2 but only 1 available
        [InlineData(10, 8)]   // need 10 but only 9 available
        public void GetRandomHashSet_ExceedsRange_ThrowsArgumentException(int numberOfElementsNeeded, int upperBoundIndex)
        {
            Assert.Throws<ArgumentException>(() => SelectionManager.GetRandomHashSet(numberOfElementsNeeded, upperBoundIndex));
        }


        [Theory]
        [InlineData(1001, 999)]       // want 3, but only 4
        [InlineData(1001, 2000)]      // numberOfElementsNeeded > upperBoundIndex + 1
        [InlineData(50000, 800000)]   // numberOfElementsNeeded > upperBoundIndex + 1
        public void GetRandomHashSet_InsufficientUniqueValues_ThrowsArgumentException(int numberOfElementsNeeded, int upperBoundIndex)
        {
            Assert.Throws<ArgumentException>(() => SelectionManager.GetRandomHashSet(numberOfElementsNeeded, upperBoundIndex));
        }


        [Theory]
        [InlineData(1000, 999)]    // Request 1000 but have 1000 available
        [InlineData(500, 499)]     // Request 500 but have 500 available
        [InlineData(5, 10)]        // Request 5 but have 11 available
        [InlineData(3, 3)]         // Request 3 but have 4 available
        [InlineData(7, 20)]        // Request 7 but 21 available
        [InlineData(10, 15)]       // Request 10 but 16 available
        [InlineData(5, 6)]         // Request 5 but 7 available
        [InlineData(5, 5)]         // Request 5 but 6 available
        [InlineData(5, 4)]         // Request 5 but 5 available
        [InlineData(4, 3)]         // Request 4 but 4 available
        [InlineData(3, 2)]         // Request 3 but 3 available
        [InlineData(2, 1)]         // Request 2 but 2 available
        [InlineData(1, 0)]         // Request 1 but 1 available
        public void GetRandomHashSet_ValidParameters_ReturnsCorrectCount(int numberOfElementsNeeded, int upperBoundIndex)
        {
            var result = SelectionManager.GetRandomHashSet(numberOfElementsNeeded, upperBoundIndex);
            Assert.Equal(numberOfElementsNeeded, result.Count);
        }











        [Fact]
        public void TestHashSetCreatorRandomeness_ShouldBeEvenlyDistributed()
        {

            var hashset = SelectionManager.GetRandomHashSet(20, 199);

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
                x.Add(SelectionManager.GetRandomHashSet(100, 1000).ToList());
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





        [Fact]
        public void GetRandomHashSet_TestDistributionOfNumbersAfterCallingManyTimes_ShouldBeAbout10Occurrences()
        {
            int[] counts = new int[100];                                        // make an array to hold 100 numbers

            for (int i = 0; i < 1000; i++)                                      // Loop throough x 1000
            {
                var hashset = SelectionManager.GetRandomHashSet(10, 99);        // run the MUT. (get 100 random numbers between 0 and 99)

                foreach (var item in hashset)                                   // loop thru the results
                {
                    counts[item]++;                                             // increment the count at that index everytime it's a match, so to speak
                }
            }

            foreach (int count in counts)
            {
                Assert.InRange(count, 70, 130);                                 // should expect abot 100 occurrences of each number (give or take...)
            }
           
            var x = counts.Where(c => c % 2 == 0).Count();                      // get the odd/even count

            Assert.InRange(x, 40, 60);                                          // should be about 50/50 (give or take...)
        }
    }
}