namespace PRDH.models
{
    public class LaboratoryTestsModel
    {
        public string OrderTestId { get; set; }
        public string PatientId { get; set; }
        public string PatientAgeRange { get; set; }
        public string PatientSex { get; set; }
        public string PatientRegion { get; set; }
        public string PatientCity { get; set; }
        public string OrderTestCategory { get; set; }
        public string OrderTestType { get; set; }
        public string SampleCollectedDate { get; set; }
        public string ResultReportDate { get; set; }
        public string OrderTestResult { get; set; }
        public string OrderTestCreatedAt { get; set; }
    }
}