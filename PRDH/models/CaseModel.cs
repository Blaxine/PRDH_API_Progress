using System.ComponentModel.DataAnnotations;

namespace PRDH.models
{
    public class CaseModel
    {
        [Key]
        public string caseId { get; set; }
        public string caseCategory { get; set; }
        public string caseType { get; set; }
        public string caseClassification { get; set; }
        public string patientId { get; set; }
        public string patientAgeRange { get; set; }
        public string patientSex { get; set; }
        public string patientPhysicalCity { get; set; }
        public string patientPhysicalRegion { get; set; }
        public string earliestPositiveRankingTestSampleCollectedDate { get; set; }
        public string earliestPositiveDiagnosticTestSampleCollectedDate { get; set; }
        public string caseCreatedAt { get; set; }
    } 
}
