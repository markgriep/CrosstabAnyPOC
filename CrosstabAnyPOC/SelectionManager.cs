using CrosstabAnyPOC.DataAccess.Models;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosstabAnyPOC
{
    public class SelectionManager
    {

        #region Variables/Properties   


        // Holds the list of all employees that was passed in
        public List<WorkdayEmployee> _currentEmployees{ get; set; }


        // Holds the matrix of jobcodes to departments
        public List<JobCodeToDepartmentMapping> _jobCodeToDepartmentMatrix { get; set; }

        // property to hold the settings for this test 
        public DrugTestSettings _drugTestSettings { get; set; }

        // Holds the 
        private List<WorkdayEmployee> _selectionPool { get; set; }


        #endregion


        #region CTOR


        // CTOR that takes in TestSettings
        public SelectionManager( DrugTestSettings drugTestSettings)
        {
            _drugTestSettings = drugTestSettings;
        }


        #endregion




        /// <summary>
        /// Do initial population of selection pool taking employees and matrix
        /// </summary>
        /// <param name="currentEmployees"></param>
        /// <param name="jobCodeToDepartmentMatrix"></param>
        public void PopulateSelectionPool(List<WorkdayEmployee> currentEmployees, List<JobCodeToDepartmentMapping> jobCodeToDepartmentMatrix)
        {
            _currentEmployees = currentEmployees;                                     // Employees
            _jobCodeToDepartmentMatrix = jobCodeToDepartmentMatrix;                   // Matrix

            var tSelMethod = _drugTestSettings.TestSubjectSelectionMethod;            // enum - auto or manual
            var tType = _drugTestSettings.TestType;                                   // enum - drug, alcohol, both
            var tCategory = _drugTestSettings.TestCategory;                           // enum - random, followup, postaccident, etc
            var tGroup = _drugTestSettings.TestingGroup;                              // enum - transit, non-transit, dot

            var tNumber = _drugTestSettings.TestNumber;                               // test number
            var tOperator = _drugTestSettings.TestOperatorName;                       // test operator name
            var tReqDtTim = _drugTestSettings.RequestDateTime;                        // request date time
            var tNumEmplo = _drugTestSettings.NumberOfEmployeesToTest;                // number of employees to test
            var tEmpPoolSz = _drugTestSettings.EmployeePoolSize;                      // pool size
            var tAlcPerct = _drugTestSettings.PercentageOfEmployeesToAlcoholTest;     // percent to Alcohol test
            var tDrugPerc = _drugTestSettings.PercentageOfEmployeesToDrugTest;        // percent to Drug test
            var tDrugHash = _drugTestSettings.DrugSelectionPattern;                   // stringized hashset for drug selection
            var tAlcoHash = _drugTestSettings.AlcoholSelectionPattern;                // stringized hashset for alcohol selection

   
            var x = tGroup.ToString();                                             // convert enum to string

            var SelectionPool = _currentEmployees.Where(emp =>
                _jobCodeToDepartmentMatrix.Any(map =>
                    map.IsActive &&                                           // ONLY match active mappings                           -AND-
                    map.CostCenterId.ToString() == emp.Department &&          // CostCenter (departments) match each other            -AND-

                    map.TestingGroup == tGroup.ToString() &&                  // testing group matches one of the enums.  (n, t, d)   -AND-
                    
                    map.JobCodeId == emp.JobCode &&                           // Jobcodes match each other                            -AND-
                    true)).Select(ee => new { EmployeeID = ee.EmployeeId })   // always true place holder so I can insert others above
                    .ToList();

            _selectionPool = new List<WorkdayEmployee>();
            _selectionPool.AddRange(_currentEmployees.Where(emp => SelectionPool.Any(sp => sp.EmployeeID == emp.EmployeeId)).ToList());
        }




        public void AddSpecialAssignmentsToSelectionPool(List<SpecialAssignment> specialAssignmentEmployees)
        {
            // will get in a list of employee IDs that have a Special Assignment group code (T, N, O,)
            // ONLY add them to the selection pool if they are not already in the pool
            // AND if the group code matches the test group code
            // This list will be the same for every run.


            //_selectionPool.AddRange(specialAssignmentEmployees                                               // Add employees to _selectionPool
            //        .Where(sa => sa.SpecialAssignmentGroup == _drugTestSettings.TestingGroup.ToString())     // Filter employees that match the defined GroupCode
            //        .Where(sa => !_selectionPool.Any(sp => sp.EmployeeId == sa.EmployeeId))
            //        .Select(sa => new WorkdayEmployee                                                        // Create a new WorkdayEmployee object
            //        {
            //            EmployeeId = sa.EmployeeId,                                                          // Add the EmployeeId from SpecialAssignment
            //        }));


            // Step 1: Filter employees based on the GroupCode
            var groupCode = _drugTestSettings.TestingGroup.ToString();                      // Convert TestingGroup enum to string

            var filteredByGroup = specialAssignmentEmployees
                .Where(sa => sa.SpecialAssignmentGroup == groupCode);                       // Employees matching the group code

            // Step 2: Filter out employees that are already in the selection pool
            var notInSelectionPool = filteredByGroup
                .Where(sa => !_selectionPool.Any(sp => sp.EmployeeId == sa.EmployeeId));    // Exclude employees already in _selectionPool

            // Step 3: Convert SpecialAssignment objects to WorkdayEmployee objects
            var newWorkdayEmployees = notInSelectionPool
                .Select(sa => new WorkdayEmployee                                           // Create new WorkdayEmployee objects
                {
                    EmployeeId = sa.EmployeeId                                              // Add EmployeeId from SpecialAssignment
                })
                .ToList();                                                                  // Convert to a list

            // Step 4: Add the new employees to the selection pool
            _selectionPool.AddRange(newWorkdayEmployees);                                   // Add the new WorkdayEmployee list to _selectionPool



            Debug.WriteLine($"In MGR Count: {_selectionPool.Count}");

        }




            public void RemoveNotEligiblesFromSelectionPool(List<int> notEligibleEmployees)
        {

            // Just a plain REMOVE, regardless of the group code or any other criteria.
            //_selectionPool.RemoveAll(emp => notEligibleEmployees.Any(sa => sa.EmployeeId == emp.EmployeeId));
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
