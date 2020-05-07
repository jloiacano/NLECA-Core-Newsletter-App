var millisecondsSinceBeforeUnload = 0;

$(document).ready(function () {

    // Delay fade for site not having been seen in three hours
    if ($('#NotSeenInThreeHours').val() == "true") {
        var expiration = new Date().addHours(3);
        document.cookie = 'ExpiresInThreeHours=Expires: ' + expiration.ConvertToReadableLocalTime() + '; expires=' + expiration.toUTCString() + ';';;
        setTimeout(function () {
            $('#nlecaSpinnerWrapper').fadeOut();
        }, 5000);
    }
    else {
        setTimeout(function () {
            $('#nlecaSpinnerWrapper').fadeOut();
        }, 550);
    }
});

$(window).on('beforeunload', function () {
    setInterval(function () {
        if (millisecondsSinceBeforeUnload > 150) {
            $('#nlecaSpinnerWrapper').fadeIn();
        }
        else {
            millisecondsSinceBeforeUnload += 50;
        }
    }, 50);
});

$(window).on('unload', function () {
    console.log("unload");
    this.incrementSinceBeforeUnload = 0;
});

// Handles the nlecaSpinner when ajax calls occur
$(document).ajaxSend(function (event, xhr, options) {
    $('#nlecaSpinnerWrapper').fadeIn();
}).ajaxComplete(function (event, xhr, options) {
    $('#nlecaSpinnerWrapper').fadeOut();
}).ajaxError(function (event, jqxhr, settings, exception) {
    $('#nlecaSpinnerWrapper').fadeOut();
});