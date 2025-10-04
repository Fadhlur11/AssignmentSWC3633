using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RecipeFinder.Models;

namespace RecipeFinder.Services
{
    public class MealApiService
    {
        private readonly HttpClient _httpClient;

        public MealApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new System.Uri("https://www.themealdb.com/api/json/v1/1/");
        }

        // Search meals by name
        public async Task<MealResponse?> SearchMealsAsync(string query)
        {
            var response = await _httpClient.GetAsync($"search.php?s={query}");

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<MealResponse>(json);

            return data;
        }

        public async Task<MealResponse?> GetMealByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"lookup.php?i={id}");

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<MealResponse>(json);

            return data;
        }
    }
}
