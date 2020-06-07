const dateSecond = 1000;
const dateMinute = 60 * dateSecond;
const dateHour = 60 * dateMinute;
const dateDay = 24 * dateHour;
const dateWeek = 7 * dateDay;
const dateMonth = 30 * dateDay;
const dateYear = 365 * dateDay;


Date.prototype.addYears = function (y) {
    this.setTime(this.getTime() + (y * dateYear));
    return this;
};

Date.prototype.addMonths = function (M) {
    this.setTime(this.getTime() + (M * dateMonth));
    return this;
};

Date.prototype.addWeeks = function (w) {
    this.setTime(this.getTime() + (w * dateWeek));
    return this;
};

Date.prototype.addDays = function (d) {
    this.setTime(this.getTime() + (d * dateDay));
    return this;
};

Date.prototype.addHours = function (h) {
    this.setTime(this.getTime() + (h * dateHour));
    return this;
};

Date.prototype.addMinutes = function (m) {
    this.setTime(this.getTime() + (m * dateMinute));
    return this;
};

Date.prototype.addSeconds = function (s) {
    this.setTime(this.getTime() + (s * dateSecond));
    return this;
};

Date.prototype.Min = function () {
    return new Date(-8640000000000000);
};

Date.prototype.Zero = function () {
    return new Date(+0);
};

Date.prototype.Max = function () {
    return new Date(8640000000000000);
};

Date.prototype.ConvertToReadableLocalTime = function () {
    var threeLetterDays = ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'];
    var offset = this.getTimezoneOffset();
    var localOffset = new Date().getTimezoneOffset();
    var localDateTime = this.addMinutes(offset).addMinutes(-localOffset);

    var meridiem = 'AM';
    var ddd = threeLetterDays[localDateTime.getDay()]; // three letter day value (ie Mon)
    var dd = localDateTime.getDate();
    var MM = localDateTime.getMonth() + 1; // thre day month value (ie Dec)
    var yyyy = localDateTime.getFullYear();

    var HH = localDateTime.getHours();
    var mm = makeDoubleDigits(localDateTime.getMinutes());
    var ss = makeDoubleDigits(localDateTime.getSeconds());

    if (HH > 12) {
        HH = HH - 12;
        meridiem = "PM";
    }

    return ''.concat(HH, '-', mm, '-', meridiem, '-on-', ddd, '-', MM, '-', dd, '-', yyyy, '-(', ss + ')');
};

Date.prototype.getDifferenceOfDays = function (subtrahendDate) {
    var difference = Math.abs(this - subtrahendDate);
    return Math.floor(difference / 86400000);
}

// Takes a Date object and turns it into a GMT string (does not adjust time of the Date object)
Date.prototype.getGMTString = function () {
    //ddd, dd MMM yyyy HH':'mm':'ss 'GMT'
    var threeLetterDays = ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'];
    var threeLetterMonths = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];

    var ddd = threeLetterDays[this.getDay()]; // three letter day value (ie Mon)
    var dd = makeDoubleDigits(this.getDate());
    var MMM = threeLetterMonths[this.getMonth()]; // thre day month value (ie Dec)
    var yyyy = this.getFullYear();
    var HH = makeDoubleDigits(this.getHours());
    var mm = makeDoubleDigits(this.getMinutes());
    var ss = makeDoubleDigits(this.getSeconds());

    return ''.concat(ddd, ', ', dd, ' ', MMM, ' ', yyyy, ' ', HH, ':', mm, ':', ss, ' GMT');
};

//Takes a Date object and converts the time (adds or subtracts offset) and then returns the string version
Date.prototype.ConvertToGMTString = function () {
    var offset = this.getTimezoneOffset();
    this.addMinutes(offset);
    var preGmtString = this.toString();
    var gmtIndex = preGmtString.indexOf('GMT-') + 3;
    var gmtStringWithoutComma = preGmtString.substring(0, gmtIndex);
    return ''.concat(gmtStringWithoutComma.slice(0, 3), ',', gmtStringWithoutComma.slice(3));
};

Date.prototype.FromHTMLInputValue = function (htmlInputValue) {
    var tenify = function (i) {
        return (i < 10 ? '0' + i : i);
    };

    if (htmlInputValue.includes("T")) {
        return new Date(htmlInputValue);
    }
    else if (htmlInputValue.includes('-')) {
        htmlInputValue += 'T00:00';
        return new Date(htmlInputValue);
    }
    else if (htmlInputValue.includes(':')) {
        var timeParts = htmlInputValue.split(':');
        var dateToReturn = new Date();
        dateToReturn.setHours(tenify(timeParts[0]));
        dateToReturn.setMinutes(tenify(timeParts[1]));
        return dateToReturn;
    }
    else {
        console.log(htmlInputValue + ' was unable to be converted to Date() in .FromHTMLInputValue()');
    }
};

Date.prototype.toDateHTMLInputValue = function () {
    var date = this,
        tenify = function (i) {
            return (i < 10 ? '0' + i : i);
        },
        YYYY = date.getFullYear(),
        MM = tenify(date.getMonth() + 1),
        DD = tenify(date.getDate())
        ;
    return YYYY + '-' + MM + '-' + DD;
};

Date.prototype.toDatetimeLocalInputValue = function () {
    var date = this,
        tenify = function (i) {
            return (i < 10 ? '0' + i : i);
        },
        YYYY = date.getFullYear(),
        MM = tenify(date.getMonth() + 1),
        DD = tenify(date.getDate()),
        HH = tenify(date.getHours()),
        II = tenify(date.getMinutes()),
        SS = tenify(date.getSeconds())
        ;
    return YYYY + '-' + MM + '-' + DD + 'T' + HH + ':' + II + ':' + SS;
};

Date.prototype.toTimeHTMLInputValue = function () {
    var date = this,
        tenify = function (i) {
            return (i < 10 ? '0' + i : i);
        },
        HH = tenify(date.getHours()),
        II = tenify(date.getMinutes())
        ;
    return HH + ':' + II;
};

Date.prototype.removeOffset = function () {
    var date = this,
    offset = date.getTimezoneOffset();
    var dateWithoutOffset = date.addMinutes(-offset);
    return dateWithoutOffset;
};

Date.prototype.stripTime = function () {
    var date = this;
    date.setHours(0, 0, 0, 0);
    return date;
}


function makeDoubleDigits(digit) {
    var stringDigit = digit.toString();
    var digitToReturn = '0';

    if (stringDigit.length == 1) {
        digitToReturn = digitToReturn.concat(stringDigit);
    }
    else {
        digitToReturn = stringDigit;
    }

    return digitToReturn;
}