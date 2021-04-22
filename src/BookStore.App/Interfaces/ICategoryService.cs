using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.App.Dtos;

namespace BookStore.App.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAll();
        Task<CategoryDto> GetById(int id);
    }
}
