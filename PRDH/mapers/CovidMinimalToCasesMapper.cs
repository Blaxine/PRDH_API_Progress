using AutoMapper;
using PRDH.models;

namespace PRDH.mapers
{
    public class CovidMinimalToCasesMapper: Profile
    {
        public CovidMinimalToCasesMapper()
        {
            CreateMap<LaboratoryTestsModel, CaseModel>()
                .ForMember(dest => dest.caseCategory, opt => opt.MapFrom(src => src.orderTestCategory))
                .ForMember(dest => dest.caseType, opt => opt.MapFrom(src => src.orderTestType))
                .ForMember(dest => dest.patientId, opt => opt.MapFrom(src => src.patientId))
                .ForMember(dest => dest.patientSex, opt => opt.MapFrom(src => src.patientSex))
                .ForMember(dest => dest.caseClassification, opt => opt.MapFrom(src => src.orderTestResult))
                .ForMember(dest => dest.patientAgeRange, opt => opt.MapFrom(src => src.patientAgeRange))
                .ForMember(dest => dest.patientPhysicalCity, opt => opt.MapFrom(src => src.patientCity))
                .ForMember(dest => dest.patientPhysicalRegion, opt => opt.MapFrom(src => src.patientRegion))
                .ForMember(dest => dest.earliestPositiveRankingTestSampleCollectedDate, opt => opt.MapFrom(src => src.resultReportDate))
                .ForMember(dest => dest.earliestPositiveDiagnosticTestSampleCollectedDate, opt => opt.MapFrom(src => src.sampleCollectedDate))
                .ForMember(dest => dest.caseCreatedAt, opt => opt.MapFrom(src => src.orderTestCreatedAt));


        }
    }
}
