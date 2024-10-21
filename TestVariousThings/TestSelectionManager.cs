using CrosstabAnyPOC;
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

    }
}
