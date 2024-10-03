using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRDH.constants;
using PRDH.models;
using PRDH.services;

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

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] string orderTestType,
            [FromQuery] string sampleCollectedStartDate,
            [FromQuery] string sampleCollectedEndDate, 
            [FromQuery] string createdAtStartDate, 
            [FromQuery] string createdAtEndDate
            )
        // DATE time example 2024-09-27T00%3A00%3A00.0000000Z
        {
            // this worked
            string apiUrl = PrdhContants.ENDPOINT_URL+$"?OrderTestCategory=COVID-19&OrderTestType={orderTestType}&SampleCollectedStartDate={sampleCollectedStartDate}&SampleCollectedEndDate={sampleCollectedEndDate}&CreatedAtStartDate={createdAtStartDate}&CreatedAtEndDate={createdAtEndDate}";  // Replace with actual URL

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