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


        // Holds the list of all employees that was passed in
        private List<WorkdayEmployee> _currentEmployees{ get; set; }
        public IReadOnlyList<WorkdayEmployee> CurrentEmployees => _currentEmployees;



        // Holds the matrix of jobcodes to departments
        private List<JobCodeToDepartmentMapping> _jobCodeToDepartmentMatrix { get; set; }
        public IReadOnlyList<JobCodeToDepartmentMapping> JobCodeToDepartmentMatrix => _jobCodeToDepartmentMatrix;




        // property to hold the settings for this test 
        private DrugTestSettings _drugTestSettings { get; set; }
        public DrugTestSettings DrugTestSettings => _drugTestSettings;





        // Holds the INTIAL POOL and will be added to and subtracted from as we go
        private List<WorkdayEmployee> _selectionPool { get; set; }
        public IReadOnlyList<WorkdayEmployee> SelectionPool => _selectionPool;





        // These will be REMOVED from the selection pool
        private List<int> _notEligibleEmployees { get; set; }
        public IReadOnlyList<int> NotEligibleEmployees => _notEligibleEmployees;




        // These will be ADDED to the selection pool
        private List<SpecialAssignment> _specialAssignments { get; set; }
        public IReadOnlyList<SpecialAssignment> SpecialAssignments => _specialAssignments;




        // This is the FINAL POOL that will hold those who will be tested
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


        //Poplulates the settings that are dependent on the selection pool
        public void PopulateSettings()
        {

            // record the pool size
            _drugTestSettings.EmployeePoolSize = _selectionPool.Count;

          
            // Calcualate the NUMBER of employees to DRUG Test,  bBased on the percentage for this testing group
            _drugTestSettings.NumberOfEmployeesToDrugTest = 
                 (int)Math.Ceiling(_drugTestSettings.EmployeePoolSize * _drugTestSettings.PercentageOfEmployeesToDrugTest / 12);


            // caculate the NUMBER of employees to ALCOHOL test, based on the percentage for this testing group
            _drugTestSettings.NumberOfEmployeesToAlcoholTest = 
                        (int)Math.Ceiling(_drugTestSettings.EmployeePoolSize * _drugTestSettings.PercentageOfEmployeesToAlcoholTest / 12 );


            //get a list of ints, based on the number of employees to drug test
            var drugTestHashSet = 
                GetRandomHashSet(_drugTestSettings.NumberOfEmployeesToDrugTest, _drugTestSettings.EmployeePoolSize -1);



            // get a list of ints, based on the number of employees to alcohol test
            // number based on size of previous list and upper bound based on size of previos   list
            var alcoholTestHashSet = 
                GetRandomHashSet(_drugTestSettings.NumberOfEmployeesToAlcoholTest, drugTestHashSet.Count);

            _drugTestSettings.DrugSelectionPattern = 
                string.Join(" ", drugTestHashSet);

            _drugTestSettings.AlcoholSelectionPattern = 
                string.Join(" ", alcoholTestHashSet);




        }





        /// <summary>
        /// This is to get a blob of random non duplicated numbers between zero and X
        /// </summary>
        /// <param name="numberOfElementsNeeded"></param>
        /// <returns></returns>
        /// <remarks>Note this is zero based because it's working with the index of a list</remarks>
        /// <example> If you want 100 elements, the upper bound would be 99. Thus the zero-th element 
        /// to the 99th element would be 100.
        /// 
        /// </example>
        /// 
        public static HashSet<int> GetRandomHashSet(int numberOfElementsNeeded, int upperBoundIndex)
        {

            if (numberOfElementsNeeded < 1 || upperBoundIndex < 0)
            {
                throw new ArgumentException("Invalid input: Number of elements must be at least 1, and upper bound index at least 0.");
            }


            if (numberOfElementsNeeded > upperBoundIndex + 1)
            {
                throw new ArgumentException("Number of elements cannot exceed the range of unique values.");
            }

            
            if (numberOfElementsNeeded > 1000)
            {
                throw new ArgumentException("Number of elements asked for should be less than 1000 or less.");
            }


            HashSet<int> randomNumbers = new HashSet<int>();          // instantiate create a hashset

            Random rand = new Random();                               // create a random number generator

            while (randomNumbers.Count < numberOfElementsNeeded)      // Loop and put X random numbers in the hashset
            {
                var x = rand.Next(0, upperBoundIndex + 1);            // get a random number between 0 and upper bound
                randomNumbers.Add(x);                                 // add it to the hashset
            }

            return randomNumbers;
        }




    }
}



