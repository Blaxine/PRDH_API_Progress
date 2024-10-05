using Microsoft.AspNetCore.Mvc;

namespace PRDH.models.requests
{
    public class GetCovidFilters
    {
     public string? OrderTestCategory { get; set; }
     public string? OrderTestType { get; set; }
     public string? SampleCollectedStartDate { get; set; }
     public string? SampleCollectedEndDate { get; set; }   
     public string? CreatedAtStartDate { get; set; }
     public string? CreatedAtEndDate { get; set; }
    }
}
