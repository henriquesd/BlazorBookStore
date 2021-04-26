using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.App.Dtos;
using BookStore.App.Interfaces;
using Microsoft.AspNetCore.Components;

namespace BookStore.App.Pages.Categories
{
    public partial class CategoryList
    {
        public IEnumerable<CategoryDto> Categories { get; set; }

        [Inject]
        public ICategoryService CategoryDataService { get; set; }

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
    }
}
