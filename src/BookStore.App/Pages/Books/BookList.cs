using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.App.Dtos;
using BookStore.App.Interfaces;
using Microsoft.AspNetCore.Components;

namespace BookStore.App.Pages.Books
{
    public partial class BookList
    {
        public IEnumerable<BookDto> Books { get; set; }

        [Inject]
        public IBookDataService BookDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Books = (await BookDataService.GetAll()).ToList();
        }

        protected void AddBook()
        {
            NavigationManager.NavigateTo("/bookedit");
        }

        protected async void DeleteBook(int bookId)
        {
            await BookDataService.Delete(bookId);
        }
    }
}