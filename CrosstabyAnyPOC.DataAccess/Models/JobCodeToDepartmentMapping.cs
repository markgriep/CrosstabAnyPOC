using System.ComponentModel.DataAnnotations.Schema;

namespace CrosstabAnyPOC.DataAccess.Models
{

	[Table("JobCodeToDepartmentMapping")]
	public class JobCodeToDepartmentMapping
	{
		public int Id { get; set; }
		public int CostCenterId { get; set; }
		public string JobCodeId { get; set; } = string.Empty;


		public string TestingGroup { get; set; } = string.Empty;
		public bool IsActive { get; set; }

		//public bool IsHistorical { get; set; } // Don't need to see this one
		public DateTime EffectiveDate { get; set; }
	}
}
