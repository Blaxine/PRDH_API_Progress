using Microsoft.AspNetCore.Mvc;
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
        public WorkerService(HttpClient httpClient, CaseDataBaseContext csdb)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _caseDatabaseContext = csdb ?? throw new ArgumentNullException(nameof(csdb));
        }

        public async Task<List<LaboratoryTestsModel>?> GetCovid(string apiUrl)
        {
            try
            {
                var response = await _httpClient.GetAsync(apiUrl);

                // Check if the response is successful
                response.EnsureSuccessStatusCode();

                // Read and deserialize the response body
                string responseBody = await response.Content.ReadAsStringAsync();
                List<LaboratoryTestsModel> results = JsonConvert.DeserializeObject<List<LaboratoryTestsModel>>(responseBody);

                return results;
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"Request error: {httpEx.Message}");
            }
            catch (JsonSerializationException jsonEx)
            {
                Console.WriteLine($"JSON deserialization error: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return null;

        }

        public async Task<CaseModel> StoreCaseDate(CaseModel @case)
        {
            _caseDatabaseContext.Add(@case);
            await _caseDatabaseContext.SaveChangesAsync().ConfigureAwait(false);
            return @case;
        } 
    }
}

