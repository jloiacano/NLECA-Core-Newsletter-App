//Heres all the stuff to make the spinner spin (or not as the case may be)
var nlecaSpinner = nlecaSpinner || {};

$(document).ready(function () {
    nlecaSpinner.init();
    console.log('nlecaSpinner initialized')
});


$(window).on('beforeunload', function () {
    nlecaSpinner.unloadSpinner();
});


nlecaSpinner = {
    slowEnoughToDisplaySpinner: false,
    splashScreenDuration: null,

    init: function () {
        this.testForSlowConnection();
        this.setHideSplashScreenCookie();
        this.setSplashScreenDuration();
        this.handleSplashScreen();
        this.setUpAjaxCallsFunctionality();
    },

    setSplashScreenDuration: function () {
        if ($('#SplashScreenData').data('splashscreenduration')) {
            this.splashScreenDuration = $('#SplashScreenData').data('splashscreenduration');
        }
        else {
            this.splashScreenDuration = 0;
        }

        //when testing set to 15 minutes
        if ($('#SplashScreenData').data('testing')) {
            this.splashScreenDuration = 900000;
        }
    },

    testForSlowConnection: function () {
        var effectiveRoundTripTime = navigator.connection.rtt; //basically measures a ping in milliseconds
        if (effectiveRoundTripTime > 500) {
            this.setUseSpinnerCookie();
        }
        else {
            this.removeUseSpinnerCookie();
        }
    },

    setUseSpinnerCookie: function () {
        var fiveMinutesFromNow = new Date().addMinutes(5);
        var fiveMinuteCookieValue = encodeURIComponent('Expiration-' + fiveMinutesFromNow.ConvertToReadableLocalTime());
        document.cookie = 'UseSpinner=' + fiveMinuteCookieValue + '; expires=' + fiveMinutesFromNow.toUTCString() + '; path=/;';

        this.slowEnoughToDisplaySpinner = true;
    },

    removeUseSpinnerCookie: function () {
        document.cookie = 'UseSpinner=; expires=' + new Date().Zero().toUTCString() + '; path=/;';
    },

    setHideSplashScreenCookie: function () {
        // if we aren't testing the splash screen set the HideSplashScreen cookie
        if ($('#SplashScreenData').data('testing') != true) {
            var hideSplashScreenExpiration = new Date().addHours(3);
            var hideSplashScreenCookieValue = encodeURIComponent('Expiration-' + hideSplashScreenExpiration.ConvertToReadableLocalTime());
            document.cookie = 'HideSplashScreen=' + hideSplashScreenCookieValue + '; expires=' + hideSplashScreenExpiration.toUTCString() + '; path=/;';
        }
        else {
            document.cookie = 'HideSplashScreen=; expires=' + new Date().Zero().toUTCString() + '; path=/;';
        }
    },

    handleSplashScreen: function () {
        if ($('#SplashScreenData').data('showscreen') == true) {
            setTimeout(function () {
                $('#nlecaSpinnerWrapper').fadeOut();
            }, this.splashScreenDuration);
        }
        else {
            if ($('#nlecaSpinnerWrapper').css('display') != 'none') {
                setTimeout(function () {
                    $('#nlecaSpinnerWrapper').fadeOut();
                }, 750);
            }
        }
    },

    unloadSpinner: function () {
        if (this.slowEnoughToDisplaySpinner) {
            $('#nlecaSpinnerWrapper').fadeIn();
        }
    },

    setUpAjaxCallsFunctionality: function () {
        $(document).ajaxSend(function (event, xhr, options) {
            $('#nlecaSpinnerWrapper').fadeIn();
        }).ajaxComplete(function (event, xhr, options) {
            $('#nlecaSpinnerWrapper').fadeOut();
        }).ajaxError(function (event, jqxhr, settings, exception) {
            $('#nlecaSpinnerWrapper').fadeOut();
        });
    }
};