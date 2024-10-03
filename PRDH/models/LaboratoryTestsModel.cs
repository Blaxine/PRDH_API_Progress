namespace PRDH.models
{
    public class LaboratoryTestsModel
    {
        public string orderTestId { get; set; }
        public string patientId { get; set; }
        public string patientAgeRange { get; set; }
        public string patientSex { get; set; }
        public string patientRegion { get; set; }
        public string patientCity { get; set; }
        public string orderTestCategory { get; set; }
        public string orderTestType { get; set; }
        public string sampleCollectedDate { get; set; }
        public string resultReportDate { get; set; }
        public string orderTestResult { get; set; }
        public string orderTestCreatedAt { get; set; }
    }
}