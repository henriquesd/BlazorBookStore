using BookStore.App.Dtos;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using BookStore.App.Interfaces;

namespace BookStore.App.Services
{
    public class BookService : IBookDataService
    {
        private readonly HttpClient _httpClient;

        public BookService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<BookDto>> GetAll()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<BookDto>>
                (await _httpClient.GetStreamAsync($"api/books"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<BookDto> GetById(int bookId)
        {
            return await JsonSerializer.DeserializeAsync<BookDto>
                (await _httpClient.GetStreamAsync($"api/books/{bookId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<BookDto> Add(BookDto bookDto)
        {
            var bookJson = new StringContent(JsonSerializer.Serialize(bookDto), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/books", bookJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<BookDto>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task Update(BookDto bookDto)
        {
            var bookJson = new StringContent(JsonSerializer.Serialize(bookDto), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync($"api/books/{bookDto.Id}", bookJson);
        }

        public async Task Delete(int bookId)
        {
            await _httpClient.DeleteAsync($"api/books/{bookId}");
        }
    }
}