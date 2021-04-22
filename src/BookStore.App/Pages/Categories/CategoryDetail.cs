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
        public ICategoryService CategoryService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var test = await CategoryService.GetById(int.Parse(CategoryId));
            Category = await CategoryService.GetById(int.Parse(CategoryId));
        }
    }
}