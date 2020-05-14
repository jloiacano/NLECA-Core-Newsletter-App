using CoordinateSharp;
using NLECA_Core_Newsletter_App.Data;
using NLECA_Core_Newsletter_App.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NLECA_Core_Newsletter_App.Service.Services
{
    public class HolidayService : IHolidayService
    {
        public DateTime Today { get; set; }

        public HolidayService() { Today = DateTime.Now; }

        #region // parameterless methods

        public bool IsHoliday() { return IsHoliday(Today); }
        public IEnumerable<Holiday> GetHolidays() { return GetHolidays(Today); }
        public bool IsNewYearsDay() { return IsNewYearsDay(Today); }
        public bool IsMartinLutherKingJrDay() { return IsMartinLutherKingJrDay(Today); }
        public bool IsGroundHogDay() { return IsGroundHogDay(Today); }
        public bool IsLincolnsBirthday() { return IsLincolnsBirthday(Today); }
        public bool IsValentinesDay() { return IsValentinesDay(Today); }
        public bool IsWashingtonsBirthdayPresidentsDay() { return IsWashingtonsBirthdayPresidentsDay(Today); }
        public bool IsDaylightSavingsStart() { return IsDaylightSavingsStart(Today); }
        public bool IsStPatricksDay() { return IsStPatricksDay(Today); }
        public bool IsAprilFoolsDay() { return IsAprilFoolsDay(Today); }
        public bool IsPassover() { return IsPassover(Today); }
        public bool IsGoodFriday() { return IsGoodFriday(Today); }
        public bool IsEaster() { return IsEaster(Today); }
        public bool IsEarthDay() { return IsEarthDay(Today); }
        public bool IsCincoDeMayo() { return IsCincoDeMayo(Today); }
        public bool IsMothersDay() { return IsMothersDay(Today); }
        public bool IsMemorialDay() { return IsMemorialDay(Today); }
        public bool IsFlagDay() { return IsFlagDay(Today); }
        public bool IsFathersDay() { return IsFathersDay(Today); }
        public bool IsIndependenceDay() { return IsIndependenceDay(Today); }
        public bool IsLaborDay() { return IsLaborDay(Today); }
        public bool IsRoshHashanah() { return IsRoshHashanah(Today); }
        public bool IsSweetestDay() { return IsSweetestDay(Today); }
        public bool IsHalloween() { return IsHalloween(Today); }
        public bool IsDaylightSavingsEnd() { return IsDaylightSavingsEnd(Today); }
        public bool IsVeteransDay() { return IsVeteransDay(Today); }
        public bool IsThanksgivingDay() { return IsThanksgivingDay(Today); }
        public bool IsHanukkahDay1() { return IsHanukkahDay1(Today); }
        public bool IsHanukkahDay2() { return IsHanukkahDay2(Today); }
        public bool IsHanukkahDay3() { return IsHanukkahDay3(Today); }
        public bool IsHanukkahDay4() { return IsHanukkahDay4(Today); }
        public bool IsHanukkahDay5() { return IsHanukkahDay5(Today); }
        public bool IsHanukkahDay6() { return IsHanukkahDay6(Today); }
        public bool IsHanukkahDay7() { return IsHanukkahDay7(Today); }
        public bool IsHanukkahDay8() { return IsHanukkahDay8(Today); }
        public bool IsChristmasEve() { return IsChristmasEve(Today); }
        public bool IsChristmasDay() { return IsChristmasDay(Today); }
        public bool IsNewYearsEve() { return IsNewYearsEve(Today); }

        #endregion

        public bool IsHoliday(DateTime dateTime)
        {
            return
                IsNewYearsDay(dateTime)
                || IsMartinLutherKingJrDay(dateTime)
                || IsGroundHogDay(dateTime)
                || IsLincolnsBirthday(dateTime)
                || IsValentinesDay(dateTime)
                || IsWashingtonsBirthdayPresidentsDay(dateTime)
                || IsDaylightSavingsStart(dateTime)
                || IsStPatricksDay(dateTime)
                || IsAprilFoolsDay(dateTime)
                || IsPassover(dateTime)
                || IsGoodFriday(dateTime)
                || IsEaster(dateTime)
                || IsEarthDay(dateTime)
                || IsCincoDeMayo(dateTime)
                || IsMothersDay(dateTime)
                || IsMemorialDay(dateTime)
                || IsFlagDay(dateTime)
                || IsFathersDay(dateTime)
                || IsIndependenceDay(dateTime)
                || IsLaborDay(dateTime)
                || IsRoshHashanah(dateTime)
                || IsSweetestDay(dateTime)
                || IsHalloween(dateTime)
                || IsDaylightSavingsEnd(dateTime)
                || IsVeteransDay(dateTime)
                || IsThanksgivingDay(dateTime)
                || IsHanukkahDay1(dateTime)
                || IsHanukkahDay2(dateTime)
                || IsHanukkahDay3(dateTime)
                || IsHanukkahDay4(dateTime)
                || IsHanukkahDay5(dateTime)
                || IsHanukkahDay6(dateTime)
                || IsHanukkahDay7(dateTime)
                || IsHanukkahDay8(dateTime)
                || IsChristmasEve(dateTime)
                || IsChristmasDay(dateTime)
                || IsNewYearsEve(dateTime);
        }

        public IEnumerable<Holiday> GetHolidays(DateTime dateTime)
        {
            List<Holiday> holidays = new List<Holiday>();

            if (IsNewYearsDay(dateTime)) { holidays.Add(Holiday.NewYearsDay); } //NewYearsDay,
            if (IsMartinLutherKingJrDay(dateTime)) { holidays.Add(Holiday.MartinLutherKingJrDay); } //MartinLutherKingJrDay,
            if (IsGroundHogDay(dateTime)) { holidays.Add(Holiday.GroundHogDay); } //GroundHogDay,
            if (IsLincolnsBirthday(dateTime)) { holidays.Add(Holiday.LincolnsBirthday); } //LincolnsBirthday,
            if (IsValentinesDay(dateTime)) { holidays.Add(Holiday.ValentinesDay); } //ValentinesDay,
            if (IsWashingtonsBirthdayPresidentsDay()) { holidays.Add(Holiday.WashingtonsBirthdayAndPresidentsDay); } //WashingtonsBirthdayAndPresidentsDay,
            if (IsDaylightSavingsStart(dateTime)) { holidays.Add(Holiday.DaylightSavingsStart); } //DaylightSavingsStart,
            if (IsStPatricksDay(dateTime)) { holidays.Add(Holiday.StPatricksDay); } //StPatricksDay,
            if (IsAprilFoolsDay(dateTime)) { holidays.Add(Holiday.AprilFoolsDay); } //AprilFoolsDay,
            if (IsPassover(dateTime)) { holidays.Add(Holiday.Passover); } //Passover,
            if (IsGoodFriday(dateTime)) { holidays.Add(Holiday.GoodFriday); } //GoodFriday,
            if (IsEaster(dateTime)) { holidays.Add(Holiday.Easter); } //Easter,
            if (IsEarthDay(dateTime)) { holidays.Add(Holiday.EarthDay); } //EarthDay,
            if (IsCincoDeMayo(dateTime)) { holidays.Add(Holiday.CincoDeMayo); } //CincoDeMayo,
            if (IsMothersDay(dateTime)) { holidays.Add(Holiday.MothersDay); } //MothersDay,
            if (IsMemorialDay(dateTime)) { holidays.Add(Holiday.MemorialDay); } //MemorialDay,
            if (IsFlagDay(dateTime)) { holidays.Add(Holiday.FlagDay); } //FlagDay,
            if (IsFathersDay(dateTime)) { holidays.Add(Holiday.FathersDay); } //FathersDay,
            if (IsIndependenceDay(dateTime)) { holidays.Add(Holiday.IndependenceDay); } //IndependenceDay,
            if (IsLaborDay(dateTime)) { holidays.Add(Holiday.LaborDay); } //LaborDay,
            if (IsRoshHashanah(dateTime)) { holidays.Add(Holiday.RoshHashanah); } //RoshHashanah,
            if (IsSweetestDay(dateTime)) { holidays.Add(Holiday.SweetestDay); } //SweetestDay,
            if (IsHalloween(dateTime)) { holidays.Add(Holiday.Halloween); } //Halloween,
            if (IsDaylightSavingsEnd(dateTime)) { holidays.Add(Holiday.DaylightSavingsEnd); } //DaylightSavingsEnd,
            if (IsVeteransDay(dateTime)) { holidays.Add(Holiday.VeteransDay); } //VeteransDay,
            if (IsThanksgivingDay(dateTime)) { holidays.Add(Holiday.ThanksGiving); } //ThanksGiving,
            if (IsHanukkahDay1(dateTime)) { holidays.Add(Holiday.HanukkahDay1); } //HanukkahDay1,
            if (IsHanukkahDay2(dateTime)) { holidays.Add(Holiday.HanukkahDay2); } //HanukkahDay2,
            if (IsHanukkahDay3(dateTime)) { holidays.Add(Holiday.HanukkahDay3); } //HanukkahDay3,
            if (IsHanukkahDay4(dateTime)) { holidays.Add(Holiday.HanukkahDay4); } //HanukkahDay4,
            if (IsHanukkahDay5(dateTime)) { holidays.Add(Holiday.HanukkahDay5); } //HanukkahDay5,
            if (IsHanukkahDay6(dateTime)) { holidays.Add(Holiday.HanukkahDay6); } //HanukkahDay6,
            if (IsHanukkahDay7(dateTime)) { holidays.Add(Holiday.HanukkahDay7); } //HanukkahDay7,
            if (IsHanukkahDay8(dateTime)) { holidays.Add(Holiday.HanukkahDay8); } //HanukkahDay8,
            if (IsChristmasEve(dateTime)) { holidays.Add(Holiday.ChristmasEve); } //ChristmasEve,
            if (IsChristmasDay(dateTime)) { holidays.Add(Holiday.ChristmasDay); } //ChristmasDay,
            if (IsNewYearsEve(dateTime)) { holidays.Add(Holiday.NewYearsEve); } //NewYearsEve
            return holidays.AsEnumerable();
        }

        public bool IsNewYearsDay(DateTime dateTime)
        {
            return (dateTime.Month == 1 && dateTime.Day == 1);
        }

        public bool IsMartinLutherKingJrDay(DateTime dateTime)
        { // Third Monday in January
            var thirdMondayInJanuary = (from day in Enumerable.Range(1, 31)
                                where new DateTime(dateTime.Year, 1, day).DayOfWeek == DayOfWeek.Monday
                                select day).ElementAt(2);
            DateTime martinLutherKingJrDay = new DateTime(dateTime.Year, 1, thirdMondayInJanuary);
            return dateTime.DayOfYear == martinLutherKingJrDay.DayOfYear;
        }

        public bool IsGroundHogDay(DateTime dateTime)
        {
            return (dateTime.Month == 2 && dateTime.Day == 2);
        }


        public bool IsLincolnsBirthday(DateTime dateTime)
        {
            return (dateTime.Month == 2 && dateTime.Day == 12);
        }

        public bool IsValentinesDay(DateTime dateTime)
        {
            return (dateTime.Month == 2 && dateTime.Day == 14);
        }

        public bool IsWashingtonsBirthdayPresidentsDay(DateTime dateTime)
        { // Third Monday in February
            var thirdMondayInFebruary = (from day in Enumerable.Range(1, 29)
                                        where new DateTime(dateTime.Year, 2, day).DayOfWeek == DayOfWeek.Monday
                                        select day).ElementAt(2);
            DateTime presidentsDay = new DateTime(dateTime.Year, 2, thirdMondayInFebruary);
            return dateTime.DayOfYear == presidentsDay.DayOfYear;
        }

        public bool IsDaylightSavingsStart(DateTime dateTime)
        { // Second Sunday in March
            var secondSundayInMarch = (from day in Enumerable.Range(1, 31)
                                        where new DateTime(dateTime.Year, 3, day).DayOfWeek == DayOfWeek.Sunday
                                        select day).ElementAt(1);
            DateTime daylightSavingsStart = new DateTime(dateTime.Year, 1, secondSundayInMarch);
            return dateTime.DayOfYear == daylightSavingsStart.DayOfYear;
        }

        public bool IsStPatricksDay(DateTime dateTime)
        {
            return (dateTime.Month == 3 && dateTime.Day == 17);
        }

        public bool IsAprilFoolsDay(DateTime dateTime)
        {
            return (dateTime.Month == 4 && dateTime.Day == 1);
        }

        public bool IsPassover(DateTime dateTime)
        {
            string[] PassoverDates =
            {
                "04/09/2020", "04/10/2020", "04/11/2020", "04/12/2020", "04/13/2020", "04/14/2020", "04/15/2020", "04/16/2020", // passover days for 2020
                "03/28/2021", "03/29/2021", "03/30/2021", "03/31/2021", "04/01/2021", "04/02/2021", "04/03/2021", "04/04/2021", // passover days for 2021
                "04/16/2022", "04/17/2022", "04/18/2022", "04/19/2022", "04/20/2022", "04/21/2022", "04/22/2022", "04/23/2022", // passover days for 2022
                "04/06/2023", "04/07/2023", "04/08/2023", "04/09/2023", "04/10/2023", "04/11/2023", "04/12/2023", "04/13/2023", // passover days for 2023
                "04/23/2024", "04/24/2024", "04/25/2024", "04/26/2024", "04/27/2024", "04/28/2024", "04/29/2024", "04/30/2024", // passover days for 2024
                "04/13/2025", "04/14/2025", "04/15/2025", "04/16/2025", "04/17/2025", "04/18/2025", "04/19/2025", "04/20/2025", // passover days for 2025
                "04/02/2026", "04/03/2026", "04/04/2026", "04/05/2026", "04/06/2026", "04/07/2026", "04/08/2026", "04/09/2026", // passover days for 2026
                "04/22/2027", "04/23/2027", "04/24/2027", "04/25/2027", "04/26/2027", "04/27/2027", "04/28/2027", "04/29/2027", // passover days for 2027
                "04/02/2028", "04/03/2028", "04/04/2028", "04/05/2028", "04/06/2028", "04/07/2028", "04/08/2028", "04/09/2028", // passover days for 2028
                "04/02/2029",
                "04/02/2030",
            };

            return CheckIfDayIsInListOfSolDays(dateTime, PassoverDates);
        }

        public bool IsGoodFriday(DateTime dateTime)
        {
            return FindEasterSunday(dateTime.Year).DayOfYear - 2 == dateTime.DayOfYear;
        }

        public bool IsEaster(DateTime dateTime)
        {
            return FindEasterSunday(dateTime.Year).DayOfYear == dateTime.DayOfYear;
        }

        public bool IsEarthDay(DateTime dateTime)
        {
            return (dateTime.Month == 4 && dateTime.Day == 22);
        }

        public bool IsCincoDeMayo(DateTime dateTime)
        {
            return (dateTime.Month == 5 && dateTime.Day == 5);
        }

        public bool IsMothersDay(DateTime dateTime)
        { // Second Sunday in May
            var secondSundayInMay = (from day in Enumerable.Range(1, 31)
                                       where new DateTime(dateTime.Year, 5, day).DayOfWeek == DayOfWeek.Sunday
                                       select day).ElementAt(1);
            DateTime mothersDay = new DateTime(dateTime.Year, 1, secondSundayInMay);
            return dateTime.DayOfYear == mothersDay.DayOfYear;
        }

        public bool IsMemorialDay(DateTime dateTime)
        { // Last Monday in May
            DateTime memorialDay = new DateTime(dateTime.Year, 5, 31);
            DayOfWeek dayOfWeek = memorialDay.DayOfWeek;
            while (dayOfWeek != DayOfWeek.Monday)
            {
                memorialDay = memorialDay.AddDays(-1);
                dayOfWeek = memorialDay.DayOfWeek;
            }
            return dateTime.DayOfYear == memorialDay.DayOfYear;
        }

        public bool IsFlagDay(DateTime dateTime)
        {
            return (dateTime.Month == 6 && dateTime.Day == 14);
        }

        public bool IsFathersDay(DateTime dateTime)
        { // Third Sunday in June
            var thirdSundayInJune = (from day in Enumerable.Range(1, 30)
                                     where new DateTime(dateTime.Year, 6, day).DayOfWeek == DayOfWeek.Sunday
                                     select day).ElementAt(2);
            DateTime fathersDay = new DateTime(dateTime.Year, 1, thirdSundayInJune);
            return dateTime.DayOfYear == fathersDay.DayOfYear;
        }

        public bool IsIndependenceDay(DateTime dateTime)
        {
            return (dateTime.Month == 7 && dateTime.Day == 4);
        }

        public bool IsLaborDay(DateTime dateTime)
        { // First Monday in September
            DateTime laborDay = new DateTime(dateTime.Year, 9, 1);
            DayOfWeek dayOfWeek = laborDay.DayOfWeek;
            while (dayOfWeek != DayOfWeek.Monday)
            {
                laborDay = laborDay.AddDays(1);
                dayOfWeek = laborDay.DayOfWeek;
            }
            return dateTime.DayOfYear == laborDay.DayOfYear;
        }

        public bool IsRoshHashanah(DateTime dateTime)
        {
            string[] RoshHashanahDates =
            {
                "09/30/2020",
                "10/01/2020",

                "09/07/2021",
                "09/08/2021",

                "09/26/2022",
                "09/27/2022",

                "09/16/2023",
                "09/17/2023",

                "10/03/2024",
                "10/04/2024",

                "09/23/2025",
                "09/24/2025"
            };

            return CheckIfDayIsInListOfSolDays(dateTime, RoshHashanahDates);
        }

        public bool IsSweetestDay(DateTime dateTime)
        { // Third Saturday in October
            var thirdSundayInOctober = (from day in Enumerable.Range(1, 31)
                                     where new DateTime(dateTime.Year, 10, day).DayOfWeek == DayOfWeek.Sunday
                                     select day).ElementAt(2);
            DateTime halloween = new DateTime(dateTime.Year, 1, thirdSundayInOctober);
            return dateTime.DayOfYear == halloween.DayOfYear;
        }

        public bool IsHalloween(DateTime dateTime)
        {
            return (dateTime.Month == 10 && dateTime.Day == 31);
        }

        public bool IsDaylightSavingsEnd(DateTime dateTime)
        { // First Sunday in November
            DateTime daylightSavingsEnd = new DateTime(dateTime.Year, 11, 1);
            DayOfWeek dayOfWeek = daylightSavingsEnd.DayOfWeek;
            while (dayOfWeek != DayOfWeek.Sunday)
            {
                daylightSavingsEnd = daylightSavingsEnd.AddDays(1);
                dayOfWeek = daylightSavingsEnd.DayOfWeek;
            }
            return dateTime.DayOfYear == daylightSavingsEnd.DayOfYear;
        }

        public bool IsVeteransDay(DateTime dateTime)
        {
            return (dateTime.Month == 11 && dateTime.Day == 11);
        }

        public bool IsThanksgivingDay(DateTime dateTime)
        { // 4th Thursday in November
            var thanksgiving = (from day in Enumerable.Range(1, 30)
                                where new DateTime(dateTime.Year, 11, day).DayOfWeek == DayOfWeek.Thursday
                                select day).ElementAt(3);
            DateTime thanksgivingDay = new DateTime(dateTime.Year, 11, thanksgiving);
            return dateTime.DayOfYear == thanksgivingDay.DayOfYear;
        }

        public bool IsDayAfterThanksgiving(DateTime dateTime)
        { // Day after Thanksgiving
            var thanksgiving = (from day in Enumerable.Range(1, 30)
                                where new DateTime(dateTime.Year, 11, day).DayOfWeek == DayOfWeek.Thursday
                                select day).ElementAt(3);
            DateTime thanksgivingDay = new DateTime(dateTime.Year, 11, thanksgiving + 1);
            return dateTime.DayOfYear == thanksgivingDay.DayOfYear;
        }

        public bool IsHanukkahDay1(DateTime dateTime)
        {
            string[] FirstDaysOfHanukkah =
            {
                "12/11/2020",
                "11/29/2021",
                "12/19/2022",
                "12/8/2023",
                "12/26/2024",
                "12/15/2025"
            };

            return CheckIfDayIsInListOfSolDays(dateTime, FirstDaysOfHanukkah);
        }

        public bool IsHanukkahDay2(DateTime dateTime)
        {
            string[] SecondDaysOfHanukkah =
            {
                "12/12/2020",
                "11/30/2021",
                "12/20/2022",
                "12/09/2023",
                "12/27/2024",
                "12/16/2025"
            };

            return CheckIfDayIsInListOfSolDays(dateTime, SecondDaysOfHanukkah);
        }

        public bool IsHanukkahDay3(DateTime dateTime)
        {
            string[] ThirdDaysOfHanukkah =
            {
                "12/13/2020",
                "12/01/2021",
                "12/21/2022",
                "12/10/2023",
                "12/28/2024",
                "12/17/2025"
            };

            return CheckIfDayIsInListOfSolDays(dateTime, ThirdDaysOfHanukkah);
        }

        public bool IsHanukkahDay4(DateTime dateTime)
        {
            string[] FourthDaysOfHanukkah =
            {
                "12/14/2020",
                "12/02/2021",
                "12/22/2022",
                "12/11/2023",
                "12/29/2024",
                "12/18/2025"
            };

            return CheckIfDayIsInListOfSolDays(dateTime, FourthDaysOfHanukkah);
        }

        public bool IsHanukkahDay5(DateTime dateTime)
        {
            string[] FifthDaysOfHanukkah =
            {
                "12/15/2020",
                "12/03/2021",
                "12/23/2022",
                "12/12/2023",
                "12/30/2024",
                "12/19/2025"
            };

            return CheckIfDayIsInListOfSolDays(dateTime, FifthDaysOfHanukkah);
        }

        public bool IsHanukkahDay6(DateTime dateTime)
        {
            string[] SixthDaysOfHanukkah =
            {
                "12/16/2020",
                "12/04/2021",
                "12/24/2022",
                "12/13/2023",
                "12/31/2024",
                "12/20/2025"
            };

            return CheckIfDayIsInListOfSolDays(dateTime, SixthDaysOfHanukkah);
        }

        public bool IsHanukkahDay7(DateTime dateTime)
        {
            string[] SeventhDaysOfHanukkah =
            {
                "12/17/2020",
                "12/05/2021",
                "12/25/2022",
                "12/14/2023",
                "01/01/2025",
                "12/21/2025"
            };

            return CheckIfDayIsInListOfSolDays(dateTime, SeventhDaysOfHanukkah);
        }

        public bool IsHanukkahDay8(DateTime dateTime)
        {
            string[] EighthDaysOfHanukkah =
            {
                "12/18/2020",
                "12/06/2021",
                "12/26/2022",
                "12/15/2023",
                "01/02/2025",
                "12/22/2025"
            };

            return CheckIfDayIsInListOfSolDays(dateTime, EighthDaysOfHanukkah);
        }

        public bool IsChristmasEve(DateTime dateTime)
        {
            return (dateTime.Month == 12 && dateTime.Day == 24);
        }
        public bool IsChristmasDay(DateTime dateTime)
        {
            return (dateTime.Month == 12 && dateTime.Day == 25);
        }
        public bool IsNewYearsEve(DateTime dateTime)
        {
            return (dateTime.Month == 12 && dateTime.Day == 31);
        }

        private static DateTime AdjustForWeekendHoliday(DateTime holiday)
        {
            if (holiday.DayOfWeek == DayOfWeek.Saturday)
            {
                return holiday.AddDays(-1);
            }
            else if (holiday.DayOfWeek == DayOfWeek.Sunday)
            {
                return holiday.AddDays(1);
            }
            else
            {
                return holiday;
            }
        }

        // Thanks to #realJSOP on codeproject.com for this easter code! (https://www.codeproject.com/Tips/1168636/When-Is-Easter)
        private static DateTime FindEasterSunday(int year)
        {
            int day;
            int month;

            int goldenNumber = year % 19;
            int century = year / 100;

            int daysToNextFullMoon = (century - (int)(century / 4) - (int)((8 * century + 13) / 25) + 19 * goldenNumber + 15) % 30;

            int daysToEquinox = daysToNextFullMoon - (int)(daysToNextFullMoon / 28) * (1 - (int)(daysToNextFullMoon / 28) * (int)(29 / (daysToNextFullMoon + 1)) * (int)((21 - goldenNumber) / 11));

            day = daysToEquinox - ((year + (int)(year / 4) + daysToEquinox + 2 - century + (int)(century / 4)) % 7) + 28;

            month = 3;
            if (day > 31)
            {
                month++;
                day -= 31;
            }

            DateTime result = new DateTime(year, month, day);

            return result;
        }

        private DateTime GetSunsetDate(DateTime date)
        {
            Celestial celestial = Celestial.CalculateCelestialTimes(42.304428, -87.948996, date);
            var sunset = celestial.SunSet.Value;
            return sunset;
        }

        private DateTime GetSunriseDate(DateTime date)
        {
            Celestial celestial = Celestial.CalculateCelestialTimes(42.304428, -87.948996, date);
            var sunrise = celestial.SunRise.Value;
            return sunrise;
        }

        private List<SolDay> GetSolDays(string[] days)
        {
            List<SolDay> daysToReturn = new List<SolDay>();

            foreach (string day in days)
            {
                DateTime endOfSolDay = DateTime.Parse(day);
                DateTime beginningOfSolDay = endOfSolDay.AddDays(-1);

                SolDay dayToAdd = new SolDay(
                    GetSunsetDate(beginningOfSolDay),
                    GetSunsetDate(endOfSolDay)
                    );
                daysToReturn.Add(dayToAdd);
            }

            return daysToReturn;
        }

        private bool CheckIfDayIsInListOfSolDays(DateTime dayToCheck, string[] dates)
        {
            bool itsInThere = false;

            List<SolDay> listOfSolDays = GetSolDays(dates);

            foreach (SolDay solDay in listOfSolDays)
            {
                if (dayToCheck.Ticks > solDay.BeginningSunSet.Ticks && dayToCheck.Ticks < solDay.EndingSunset.Ticks)
                {
                    itsInThere = true;
                }
            }

            return itsInThere;
        }
    }

    public class SolDay
    {
        public DateTime BeginningSunSet { get; set; }
        public DateTime EndingSunset { get; set; }

        public SolDay(DateTime begin, DateTime end)
        {
            BeginningSunSet = begin;
            EndingSunset = end;
        }
    }
}
