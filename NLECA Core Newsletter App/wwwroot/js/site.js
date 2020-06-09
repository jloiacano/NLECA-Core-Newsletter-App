var Site = Site || {};

$(document).ready(function () {
    Site.init();
});

Site = {
    init: function () {
        console.log('Site initialized');

        $('.undoHideAlertsButton').click(function () {
            Site.UndoHideAlerts();
        })
    },

    UndoHideAlerts: function () {

        var expiration = new Date().addDays(-1).toUTCString();
        var expiredHiddenAlertsCookie = 'HiddenAlerts=; path=/;  expires=' + expiration + ';';
        document.cookie = expiredHiddenAlertsCookie;
        window.location.reload(true); 
    }
}
