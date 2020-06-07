var EditEvent = EditEvent || {};

$(document).ready(function () {
    EditEvent.init();
});

EditEvent = {
    init: function () {
        EditEvent.ShowCorrectDateTimeInputs();

        $('#SaveEventButton').click(function () {
            EditEvent.SaveEvent();
        });

        $("input[name='eventType']").change(function () {
            EditEvent.ShowCorrectDateTimeInputs();
        });

        EditEvent.SetUpDateTimeValidations();
    },

    ShowCorrectDateTimeInputs: function () {
        var radioValue = $("input[name='eventType']:checked").val();

        if (radioValue == 'TimedEvent') {
            $("#TimedEventDateTimeArea").show();
            $("#AllDayEventDateTimeArea").hide();
            $("#MultiDayEventDateTimeArea").hide();
        }
        else if (radioValue == 'IsAllDayEvent') {
            $("#TimedEventDateTimeArea").hide();
            $("#AllDayEventDateTimeArea").show();
            $("#MultiDayEventDateTimeArea").hide();
        }
        else if (radioValue == 'IsMultiDayEvent') {
            $("#TimedEventDateTimeArea").hide();
            $("#AllDayEventDateTimeArea").hide();
            $("#MultiDayEventDateTimeArea").show();
        }        
    },

    SetUpDateTimeValidations: function () {
        // When a start or end date or time is changed, make sure the other is appropriately less or more.
        $('#TimedEventTime').change(function () {
            var changedTime = new Date().FromHTMLInputValue($(this).val()),
                timeToCheck = new Date().FromHTMLInputValue($('#TimedEventTimeEnd').val());

            if (changedTime > timeToCheck.addMinutes(-15)) {
                var newTimedEventTimeEnd = changedTime.addMinutes(15).toTimeHTMLInputValue();
                $('#TimedEventTimeEnd').val(newTimedEventTimeEnd)
            }
        });

        $('#TimedEventTimeEnd').change(function () {
            var changedTime = new Date().FromHTMLInputValue($(this).val()),
                timeToCheck = new Date().FromHTMLInputValue($('#TimedEventTime').val());

            if (changedTime < timeToCheck.addMinutes(15)) {
                var newTimedEventTime = changedTime.addMinutes(-15).toTimeHTMLInputValue();
                $('#TimedEventTime').val(newTimedEventTime)
            }
        });

        $('#MultiDayEventDate').change(function () {
            var changedDateTime = new Date().FromHTMLInputValue($(this).val()),
                dateTimeToCheck = new Date().FromHTMLInputValue($('#MultiDayEventDateEnd').val()),
                comparableChangedDate = new Date(changedDateTime).stripTime(),
                comparableDateToCheck = new Date(dateTimeToCheck).stripTime();

            if (comparableChangedDate >= comparableDateToCheck) {
                var differenceOfDays = changedDateTime.getDifferenceOfDays(dateTimeToCheck);
                var newEventDateEnd = dateTimeToCheck.addDays(differenceOfDays + 1).toDatetimeLocalInputValue();
                $('#MultiDayEventDateEnd').val(newEventDateEnd);
            }
        });

        $('#MultiDayEventDateEnd').change(function () {
            var changedDateTime = new Date().FromHTMLInputValue($(this).val()),
                dateTimeToCheck = new Date().FromHTMLInputValue($('#MultiDayEventDate').val()),
                comparableChangedDate = new Date(changedDateTime).stripTime(),
                comparableDateToCheck = new Date(dateTimeToCheck).stripTime();

            if (comparableChangedDate <= comparableDateToCheck) {
                var differenceOfDays = changedDateTime.getDifferenceOfDays(dateTimeToCheck);
                var newEventDate = dateTimeToCheck.addDays(-differenceOfDays - 1).toDatetimeLocalInputValue();
                $('#MultiDayEventDate').val(newEventDate);
            }
        });
    },

    SaveEvent: function () {
        var radioValue = $("input[name='eventType']:checked").val();

        if (radioValue == 'TimedEvent') {
            $('#IsAllDayEvent').val(false);
            $('#IsMultiDayEvent').val(false);

            var startDate = GetCorrectDateFormat($('#TimedEventDate').val(), $('#TimedEventTime').val());
            var endDate = GetCorrectDateFormat($('#TimedEventDate').val(), $('#TimedEventTimeEnd').val());
            $('#EventDate').val(startDate);
            $('#EventDateEnd').val(endDate);

            function GetCorrectDateFormat(date, time) {
                var datetime = new Date(date + 'T' + time);
                return EditEvent.AdjustDateTimeForInput(datetime);
            }
        }
        else if (radioValue == 'IsAllDayEvent') {
            $('#IsAllDayEvent').val(true);
            $('#IsMultiDayEvent').val(false);

            $('#EventDate').val($('#AllDayEventDate').val());
            $('#EventDateEnd').val($('#AllDayEventDate').val());
        }
        else if (radioValue == 'IsMultiDayEvent') {
            $('#IsAllDayEvent').val(false);
            $('#IsMultiDayEvent').val(true);

            var startDate = EditEvent.AdjustDateTimeForInput(new Date($('#MultiDayEventDate').val()));
            var endDate = EditEvent.AdjustDateTimeForInput(new Date($('#MultiDayEventDateEnd').val()));

            $('#EventDate').val(startDate);
            $('#EventDateEnd').val(endDate);

            function GetCorrectDateFormat(incorrectFormat) {
                var datetime = new Date(incorrectFormat);
                return EditEvent.AdjustDateTimeForInput(datetime);
            }
        }

        $('#EventForm').submit();
    },

    AdjustDateTimeForInput: function (datetime) {
        var adjustedDateTime = datetime.removeOffset().toISOString().slice(0, -8);
        return adjustedDateTime;
    }
};