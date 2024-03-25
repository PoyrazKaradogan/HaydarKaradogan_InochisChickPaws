using Microsoft.AspNetCore.Mvc.Rendering;
using Inochis.Shared.ViewModels;

namespace Inochis.UI.Areas.Admin.Models
{
    public class OrderFilterViewModel
    {
        public List<AdminOrderViewModel> Orders { get; set; }
        public List<SelectListItem> ProductListItems { get; set; }
    }
}
