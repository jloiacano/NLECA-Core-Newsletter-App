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

        $('.publishAlertButton').click(function () {
            AlertManager.PublishAlert(this);
        });

        $('.unpublishAlertButton').click(function () {
            AlertManager.UnpublishAlert(this);
        });

        $('.deleteAlertButton').click(function () {
            AlertManager.DeleteAlert(this);
        });
    },

    EditAlert: function (alert) {
        var alertId = $(alert).closest('.individualAlert').data('alertid');
        window.location.href = '/Alert/EditAlert/?alertId=' + alertId;
    },

    PublishAlert: function (alert) {
        var alertId = $(alert).closest('.individualAlert').data('alertid');
        window.location.href = '/Alert/PublishAlert/?alertId=' + alertId;
    },

    UnpublishAlert: function (alert) {
        var alertId = $(alert).closest('.individualAlert').data('alertid');
        window.location.href = '/Alert/UnpublishAlert/?alertId=' + alertId;
    },

    DeleteAlert: function (alert) {
        var alertId = $(alert).closest('.individualAlert').data('alertid');
        window.location.href = '/Alert/DeleteAlert/?alertId=' + alertId;
    }

}