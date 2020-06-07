var EventManager = EventManager || {};

$(document).ready(function () {
    EventManager.init();
});

EventManager = {
    init: function () {

        $('#addEventButton').click(function () {
            window.location.href = '/Event/AddEvent/';
        });

        $('.editEventButton').click(function () {
            EventManager.EditEvent(this);
            //var eventId = $(this).closest('.individualEvent').data('eventid');
            //window.location.href = '/Event/EditEvent/?eventId=' + eventId;
        });

        $('.publishEventButton').click(function () {
            EventManager.PublishEvent(this);
            //var eventId = $(this).closest('.individualEvent').data('eventid');
            //window.location.href = '/Event/PublishEvent/?eventId=' + eventId;
        });

        $('.unpublishEventButton').click(function () {
            EventManager.UnpublishEvent(this);
            //var eventId = $(this).closest('.individualEvent').data('eventid');
            //window.location.href = '/Event/UnpublishEvent/?eventId=' + eventId;
        });

        $('.deleteEventButton').click(function () {
            EventManager.DeleteEvent(this);
            //var eventId = $(this).closest('.individualEvent').data('eventid');
            //window.location.href = '/Event/DeleteEvent/?eventId=' + eventId;
        });
    },

    EditEvent: function (event) {
        var eventId = $(event).closest('.individualEvent').data('eventid');
        window.location.href = '/Event/EditEvent/?eventId=' + eventId;
    },

    PublishEvent: function (event) {
        var eventId = $(event).closest('.individualEvent').data('eventid');
        window.location.href = '/Event/PublishEvent/?eventId=' + eventId;
    },

    UnpublishEvent: function (event) {
        var eventId = $(event).closest('.individualEvent').data('eventid');
        window.location.href = '/Event/UnpublishEvent/?eventId=' + eventId;
    },

    DeleteEvent: function (event) {
        var eventId = $(event).closest('.individualEvent').data('eventid');
        window.location.href = '/Event/DeleteEvent/?eventId=' + eventId;
    }

}