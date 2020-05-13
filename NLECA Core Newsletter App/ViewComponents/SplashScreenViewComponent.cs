using Microsoft.AspNetCore.Mvc;
using NLECA_Core_Newsletter_App.Models;

namespace NLECA_Core_Newsletter_App.ViewComponents
{
    public class SplashScreenViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            bool showSplashScreen = HttpContext.Request.Cookies["HideSplashScreen"] == null;
            SplashScreenModel splashScreenLoader = new SplashScreenModel(showSplashScreen);

            return View(splashScreenLoader);
        }
    }
}