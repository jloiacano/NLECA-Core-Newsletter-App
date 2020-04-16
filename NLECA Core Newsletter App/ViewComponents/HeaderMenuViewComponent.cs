using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NLECA_Core_Newsletter_App.Models;

namespace NLECA_Core_Newsletter_App.ViewComponents
{
    public class HeaderMenuViewComponent : ViewComponent
    {
        private readonly IConfiguration config;

        public HeaderMenuViewComponent(IConfiguration config)
        {
            this.config = config;
        }
        public IViewComponentResult Invoke()
        {
            HeaderMenuModel headerMenu = new HeaderMenuModel()
            {
                BrandName = config.GetConnectionString("nlecaDBConnection")
            };

            return View(headerMenu);
        }
    }
}
