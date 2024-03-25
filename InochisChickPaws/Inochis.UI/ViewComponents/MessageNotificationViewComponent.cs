using Microsoft.AspNetCore.Mvc;

namespace Inochis.UI.ViewComponents
{
    public class MessageNotificationViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
