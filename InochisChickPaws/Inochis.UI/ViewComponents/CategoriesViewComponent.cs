using Microsoft.AspNetCore.Mvc;
using Inochis.Business.Abstract;

namespace Inochis.UI.ViewComponents
{
    public class CategoriesViewComponent:ViewComponent
    {
        private readonly ICategoryService _categoryManager;

        public CategoriesViewComponent(ICategoryService categoryManager)
        {
            _categoryManager = categoryManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await _categoryManager.GetTopCategories(5);
            return View(response.Data);
        }
    }
}
