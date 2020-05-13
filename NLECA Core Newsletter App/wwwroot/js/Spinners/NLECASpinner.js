//Heres all the stuff to make the spinner spin (or not as the case may be)
var nlecaSpinner = nlecaSpinner || {};

document.addEventListener('DOMContentLoaded', function () {
    nlecaSpinner.init();
});


window.addEventListener('beforeunload', function () {
    nlecaSpinner.unloadSpinner();
});


nlecaSpinner = {
    slowEnoughToDisplaySpinner: false,
    testing: false,

    init: function () {
        this.testForSlowConnection();
        this.handleSpinner();
        this.setUpAjaxCallsFunctionality();
        this.setClickOnOff();
        console.log('nlecaSpinner initialized');
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

    handleSpinner: function () {
        if ($('#SpinnerData').data('showspinner') == 'True') {
            setTimeout(function () {
                $('#nlecaSpinnerWrapper').fadeOut();
            }, 500);
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
    },

    setClickOnOff: function () {
        $('#spinnerTester').click(function () {
            $('#nlecaSpinnerWrapper').fadeIn();
            nlecaSpinner.testing = true;
        });

        $('#nlecaSpinnerOverlay').click(function () {
            if (nlecaSpinner.testing) {
                $('#nlecaSpinnerWrapper').fadeOut();
            }
        });
    },
};