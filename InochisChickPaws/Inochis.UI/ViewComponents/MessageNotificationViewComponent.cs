using Microsoft.AspNetCore.Mvc;

namespace Inochis.UI.ViewComponents
{
    public class MessageNotificationViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
