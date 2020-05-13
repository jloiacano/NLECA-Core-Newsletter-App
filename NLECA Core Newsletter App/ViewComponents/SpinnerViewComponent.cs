using Microsoft.AspNetCore.Mvc;
using NLECA_Core_Newsletter_App.Models;

namespace NLECA_Core_Newsletter_App.ViewComponents
{
    public class SpinnerViewComponent : ViewComponent
    {        public IViewComponentResult Invoke()
        {
            bool useSpinner = HttpContext.Request.Cookies["UseSpinner"] != null;
            SpinnerModel spinner = new SpinnerModel(useSpinner);

            return View(spinner);
        }
    }
}
