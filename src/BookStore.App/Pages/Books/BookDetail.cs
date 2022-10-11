using BookStore.App.Dtos;
using BookStore.App.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BookStore.App.Pages.Books
{
    public partial class BookDetail
    {
        [Parameter]
        public string BookId { get; set; }

        public BookDto Book { get; set; } = new BookDto();

        [Inject]
        public IBookDataService BookDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Book = await BookDataService.GetById(int.Parse(BookId));
        }

        protected void NavigateToList()
        {
            NavigationManager.NavigateTo("/books");
        }
    }
}