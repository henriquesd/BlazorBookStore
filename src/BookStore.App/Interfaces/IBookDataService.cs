using BookStore.App.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.App.Interfaces
{
    public interface IBookDataService
    {
        Task<IEnumerable<BookDto>> GetAll();
        Task<BookDto> GetById(int bookId);
        Task<BookDto> Add(BookDto bookDto);
        Task Update(BookDto bookDto);
        Task Delete(int bookId);
    }
}