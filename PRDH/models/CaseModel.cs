using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace PRDH.models
{
    public class CaseModel
    {
        
        [Key]
        public string CaseId { get; set; } = Guid.NewGuid().ToString();
        public string CaseCategory { get; set; }
        public string CaseType { get; set; }
        public string CaseClassification { get; set; }
        public string PatientId { get; set; }
        public string PatientAgeRange { get; set; }
        public string PatientSex { get; set; }
        public string PatientPhysicalCity { get; set; }
        public string PatientPhysicalRegion { get; set; }
        public string EarliestPositiveRankingTestSampleCollectedDate { get; set; }
        public string EarliestPositiveDiagnosticTestSampleCollectedDate { get; set; }
        public string CaseCreatedAt { get; set; }
    } 
}
