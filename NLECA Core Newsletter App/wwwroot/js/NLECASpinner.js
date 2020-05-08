//Heres all the stuff to make the spinner spin (or not as the case may be)
var nlecaSpinner = nlecaSpinner || {};

$(document).ready(function () {
    nlecaSpinner.init();
});


$(window).on('beforeunload', function () {
    nlecaSpinner.unloadSpinner();
});


nlecaSpinner = {
    slowEnoughToDisplaySpinner: false,
    splashScreenDuration: 5000,

    init: function () {
        this.testForSlowConnection();
        this.setHideSplashScreenCookie();
        this.handleSplashScreen();
        this.setUpAjaxCallsFunctionality();
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
        var hideSplashScreenExpiration = new Date().addHours(3);
        var hideSplashScreenCookieValue = encodeURIComponent('Expiration-' + hideSplashScreenExpiration.ConvertToReadableLocalTime());
        document.cookie = 'HideSplashScreen=' + hideSplashScreenCookieValue + '; expires=' + hideSplashScreenExpiration.toUTCString() + '; path=/;';
    },

    handleSplashScreen: function () {
        if ($('#SplashScreenCurrentlyVisible').val() == 'true') {
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