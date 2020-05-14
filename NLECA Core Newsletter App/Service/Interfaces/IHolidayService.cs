using NLECA_Core_Newsletter_App.Data;
using System;
using System.Collections.Generic;

namespace NLECA_Core_Newsletter_App.Service.Interfaces
{
    /// <summary>
    /// Contains all the methods for checking if a DateTime is a holiday
    /// </summary>
    public interface IHolidayService
    {
        /// <summary>
        /// Gets all the holidays that are currently occurring
        /// </summary>
        /// <returns>IEnumerable of the current Holidays in Holiday enum</returns>
        IEnumerable<Holiday> GetHolidays();
        /// <summary>
        /// Gets all the holidays occuring on the DateTime provided
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check for holidays.</param>
        /// <returns>IEnumerable of the current Holidays in Holiday enum</returns>
        IEnumerable<Holiday> GetHolidays(DateTime dateTime);
        /// <summary>
        /// Checks if the current moment is a holiday
        /// </summary>
        /// <returns>true if it is a holiday</returns>
        ///<remarks>Works on DateTime.Now so the time aspect will be subject to server location</remarks>
        bool IsHoliday();
        /// <summary>
        /// Checks if the provided DateTime is a holiday
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check if it is a holiday.</param>
        /// <returns>true if provided DateTime is a holiday</returns>
        bool IsHoliday(DateTime dateTime);
        bool IsNewYearsDay();
        bool IsNewYearsDay(DateTime dateTime);
        bool IsMartinLutherKingJrDay();
        bool IsMartinLutherKingJrDay(DateTime dateTime);
        bool IsGroundHogDay();
        bool IsGroundHogDay(DateTime dateTime);
        bool IsLincolnsBirthday();
        bool IsLincolnsBirthday(DateTime dateTime);
        bool IsValentinesDay();
        bool IsValentinesDay(DateTime dateTime);
        bool IsWashingtonsBirthdayPresidentsDay();
        bool IsWashingtonsBirthdayPresidentsDay(DateTime dateTime);
        bool IsDaylightSavingsStart();
        bool IsDaylightSavingsStart(DateTime dateTime);
        bool IsStPatricksDay();
        bool IsStPatricksDay(DateTime dateTime);
        bool IsAprilFoolsDay();
        bool IsAprilFoolsDay(DateTime dateTime);
        bool IsPassover();
        bool IsPassover(DateTime dateTime);
        bool IsGoodFriday();
        bool IsGoodFriday(DateTime dateTime);
        bool IsEaster();
        bool IsEaster(DateTime dateTime);
        bool IsEarthDay();
        bool IsEarthDay(DateTime dateTime);
        bool IsCincoDeMayo();
        bool IsCincoDeMayo(DateTime dateTime);
        bool IsMothersDay();
        bool IsMothersDay(DateTime dateTime);
        bool IsMemorialDay();
        bool IsMemorialDay(DateTime dateTime);
        bool IsFlagDay();
        bool IsFlagDay(DateTime dateTime);
        bool IsFathersDay();
        bool IsFathersDay(DateTime dateTime);
        bool IsIndependenceDay();
        bool IsIndependenceDay(DateTime dateTime);
        bool IsLaborDay();
        bool IsLaborDay(DateTime dateTime);
        bool IsRoshHashanah();
        bool IsRoshHashanah(DateTime dateTime);
        bool IsSweetestDay();
        bool IsSweetestDay(DateTime dateTime);
        bool IsHalloween();
        bool IsHalloween(DateTime dateTime);
        bool IsDaylightSavingsEnd();
        bool IsDaylightSavingsEnd(DateTime dateTime);
        bool IsVeteransDay();
        bool IsVeteransDay(DateTime dateTime);
        bool IsThanksgivingDay();
        bool IsThanksgivingDay(DateTime dateTime);
        bool IsHanukkahDay1();
        bool IsHanukkahDay1(DateTime dateTime);
        bool IsHanukkahDay2();
        bool IsHanukkahDay2(DateTime dateTime);
        bool IsHanukkahDay3();
        bool IsHanukkahDay3(DateTime dateTime);
        bool IsHanukkahDay4();
        bool IsHanukkahDay4(DateTime dateTime);
        bool IsHanukkahDay5();
        bool IsHanukkahDay5(DateTime dateTime);
        bool IsHanukkahDay6();
        bool IsHanukkahDay6(DateTime dateTime);
        bool IsHanukkahDay7();
        bool IsHanukkahDay7(DateTime dateTime);
        bool IsHanukkahDay8();
        bool IsHanukkahDay8(DateTime dateTime);
        bool IsChristmasEve();
        bool IsChristmasEve(DateTime dateTime);
        bool IsChristmasDay();
        bool IsChristmasDay(DateTime dateTime);
        bool IsNewYearsEve();
        bool IsNewYearsEve(DateTime dateTime);
    }
}
