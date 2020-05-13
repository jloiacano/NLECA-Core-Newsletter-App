namespace NLECA_Core_Newsletter_App.Models
{
    public class SplashScreenModel
    {
        public bool ShowScreen { get; set; }
        public string Style { get; set; }
        public int Duration { get; set; }

        public SplashScreenModel(bool showSplashScreen)
        {
            ShowScreen = showSplashScreen;
            Style = "display: block;";
            Duration = 2500;
        }
    }
}
