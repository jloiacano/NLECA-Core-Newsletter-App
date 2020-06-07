var EventManager = EventManager || {};

$(document).ready(function () {
    EventManager.init();
});

EventManager = {
    init: function () {

        $('.addEventButton').click(function () {
            window.location.href = '/Event/AddEvent/';
        });

        $('.editEventButton').click(function () {
            EventManager.EditEvent(this);
        });

        $('.publishEventButton').click(function () {
            EventManager.PublishEvent(this);
        });

        $('.unpublishEventButton').click(function () {
            EventManager.UnpublishEvent(this);
        });

        $('.deleteEventButton').click(function () {
            EventManager.DeleteEvent(this);
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