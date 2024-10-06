using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Newtonsoft.Json;
using PRDH.DataBase;
using PRDH.models;
using PRDH.validators;
using System.Text.RegularExpressions;

namespace PRDH.services
{
    public class WorkerService
    {

        private readonly HttpClient _httpClient = new HttpClient();
        private readonly CaseDataBaseContext _caseDatabaseContext;
        private readonly ILogger<WorkerService> _logger;
        public WorkerService(HttpClient httpClient, CaseDataBaseContext csdb, ILogger<WorkerService> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _caseDatabaseContext = csdb ?? throw new ArgumentNullException(nameof(csdb));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }

        public async Task<List<LaboratoryTestsModel>?> GetCovid(string apiUrl)
        {
            try
            {
                var response = await _httpClient.GetAsync(apiUrl);

                // Check if the response is successful
                response.EnsureSuccessStatusCode();

                // Read and deserialize the response body
                string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
              
                List<LaboratoryTestsModel> results = JsonConvert.DeserializeObject<List<LaboratoryTestsModel>>(responseBody)!;

                return results;
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogCritical($"Request error: {httpEx.Message}");
            }
            catch (JsonSerializationException jsonEx)
            {
                _logger.LogCritical($"JSON deserialization error: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An error occurred: {ex.Message}");
            }

            return default;

        }

        public async Task<CaseModel> StoreCaseDate(CaseModel @case,int orderTotal)
        {
            @case.OrderCounts = orderTotal;
            @case.CaseId  = Guid.NewGuid().ToString();
            _caseDatabaseContext.Add(@case);
            await _caseDatabaseContext.SaveChangesAsync().ConfigureAwait(false);
            return @case;
        }

        public async Task<ActionResult<IEnumerable<CaseModel>>> ListCases()
        {
            var results = await _caseDatabaseContext.Cases.ToListAsync().ConfigureAwait(false) ;
            if (results != null && results.Count()>0)  return results;
            return default!;
            
        }
    }
   
}

