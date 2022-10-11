using BookStore.App.Dtos;
using BookStore.App.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.App.Pages.Books
{
    public partial class BookEdit
    {
        [Parameter]
        public string BookId { get; set; }

        public BookDto BookDto { get; set; } = new BookDto();

        public IEnumerable<CategoryDto> CategoryListDto { get; set; }

        [Inject]
        public IBookDataService BookDataService { get; set; }

        [Inject]
        public ICategoryDataService CategoryDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;

        protected override async Task OnInitializedAsync()
        {
            Saved = false;

            CategoryListDto = await CategoryDataService.GetAll();

            if (BookId != null)
            {
                int.TryParse(BookId, out var bookId);

                if (bookId != 0)
                {
                    BookDto = await BookDataService.GetById(bookId);
                }
            }
        }

        protected async Task HandleValidSubmit()
        {
            Saved = false;

            if (BookDto.Id == 0)
            {
                var bookAdded = await BookDataService.Add(BookDto);
                if (bookAdded != null)
                {
                    StatusClass = "alert-success";
                    Message = "New book added successfully.";
                    Saved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new book. Please try again.";
                    Saved = false;
                }
            }
            else
            {
                await BookDataService.Update(BookDto);
                StatusClass = "alert-success";
                Message = "Book updated successfully.";
                Saved = true;
            }
        }

        protected void HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "There are some validation errors. Please try again.";
        }

        protected async Task DeleteBook()
        {
            await BookDataService.Delete(BookDto.Id);

            StatusClass = "alert-success";
            Message = "Deleted successfully";

            Saved = true;
        }

        protected void NavigateToList()
        {
            NavigationManager.NavigateTo("/books");
        }
    }
}
