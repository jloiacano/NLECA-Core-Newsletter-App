var SplashScreen = SplashScreen || {};

document.addEventListener("DOMContentLoaded", function () {
    SplashScreen.init();
});

SplashScreen = {

    splashScreenDuration: 0,


    init: function () {
        this.setSplashScreenDuration();
        this.setHideSplashScreenCookie();
        this.handleSplashScreen();
        this.setClickOnOff();
        console.log('Splash screen initialized');
    },

    setSplashScreenDuration: function () {
        if ($('#SplashScreenData').data('splashscreenduration')) {
            this.splashScreenDuration = $('#SplashScreenData').data('splashscreenduration');
        }
        else {
            this.splashScreenDuration = 0;
        }
    },

    setHideSplashScreenCookie: function () {
        var hideSplashScreenExpiration = new Date().addHours(3);
        var hideSplashScreenCookieValue = encodeURIComponent('Expiration-' + hideSplashScreenExpiration.ConvertToReadableLocalTime());
        document.cookie = 'HideSplashScreen=' + hideSplashScreenCookieValue + '; expires=' + hideSplashScreenExpiration.toUTCString() + '; path=/;';
    },

    handleSplashScreen: function () {
        if ($('#SplashScreenData').data('showscreen') == "True") {
            setTimeout(function () {
                $('#splashScreenWrapper').fadeOut();
            }, this.splashScreenDuration);
        }
    },

    setClickOnOff: function () {
        $('#splashScreenTester').click(function () {
            $('#splashScreenWrapper').fadeIn();
        });

        $('#splashScreenOverlay').click(function () {
            $('#splashScreenWrapper').fadeOut();
        });
    },
};