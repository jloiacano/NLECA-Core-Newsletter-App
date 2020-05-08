Date.prototype.addHours = function (h) {
    this.setTime(this.getTime() + (h * 3600000));
    return this;
}

Date.prototype.addMinutes = function (m) {
    this.setTime(this.getTime() + (m * 60000));
    return this;
}

Date.prototype.addSeconds = function (s) {
    this.setTime(this.getTime() + (s * 1000));
    return this;
}

Date.prototype.Min = function () {
    return new Date(-8640000000000000);
}

Date.prototype.Zero = function () {
    return new Date(+0);
}

Date.prototype.Max = function () {
    return new Date(8640000000000000);
}

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
}

//Takes a Date object and converts the time (adds or subtracts offset) and then returns the string version
Date.prototype.ConvertToGMTString = function () {
    var offset = this.getTimezoneOffset();
    this.addMinutes(offset);
    var preGmtString = this.toString();
    var gmtIndex = preGmtString.indexOf('GMT-') + 3;
    var gmtStringWithoutComma = preGmtString.substring(0, gmtIndex);
    return ''.concat(gmtStringWithoutComma.slice(0, 3), ',', gmtStringWithoutComma.slice(3));
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