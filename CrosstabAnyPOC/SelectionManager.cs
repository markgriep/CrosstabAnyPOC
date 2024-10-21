using CrosstabAnyPOC.DataAccess.Models;
using CrosstabAnyPOC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosstabAnyPOC
{
    public class SelectionManager
    {

        // variables


        // Store the list of all employees that was passed in
        public List<WorkdayEmployee> _currentEmployees{ get; set; }


        public List<JobCodeToDepartmentMapping> _jobCodeToDepartmentMatrix { get; set; }



        // property to hols DrugTestSettings 
        public DrugTestSettings _drugTestSettings { get; set; }


        private List<WorkdayEmployee> _selectionPool { get; set; }



        // constructor that takes in all three of these parameters
        public SelectionManager( DrugTestSettings drugTestSettings)
        {
            _drugTestSettings = drugTestSettings;
        }




        // do initial populate of selection pool taking in the other two parameters
        public void PopulateSelectionPool(List<WorkdayEmployee> currentEmployees, List<JobCodeToDepartmentMapping> jobCodeToDepartmentMatrix)
        {
            _currentEmployees = currentEmployees;
            _jobCodeToDepartmentMatrix = jobCodeToDepartmentMatrix;


            var SelectionPool = _currentEmployees.Where(emp =>
                _jobCodeToDepartmentMatrix.Any(map =>
                    map.IsActive &&                                         // ONLY match active mappings                           -AND-
                    map.CostCenterId.ToString() == emp.Department &&        // CostCenter (departments) match each other            -AND-
                    map.TestingGroup == TestingGroup.T.ToString() &&        // testing group matches one of the enums.  (n, t, d)   -AND-
                    map.JobCodeId == emp.JobCode &&                         // Jobcodes match each other                            -AND-
                    true)).Select(ee => new { EmployeeID = ee.EmployeeId })                                                  // always true place holder so I can insert others above
                    .ToList();

            _selectionPool = new List<WorkdayEmployee>();
            _selectionPool.AddRange(_currentEmployees.Where(emp => SelectionPool.Any(sp => sp.EmployeeID == emp.EmployeeId)).ToList());
        }


        public List<WorkdayEmployee> GetSelectionPool()
        {
            return _selectionPool;
        }















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
