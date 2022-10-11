using System.Threading.Tasks;
using BookStore.App.Dtos;
using BookStore.App.Interfaces;
using Microsoft.AspNetCore.Components;

namespace BookStore.App.Pages.Categories
{
    public partial class CategoryDetail
    {
        [Parameter]
        public string CategoryId { get; set; }

        public CategoryDto Category { get; set; } = new CategoryDto();

        [Inject]
        public ICategoryDataService CategoryDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Category = await CategoryDataService.GetById(int.Parse(CategoryId));
        }

        protected void NavigateToList()
        {
            NavigationManager.NavigateTo("/categories");
        }
    }
}