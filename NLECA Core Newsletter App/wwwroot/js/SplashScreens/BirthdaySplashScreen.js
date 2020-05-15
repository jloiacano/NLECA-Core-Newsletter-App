var BirthdaySplashScreen = BirthdaySplashScreen || {};

document.addEventListener("DOMContentLoaded", function () {
    BirthdaySplashScreen.init();
});

BirthdaySplashScreen = {

    init: function () {
        $('#closeBirthdaySplashScreenForDay').click(function () {
            // close spash screen and add cookie to not show for the day which expires at midnight
            $('#splashScreenWrapper').fadeOut();

            var hideBirthdaySplashScreenExpiration = new Date().addDays(1);
            var hideSplashScreenCookieValue = encodeURIComponent('Expiration-' + hideBirthdaySplashScreenExpiration.ConvertToReadableLocalTime());
            document.cookie = 'HideBirthdaySplashScreen=' + hideSplashScreenCookieValue + '; expires=' + hideBirthdaySplashScreenExpiration.toUTCString() + '; path=/;';
        });

        document.cookie = 'HideBirthdaySplashScreenForSession=true;expires=0;path=/;';
    }
};