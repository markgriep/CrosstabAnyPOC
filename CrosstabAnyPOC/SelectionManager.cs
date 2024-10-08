using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosstabAnyPOC
{
    internal class SelectionManager
    {




        private static HashSet<int> GetHashsetForEmployees(int numberOfElements)
        {
            // create a hashset to hold that number of random numbers
            HashSet<int> randomNumbers = new HashSet<int>();

            // create a random number generator
            Random rand = new Random();

            //put that number of random numbers in the hashset
            while (randomNumbers.Count < numberOfElements)
            {
                randomNumbers.Add(rand.Next(0, numberOfElements));
            }

            return randomNumbers;
        }







    }
}
