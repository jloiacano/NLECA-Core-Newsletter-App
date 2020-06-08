var AlertManager = AlertManager || {};

$(document).ready(function () {
    AlertManager.init();
});

AlertManager = {

    init: function () {
        console.log('Alert Manager Initialized');

        $('.addAlertButton').click(function () {
            window.location.href = '/Alert/AddAlert/';
        });

        $('.editAlertButton').click(function () {
            AlertManager.EditAlert(this);
        });
    },

    EditAlert: function (alert) {
        var alertId = $(alert).closest('.individualAlert').data('alertid');
        window.location.href = '/Alert/EditAlert/?alertId=' + alertId;
    }

}