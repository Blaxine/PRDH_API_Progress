using AutoMapper;
using PRDH.models;

namespace PRDH.mapers
{
    public class CovidMinimalToCasesMapper: Profile
    {
        public CovidMinimalToCasesMapper()
        {
            CreateMap<LaboratoryTestsModel, CaseModel>()
                .ForMember(dest => dest.CaseCategory, opt => opt.MapFrom(src => src.OrderTestCategory))
                .ForMember(dest => dest.CaseType, opt => opt.MapFrom(src => src.OrderTestType))
                .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.PatientId))
                .ForMember(dest => dest.PatientSex, opt => opt.MapFrom(src => src.PatientSex))
                .ForMember(dest => dest.CaseClassification, opt => opt.MapFrom(src => src.OrderTestResult))
                .ForMember(dest => dest.PatientAgeRange, opt => opt.MapFrom(src => src.PatientAgeRange))
                .ForMember(dest => dest.PatientPhysicalCity, opt => opt.MapFrom(src => src.PatientCity))
                .ForMember(dest => dest.PatientPhysicalRegion, opt => opt.MapFrom(src => src.PatientRegion))
                .ForMember(dest => dest.EarliestPositiveRankingTestSampleCollectedDate, opt => opt.MapFrom(src => src.ResultReportDate))
                .ForMember(dest => dest.EarliestPositiveDiagnosticTestSampleCollectedDate, opt => opt.MapFrom(src => src.SampleCollectedDate))
                .ForMember(dest => dest.CaseCreatedAt, opt => opt.MapFrom(src => src.OrderTestCreatedAt));

        }
    }
}
