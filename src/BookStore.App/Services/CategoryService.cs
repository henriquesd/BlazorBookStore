using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BookStore.App.Dtos;
using BookStore.App.Interfaces;

namespace BookStore.App.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CategoryDto>> GetAll()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<CategoryDto>>
                (await _httpClient.GetStreamAsync($"api/categories"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<CategoryDto> GetById(int categoryId)
        {
            return await JsonSerializer.DeserializeAsync<CategoryDto>
                (await _httpClient.GetStreamAsync($"api/categories/{categoryId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<CategoryDto> Add(CategoryDto categoryDto)
        {
            var categoryJson = new StringContent(JsonSerializer.Serialize(categoryDto), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/categories", categoryJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<CategoryDto>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task Update(CategoryDto categoryDto)
        {
            var categoryJson = new StringContent(JsonSerializer.Serialize(categoryDto), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync($"api/categories/{categoryDto.Id}", categoryJson);
        }

        public async Task Delete(int categoryId)
        {
            await _httpClient.DeleteAsync($"api/categories/{categoryId}");
        }
    }
}
