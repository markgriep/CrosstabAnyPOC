using CrosstabAnyPOC.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosstabAnyPOC.Utilities
{
    public class MockIncludeExclude
    {
        public List<int> NotEligibleList { get; set; } = new List<int>();

        public List<SpecialAssignment> SpecialAssignmentsList { get; set; } = new List<SpecialAssignment>();


        public MockIncludeExclude()
        {
            SetRandomSpecialAssignmentsList();
            SetRandomNotEligibleList();
        }





        private void SetRandomNotEligibleList()
        {
            for (int i = 0; i < 25; i++)
            {
                NotEligibleList.Add(new Random().Next(892000, 892050));
            }
           
        }



        private void SetRandomSpecialAssignmentsList()
        {
            var random = new Random();
            for (int i = 0; i < random.Next(12, 55); i++)
                SpecialAssignmentsList.Add(new SpecialAssignment
                {
                    EmployeeId = random.Next(991000, 999000),
                    SpecialAssignmentGroup = ((TestingGroup)random.Next(0, 3)).ToString()
                });
        }









        private static List<int> GetEmployeeIdsToRemove(string groupCode)
        {
            return groupCode switch
            {
                "A" => new List<int> { 6666666, 6666667, 6666668 },
                "B" => new List<int> { 123456, 654321, 789012 },
                _ => new List<int> { 890567, 890211, 890134 },
            };
        }


      


    }
}
