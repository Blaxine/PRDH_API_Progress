using Microsoft.OpenApi.Any;
using Newtonsoft.Json;
using PRDH.models;
using PRDH.validators;
using System.Text.RegularExpressions;

namespace PRDH.services
{
    public class WorkerService
    {

        private readonly HttpClient _httpClient = new HttpClient();
        public WorkerService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
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
                // Log the HTTP request error
                Console.WriteLine($"Request error: {httpEx.Message}");
            }
            catch (JsonSerializationException jsonEx)
            {
                // Log the deserialization error
                Console.WriteLine($"JSON deserialization error: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                // General exception handler for other unexpected errors
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return null; // Return null or handle it appropriately

        }
    }
}
