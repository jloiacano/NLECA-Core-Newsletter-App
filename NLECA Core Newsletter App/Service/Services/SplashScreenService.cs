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
            return "";
        }

        public bool IsAHoliday()
        {
            return true;
        }

        public string GetHolidayLayout()
        {
            return " ";
        }
    }
}
