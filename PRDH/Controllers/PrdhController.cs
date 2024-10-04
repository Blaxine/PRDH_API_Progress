using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRDH.constants;
using PRDH.models;
using PRDH.models.requests;
using PRDH.services;
using PRDH.validators;
using System.ComponentModel.DataAnnotations;

namespace PRDH.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrdhController : ControllerBase
    {
        private readonly WorkerService _userService;

        public PrdhController(WorkerService userService)
        {
            _userService = userService?? throw new ArgumentNullException(nameof(_userService)); ;
        }

        [HttpPost]
        public async Task<IActionResult> GetUsers([FromBody] GetCovidFilters covidFilters)
        // DATE time example 2024-09-27T00%3A00%3A00.0000000Z
        {

            var validator = new GetCovidValidator(); 
            var validationResult = await validator.ValidateAsync(covidFilters);
            
            if(!validationResult.IsValid) {
            return BadRequest(validationResult);
            }

            // this worked
            string apiUrl = PrdhContants.ENDPOINT_URL+$"?OrderTestCategory=COVID-19&OrderTestType={covidFilters.orderTestType}&SampleCollectedStartDate={covidFilters.sampleCollectedStartDate}&SampleCollectedEndDate={covidFilters.sampleCollectedEndDate}&CreatedAtStartDate={covidFilters.createdAtStartDate}&CreatedAtEndDate={covidFilters.createdAtEndDate}";  // Replace with actual URL

            var users = await _userService.GetCovid(apiUrl);
            if (users.Count() > 0)
            {
                // Group users by patientId
                var groupOrders = users.GroupBy(value => value?.patientId.ToString())
                                       .Select(group => new
                                       {
                                           PatientId = group.Key,
                                           Users = group.ToList(),
                                           numberOfPositiveCases = group.Count(x=>x.orderTestResult.ToString() == "Positive")
                                       });
                return Ok(groupOrders);
            }
            return Ok(users);  // Return the data as JSON
        }

    }
}