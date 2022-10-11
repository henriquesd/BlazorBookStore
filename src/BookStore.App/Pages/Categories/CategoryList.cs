using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.App.Dtos;
using BookStore.App.Interfaces;
using BookStore.App.Services;
using Microsoft.AspNetCore.Components;

namespace BookStore.App.Pages.Categories
{
    public partial class CategoryList
    {
        public IEnumerable<CategoryDto> Categories { get; set; }

        [Inject]
        public ICategoryDataService CategoryDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Categories = (await CategoryDataService.GetAll()).ToList();
        }

        protected void AddCategory()
        {
            NavigationManager.NavigateTo("/categoryedit");
        }

        protected async void DeleteCategory(int categoryId)
        {
            await CategoryDataService.Delete(categoryId);
        }
    }
}
