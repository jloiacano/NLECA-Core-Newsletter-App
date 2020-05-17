using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLECA_Core_Newsletter_App.Data;
using NLECA_Core_Newsletter_App.Models;
using NLECA_Core_Newsletter_App.Service.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace NLECA_Core_Newsletter_App.ViewComponents
{
    public class SplashScreenViewComponent : ViewComponent
    {
        private readonly IHolidayService _holidayService;

        public SplashScreenViewComponent(IHolidayService holidayService)
        {
            _holidayService = holidayService;
        }

        public IViewComponentResult Invoke()
        {

            // first check if it is the current users birthday and if so use the birthday splash screen
            // because birthdays take priority
            IViewComponentResult BirthdayView = GetBirthdayView();
            if (BirthdayView != null) return BirthdayView;

            // check if it is a holiday and if so use the holiday splash screen
            IViewComponentResult HolidayView = GetHolidayView();
            if (HolidayView != null) return HolidayView;

            // or just show the regular splash screen
            bool showSplashScreen = HttpContext.Request.Cookies["HideSplashScreen"] == null;
            SplashScreenModel splashScreenLoader = new SplashScreenModel(showSplashScreen);

            return View("Default", splashScreenLoader);
            //return View(splashScreenLoader);
        }

        private IViewComponentResult GetBirthdayView()
        {
            IViewComponentResult BirthdayView = null;

            if (User.Identity.IsAuthenticated 
                && HttpContext.Request.Cookies["HideBirthdaySplashScreen"] == null
                && HttpContext.Request.Cookies["HideBirthdaySplashScreenForSession"] != "true")
            {
                var identity = (ClaimsIdentity)User.Identity;
                IEnumerable<Claim> claims = identity.Claims;

                string name = claims.Where(x => x.Type == "ContactName").FirstOrDefault().Value;
                
                BirthdaySplashScreenModel birthdaySplashScreen = new BirthdaySplashScreenModel(true, name);
                BirthdayView = View("BirthdaySplashScreen", birthdaySplashScreen);
            }

            return BirthdayView;
        }

        private IViewComponentResult GetHolidayView()
        {
            IViewComponentResult HoldiayView = null;
            // check if it is a holiday and if so use the holiday splash screen
            if (HttpContext.Request.Cookies["ItsAHoliday"] == "true")
            {   // check cookies since it is less expensive then running the holiday service
                var todaysHoliday = HttpContext.Request.Cookies["TodaysHoliday"];

                // TODO - J - create all the holiday splash screen models and functionality
                SplashScreenModel holidaySplashScreen = new SplashScreenModel(true);
                return View("BirthdaySplashScreen", holidaySplashScreen);
            }
            else
            {
                // check for holidays
                IEnumerable<Holiday> holidays;

                //DateTime dayToCheck = DateTime.Parse("12/12/2020"); // TODO - J - remove once testing is done
                //holidays = _holidayService.GetHolidays(dayToCheck); // TODO - J - remove once testing is done

                holidays = _holidayService.GetHolidays();

                if (holidays.Count() > 0)
                {
                    // TODO - J - create all the holiday splash screen model(s) and functionality
                    SplashScreenModel holidaySplashScreen = new SplashScreenModel(true);
                    HoldiayView = View("HolidaySplashScreen", holidaySplashScreen);
                }
            }
            return HoldiayView;
        }
    }
}