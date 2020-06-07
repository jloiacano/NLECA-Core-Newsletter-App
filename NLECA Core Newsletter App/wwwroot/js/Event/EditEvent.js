var EventManager = EventManager || {};

$(document).ready(function () {
    EventManager.init();
});

EventManager = {
    init: function () {
        EventManager.ShowCorrectDateTimeInputs();

        $('#SaveEventButton').click(function () {
            EventManager.SaveEvent();
        });

        $("input[name='eventType']").change(function () {
            EventManager.ShowCorrectDateTimeInputs();
        });

        EventManager.SetUpDateTimeValidations();
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
            var changedTime = $(this).val(),
            timeToCheck = $('#TimedEventTimeEnd').val();

            if (GetComparableTime(changedTime, 0) > GetComparableTime(timeToCheck, -15)) {
                var newTimedEventTimeEnd = GetNewTimedEventTime(changedTime , 15);
                $('#TimedEventTimeEnd').val(newTimedEventTimeEnd)
            }
        });

        $('#TimedEventTimeEnd').change(function () {
            var changedTime = $(this).val(),
                timeToCheck = $('#TimedEventTime').val();

            if (GetComparableTime(changedTime, 0) < GetComparableTime(timeToCheck, 15)) {
                var newTimedEventTime = GetNewTimedEventTime(changedTime, -15);
                $('#TimedEventTime').val(newTimedEventTime)
            }
        });

        function GetComparableTime(time, difference) {
            var timeSplit = time.split(':');
            var dateToUse = new Date();
            dateToUse.setHours(timeSplit[0]);
            dateToUse.setMinutes(timeSplit[1]);
            dateToUse.addMinutes(difference);
            return dateToUse.toTimeHoursAndMinutes();
        }

        function GetNewTimedEventTime(time, change) {
            var splitTime = time.split(':'),
                dateTimeValue = new Date();
            dateTimeValue.setHours(splitTime[0]);
            dateTimeValue.setMinutes(splitTime[1]);
            dateTimeValue.addMinutes(change);
            return (dateTimeValue.getHours() < 10 ? '0' : '') + dateTimeValue.getHours() + ':'
                + (dateTimeValue.getMinutes() < 10 ? '0' : '') + dateTimeValue.getMinutes();
        };

        $('#MultiDayEventDate').change(function () {
            var changedDateTime = $(this).val()
            dateTimeToCheck = $('#MultiDayEventDateEnd').val();

            if (GetComparableDate(changedDateTime) >= GetComparableDate(dateTimeToCheck)) {
                var differenceOfDays = GetDifferenceOfDays(changedDateTime, dateTimeToCheck);
                var newEventDateEnd = GetNewEventDate(dateTimeToCheck, differenceOfDays + 1);
                $('#MultiDayEventDateEnd').val(newEventDateEnd);
            }
        });

        $('#MultiDayEventDateEnd').change(function () {
            var changedDateTime = $(this).val()
            dateTimeToCheck = $('#MultiDayEventDate').val();

            if (GetComparableDate(changedDateTime) <= GetComparableDate(dateTimeToCheck)) {
                var differenceOfDays = GetDifferenceOfDays(changedDateTime, dateTimeToCheck);
                var newEventDate = GetNewEventDate(dateTimeToCheck, -differenceOfDays - 1);
                $('#MultiDayEventDate').val(newEventDate);
            }
        });

        function GetComparableDate(date) {
            var comparable = new Date(date);
            comparable.setHours(0, 0, 0, 0);
            return comparable;
        };

        function GetDifferenceOfDays(date1, date2) {
            var firstDate = new Date(date1).getTime(),
                secondDate = new Date(date2).getTime();

            var difference = Math.abs(firstDate - secondDate);
            return Math.floor(difference / 86400000);
        }

        function GetNewEventDate(date, change) {
            var newDate = new Date(date).addDays(change);
            return newDate.removeOffset().toISOString().slice(0, -8);
        };
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
                return EventManager.AdjustDateTimeForInput(datetime);
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

            var startDate = EventManager.AdjustDateTimeForInput(new Date($('#MultiDayEventDate').val()));
            var endDate = EventManager.AdjustDateTimeForInput(new Date($('#MultiDayEventDateEnd').val()));

            $('#EventDate').val(startDate);
            $('#EventDateEnd').val(endDate);

            function GetCorrectDateFormat(incorrectFormat) {
                var datetime = new Date(incorrectFormat);
                return EventManager.AdjustDateTimeForInput(datetime);
            }
        }

        $('#EventForm').submit();
    },

    AdjustDateTimeForInput: function (datetime) {
        var adjustedDateTime = datetime.removeOffset().toISOString().slice(0, -8);
        return adjustedDateTime;
    }
};