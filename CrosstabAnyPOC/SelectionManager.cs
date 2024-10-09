using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosstabAnyPOC
{
    public class SelectionManager
    {



        /// <summary>
        /// This is to get a blob of random non duplicated numbers between zero and X
        /// </summary>
        /// <param name="numberOfElements"></param>
        /// <returns></returns>
        /// <remarks>Note this is zero based because it's working with the index of a list</remarks>
        public static HashSet<int> GetRandomHashset(int numberOfElements, int upperBound)
        {
            
            if (numberOfElements > upperBound)                      // prevents loop if numberOfElements is greater than upperBound
            {
                throw new ArgumentException("RandomHashSet: numberOfElements cannot be greater than upperBound.");
            }

            HashSet<int> randomNumbers = new HashSet<int>();        // create a hashset to hold X random numbers

            Random rand = new Random();                             // create a random number generator
            
            while (randomNumbers.Count < numberOfElements)          // put X random numbers in the hashset
            {
                randomNumbers.Add(rand.Next(0, upperBound));
            }

            return randomNumbers;
        }







    }
}
