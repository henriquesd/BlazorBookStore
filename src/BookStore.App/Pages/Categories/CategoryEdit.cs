using System.Threading.Tasks;
using BookStore.App.Dtos;
using BookStore.App.Interfaces;
using Microsoft.AspNetCore.Components;

namespace BookStore.App.Pages.Categories
{
    public partial class CategoryEdit
    {
        [Parameter]
        public string CategoryId { get; set; }

        public CategoryDto CategoryDto { get; set; } = new CategoryDto();

        [Inject]
        public ICategoryDataService CategoryDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;

        protected override async Task OnInitializedAsync()
        {
            CategoryDto = await CategoryDataService.GetById(int.Parse(CategoryId));

            Saved = false;

            int.TryParse(CategoryId, out var categoryId);

            if (categoryId != 0)
            {
                CategoryDto = await CategoryDataService.GetById(categoryId);
            }
        }

        protected async Task HandleValidSubmit()
        {
            Saved = false;

            if (CategoryDto.Id == 0)
            {
                var categoryAdded = await CategoryDataService.Add(CategoryDto);
                if (categoryAdded != null)
                {
                    StatusClass = "alert-success";
                    Message = "New category added successfully.";
                    Saved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new category. Please try again.";
                    Saved = false;
                }
            }
            else
            {
                await CategoryDataService.Update(CategoryDto);
                StatusClass = "alert-success";
                Message = "Category updated successfully.";
                Saved = true;
            }
        }

        protected void HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "There are some validation errors. Please try again.";
        }

        protected async Task DeleteCategory()
        {
            await CategoryDataService.Delete(CategoryDto.Id);

            StatusClass = "alert-success";
            Message = "Deleted successfully";

            Saved = true;
        }

        protected void NavigateToList()
        {
            NavigationManager.NavigateTo("/categories");
        }
    }
}
