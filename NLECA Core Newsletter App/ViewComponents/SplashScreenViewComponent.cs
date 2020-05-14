using Microsoft.AspNetCore.Mvc;
using NLECA_Core_Newsletter_App.Data;
using NLECA_Core_Newsletter_App.Models;
using NLECA_Core_Newsletter_App.Service.Interfaces;
using System.Collections.Generic;
using System.Linq;

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
            if (HttpContext.Request.Cookies["ItsYourBirthday"] == "true")
            {
                // TODO - J - create a birthday splash screen model and functionality
                SplashScreenModel birthdaySplashScreen = new SplashScreenModel(true);
                return View("BirthdaySplashScreen", birthdaySplashScreen);
            }


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
                    return View("HolidaySplashScreen", holidaySplashScreen);
                }
            }
            

            // or just show the regular splash screen
            bool showSplashScreen = HttpContext.Request.Cookies["HideSplashScreen"] == null;
            SplashScreenModel splashScreenLoader = new SplashScreenModel(showSplashScreen);

            return View("Default", splashScreenLoader);
            //return View(splashScreenLoader);
        }
    }
}