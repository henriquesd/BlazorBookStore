using System.Collections.Generic;
using System.Net.Http;
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
    }
}