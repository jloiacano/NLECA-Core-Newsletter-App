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

    init: function () {
        this.testForSlowConnection();
        this.setThreeHourDelayCookie();
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

    setThreeHourDelayCookie: function () {
        var threeHoursFromNow = new Date().addHours(3);
        var threeHourCookieValue = encodeURIComponent('Expiration-' + threeHoursFromNow.ConvertToReadableLocalTime());
        document.cookie = 'ExpiresInThreeHours=' + threeHourCookieValue + '; expires=' + threeHoursFromNow.toUTCString() + '; path=/;';
    },

    handleSplashScreen: function () {
        if ($('#NotSeenInThreeHours').val() == 'true') {
            setTimeout(function () {
                $('#nlecaSpinnerWrapper').fadeOut();
            }, 5000);
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