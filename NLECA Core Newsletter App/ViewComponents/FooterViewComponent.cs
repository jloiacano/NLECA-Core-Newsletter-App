using Microsoft.AspNetCore.Mvc;
using NLECA_Core_Newsletter_App.Models;

namespace NLECA_Core_Newsletter_App.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            FooterModel footerModel = new FooterModel()
            {
                BrandName = "loiacanoDesigns"
            };

            return View(footerModel);
        }
    }
}
