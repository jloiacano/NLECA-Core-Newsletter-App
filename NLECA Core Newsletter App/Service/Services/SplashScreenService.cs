using NLECA_Core_Newsletter_App.Service.Interfaces;

namespace NLECA_Core_Newsletter_App.Service.Services
{
    public class SplashScreenService : ISplashScreenService
    {
        public bool IsABirthday()
        {
            return false;
        }

        public string GetBirthdayLayout()
        {
            return "~/Views/Shared/SplashScreen/_BirthdaySplashScreenPartial.cshtml";
        }

        public bool IsAHoliday()
        {
            return false;
        }

        public string GetHolidayLayout()
        {
            return "~/Views/Shared/SplashScreen/_HolidaySplashScreenPartial.cshtml";
        }
    }
}
