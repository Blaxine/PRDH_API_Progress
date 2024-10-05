using Microsoft.AspNetCore.Mvc;

namespace PRDH.models.requests
{
    public class GetCovidFilters
    {
     public string? OrderTestCategory { get; set; }
     public string? orderTestType { get; set; }
     public string? sampleCollectedStartDate { get; set; }
     public string? sampleCollectedEndDate { get; set; }   
     public string? createdAtStartDate { get; set; }
     public string? createdAtEndDate { get; set; }
    }
}
