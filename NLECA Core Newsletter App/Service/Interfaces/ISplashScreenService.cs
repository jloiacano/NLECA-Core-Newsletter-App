namespace NLECA_Core_Newsletter_App.Service.Interfaces
{
    public interface ISplashScreenService
    {
        bool IsABirthday();
        string GetBirthdayLayout();
        bool IsAHoliday();
        string GetHolidayLayout();
    }
}
