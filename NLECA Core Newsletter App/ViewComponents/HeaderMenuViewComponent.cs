using Microsoft.AspNetCore.Mvc;
using NLECA_Core_Newsletter_App.Models;

namespace NLECA_Core_Newsletter_App.ViewComponents
{
    public class HeaderMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            HeaderMenuModel headerMenu = new HeaderMenuModel()
            {
                BrandName = "nleca new"
            };

            return View(headerMenu);
        }
    }
}
