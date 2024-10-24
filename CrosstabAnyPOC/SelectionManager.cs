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
        private List<WorkdayEmployee> _currentEmployees{ get; set; }
        public IReadOnlyList<WorkdayEmployee> CurrentEmployees => _currentEmployees;



        // Holds the matrix of jobcodes to departments
        private List<JobCodeToDepartmentMapping> _jobCodeToDepartmentMatrix { get; set; }
        public List<JobCodeToDepartmentMapping> JobCodeToDepartmentMatrix => _jobCodeToDepartmentMatrix;




        // property to hold the settings for this test 
        private DrugTestSettings _drugTestSettings { get; set; }
        public DrugTestSettings DrugTestSettings => _drugTestSettings;





        // Holds the POOL
        private List<WorkdayEmployee> _selectionPool { get; set; }
        public List<WorkdayEmployee> SelectionPool => _selectionPool;
       
        





        private List<WorkdayEmployee> _selectedForTesting { get; set; }
        public List<WorkdayEmployee> SelectedForTesting => _selectedForTesting;


        private List<int> _notEligibleEmployees { get; set; }
        public List<int> NotEligibleEmployees => _notEligibleEmployees;





        private List<SpecialAssignment> _specialAssignments { get; set; }
        public List<SpecialAssignment> SpecialAssignments => _specialAssignments;





        #endregion


        #region CTOR  -----------------------------------------------------------------------------------------------


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

            var SelectionPool = _currentEmployees.Where(emp =>
                _jobCodeToDepartmentMatrix.Any(map =>
                    map.IsActive &&                                           // ONLY match active mappings                           -AND-
                    map.CostCenterId.ToString() == emp.Department &&          // CostCenter (departments) match each other            -AND-

                    map.TestingGroup == this._drugTestSettings.TestingGroup.ToString() &&                  // testing group matches one of the enums.  (n, t, d)   -AND-
                    
                    map.JobCodeId == emp.JobCode &&                           // Jobcodes match each other                            -AND-
                    true)).Select(ee => new { EmployeeID = ee.EmployeeId })   // always true place holder so I can insert others above
                    .ToList();

            _selectionPool = new List<WorkdayEmployee>();
            _selectionPool.AddRange(_currentEmployees.Where(emp => SelectionPool.Any(sp => sp.EmployeeID == emp.EmployeeId)).ToList());
        }




        public void AddSpecialAssignmentsToSelectionPool(List<SpecialAssignment> specialAssignmentEmployees)
        {

            _specialAssignments = specialAssignmentEmployees;
            // will get in a list of employee IDs that have a Special Assignment group code (T, N, O,)
            // ONLY add them to the selection pool if they are not already in the pool
            // AND if the group code matches the test group code
            // This list will be the same for every run.

            // Step 1: Filter employees based on the GroupCode
            var groupCode = _drugTestSettings.TestingGroup.ToString();                      // Convert TestingGroup enum to string

            var filteredByGroup = _specialAssignments
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
            // in other words, just get those Employee IDs out of the selection pool.

            _notEligibleEmployees = notEligibleEmployees;

            _selectionPool.RemoveAll(emp => _notEligibleEmployees.Any(sa => sa == emp.EmployeeId));

            Debug.WriteLine($"In MGR Count: {_selectionPool.Count}");
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
