using CrosstabAnyPOC.DataAccess.Models;

using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosstabAnyPOC
{
    public class SelectionManager
    {

        #region Variables/Properties


        // ALL EMPLOYEES: Holds the list of all current employees
        private List<WorkdayEmployee> _currentEmployees{ get; set; }
        public IReadOnlyList<WorkdayEmployee> CurrentEmployees => _currentEmployees;


        // MATRIX: Holds the job code to departments matrix
        private List<JobCodeToDepartmentMapping> _jobCodeToDepartmentMatrix { get; set; }
        public IReadOnlyList<JobCodeToDepartmentMapping> JobCodeToDepartmentMatrix => _jobCodeToDepartmentMatrix;               


        // SETTINGS: Holds the settings for this test
        private DrugTestSettings _drugTestSettings { get; set; }
        public DrugTestSettings DrugTestSettings => _drugTestSettings;


        // INITAL POOL: Holds the list of employees that will be added to and subtracted from as we go
        private List<WorkdayEmployee> _selectionPool { get; set; }
        public IReadOnlyList<WorkdayEmployee> SelectionPool => _selectionPool;


        // NOT ELIGIBLE: These people/employee IDs will be removed from the pool
        private List<int> _notEligibleEmployees { get; set; }
        public IReadOnlyList<int> NotEligibleEmployees => _notEligibleEmployees;


        // SPECIAL ASSIGNMENT: These people/employee ids be added to the selection pool
        private List<SpecialAssignment> _specialAssignments { get; set; }
        public IReadOnlyList<SpecialAssignment> SpecialAssignments => _specialAssignments;


        // FINAL POOL: This holds the list of those who will be tested
        private List<WorkdayEmployee> _selectedForTesting { get; set; }
        public IReadOnlyList<WorkdayEmployee> SelectedForTesting => _selectedForTesting;


        #endregion



        #region CTOR  -----------------------------------------------------------------------------------------------

        // CTOR that takes in TestSettings
        public SelectionManager( DrugTestSettings drugTestSettings)
        {
            _drugTestSettings = drugTestSettings;
        }

        #endregion



        #region METHODS  ------------------------------------------------------------------------------------


        /// <summary>
        /// Do initial population of selection pool taking employees and matrix
        /// </summary>
        /// <param name="currentEmployees"></param>
        /// <param name="jobCodeToDepartmentMatrix"></param>
        public void PopulateSelectionPool(List<WorkdayEmployee> currentEmployees, List<JobCodeToDepartmentMapping> jobCodeToDepartmentMatrix)
        {
            _currentEmployees = currentEmployees;                                     // Employees
            _jobCodeToDepartmentMatrix = jobCodeToDepartmentMatrix;                   // Matrix

            var selectionPool = _currentEmployees.Where(emp =>
                _jobCodeToDepartmentMatrix.Any(map =>
                    map.IsActive &&                                           // ONLY match active mappings                           -AND-
                    map.CostCenterId.ToString() == emp.Department &&          // CostCenter (departments) match each other            -AND-

                    map.TestingGroup == _drugTestSettings.TestingGroup.ToString() &&                  // testing group matches one of the enums.  (n, t, d)   -AND-

                    map.JobCodeId == emp.JobCode &&                           // Jobcodes match each other                            -AND-
                    true)).Select(ee => new { EmployeeID = ee.EmployeeId })   // always true place holder so I can insert others above
                    .ToList();

            _selectionPool = new List<WorkdayEmployee>();
            _selectionPool.AddRange(_currentEmployees.Where(emp => selectionPool.Any(sp => sp.EmployeeID == emp.EmployeeId)).ToList());
        }


        /// <summary>
        /// this is for testing. It will force the selection pool to be a certain size
        /// </summary>
        /// <param name="currentEmployees"></param>
        /// <param name="blindSelectNumberOfRandomEmployees"></param>
        [Obsolete("This is for testing only. Do not use in production code.")]
        [Conditional("DEBUG")]
        public void PopulateSelectionPoolForTesting(List<WorkdayEmployee> currentEmployees,
            int blindSelectNumberOfRandomEmployees)
        {
            _currentEmployees = currentEmployees;

            // Get a HashSet of unique random indices
            HashSet<int> randomIndices = GetRandomHashSet(blindSelectNumberOfRandomEmployees, _currentEmployees.Count);

            // Select employees based on the random indices
            _selectionPool = _currentEmployees
                .Where((emp, index) => randomIndices.Contains(index))
                .ToList();
        }


        /// <summary>
        /// Adds Special Assingment employees to the selection pool
        /// </summary>
        /// <param name="specialAssignmentEmployees"></param>
        public void AddSpecialAssignmentEmployeesToPool(List<SpecialAssignment> specialAssignmentEmployees)
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


        /// <summary>
        /// Removed employees from the selection pool that are ineligible for testing
        /// </summary>
        /// <param name="notEligibleEmployees"></param>
        public void RemoveNotEligibleEmployeesFromPool(List<int> notEligibleEmployees)
        {
            // Just a plain REMOVE, regardless of the group code or any other criteria.
            // in other words, just get those Employee IDs out of the selection pool.

            _notEligibleEmployees = notEligibleEmployees;

            _selectionPool.RemoveAll(emp => _notEligibleEmployees.Any(sa => sa == emp.EmployeeId));

            Debug.WriteLine($"In MGR Count: {_selectionPool.Count}");
        }


        /// <summary>
        /// Populates the settings that depend on operations in this class
        /// </summary>
        public void PopulateSettings()
        {

            // record the pool size
            _drugTestSettings.EmployeePoolSize = _selectionPool.Count;


            // Calculate the NUMBER of employees to DRUG Test, based on the percentage for this testing group and the pool size
            _drugTestSettings.NumberOfEmployeesToDrugTest =
                 (int)Math.Ceiling(_drugTestSettings.EmployeePoolSize * _drugTestSettings.PercentageOfEmployeesToDrugTest / 12);


            // caculate the NUMBER of employees to ALCOHOL test, based on the percentage for this testing group
            _drugTestSettings.NumberOfEmployeesToAlcoholTest =
                 (int)Math.Ceiling(_drugTestSettings.EmployeePoolSize * _drugTestSettings.PercentageOfEmployeesToAlcoholTest / 12 );


            //get a list of ints, based on the number of employees to DRUG test
            var drugTestHashSet =
                GetRandomHashSet(_drugTestSettings.NumberOfEmployeesToDrugTest, _drugTestSettings.EmployeePoolSize -1);
            _drugTestSettings.DrugTestHashset = drugTestHashSet.ToList();

            // get a list of ints, based on the number of employees to ALCOHOL test
            // number based on size of previous list and upper bound based on size of previos   list
            var alcoholTestHashSet =
                GetRandomHashSet(_drugTestSettings.NumberOfEmployeesToAlcoholTest, drugTestHashSet.Count);
            _drugTestSettings.AlcoholTestHashset = alcoholTestHashSet.ToList();

            // populate the string properties with the hashsets
            _drugTestSettings.DrugSelectionPattern =
                string.Join(" ", drugTestHashSet);

            _drugTestSettings.AlcoholSelectionPattern =
                string.Join(" ", alcoholTestHashSet);

        }


        /// <summary>
        /// This is where we actually populate the list of employees that will be tested
        /// </summary>
        public void PopulateSelectedForTesting()
        {
            // Assuming 'hashSet' is your HashSet<int> and '_selectionPool' is your list
            var selectedEmployees = _selectionPool.OrderBy(x => x.EmployeeName)
                .Where((employee, index) => _drugTestSettings.DrugTestHashset.Contains(index))
                .ToList();

            _selectedForTesting = selectedEmployees;



        }


        /// <summary>
        /// This is to get a blob of random non-duplicated numbers between zero and X
        /// </summary>
        /// <param name="numberOfElementsNeeded"></param>
        /// <returns></returns>
        /// <remarks>Note this is zero based because it's working with the index of a list</remarks>
        /// <example> To get 100 elements, the upper bound would be 99, covering indices from 0 to 99</example>
        public static HashSet<int> GetRandomHashSet(int numberOfElementsNeeded, int upperBoundIndex)
        {

            if (numberOfElementsNeeded < 1 || upperBoundIndex < 0)
            {
                throw new ArgumentOutOfRangeException("Invalid input: Number of elements must be at least 1, and upper bound index at least 0.");
            }


            if (numberOfElementsNeeded > upperBoundIndex + 1)
            {
                throw new ArgumentOutOfRangeException("Number of elements cannot exceed the range of unique values.");
            }


            if (numberOfElementsNeeded > 1000)
            {
                throw new ArgumentOutOfRangeException("Number of elements asked for should be 1000 or less.");
            }


            HashSet<int> randomNumbers = new HashSet<int>();          // instantiate create a hashset to store unique/distinct random numbers

            Random rand = new Random();                               // create a random number generator

            while (randomNumbers.Count < numberOfElementsNeeded)      // Loop and put X random numbers in the hashset
            {
                var x = rand.Next(0, upperBoundIndex + 1);            // get a random number between 0 and upper bound
                randomNumbers.Add(x);                                 // add it to the hashset
            }

            return randomNumbers;
        }


        #endregion
    }
}



