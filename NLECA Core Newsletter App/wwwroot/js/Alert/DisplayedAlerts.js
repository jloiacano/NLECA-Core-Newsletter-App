var DisplayedAlerts = DisplayedAlerts || {};

$(document).ready(function () {
    DisplayedAlerts.init();
});

DisplayedAlerts = {
    init: function () {
        console.log('Displayed Alerts initialized');

        $('.hideAlert').click(function () {
            DisplayedAlerts.HideAlert(this);
        })

        $('.showAlertDetails').click(function () {
            DisplayedAlerts.ShowAlertDetails(this);
        });
    },

    HideAlert: function (hideAlertButton) {
        var currentAlert = $(hideAlertButton).closest('.displayedAlert'),
            alertId = $(currentAlert).data('alertid');
        $(currentAlert).hide();

        $.ajax({
            url: '/Alert/HideAlert',
            type: 'POST',
            data: {
                'alertId': alertId,
            },
            dataType: 'json',
            success: function (response) {
                if (response.success == true) {
                    console.log(response.responseText);
                    var expiration = new Date(response.cookieExpiration).toUTCString();
                    var newCookie = 'HiddenAlerts=' + response.newCookie + '; path=/;  expires=' + expiration + ';';
                    document.cookie = newCookie;
                }
                else {
                    console.log(response.responseText);
                }
            },
            error: function (request, error) {
                alert("Request: " + JSON.stringify(request));
            }
        });
    },

    ShowAlertDetails: function (showDetailsButton) {
        var currentAlert = $(showDetailsButton).closest('.displayedAlert'),
            alertId = $(currentAlert).data('alertid');
        window.location.href = '/Alert/AlertDetails/?alertId=' + alertId;
    }
}