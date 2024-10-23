using System;
using System.Collections.Generic;

namespace CrosstabAnyPOC.DataAccess.Models
{

    public partial class SpecialAssignment
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public string? SpecialAssignmentGroup { get; set; }
    }
}