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
        /// Gets all the holidays that are currently occurring<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
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
        /// Works on DateTime.Now so the time aspect will be subject to server location
        /// </summary>
        /// <returns>true if it is a holiday</returns>
        bool IsHoliday();

        /// <summary>
        /// Checks if the provided DateTime is a holiday
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check if it is a holiday.</param>
        /// <returns>true if provided DateTime is a holiday</returns>
        bool IsHoliday(DateTime dateTime);

        /// <summary>
        /// Checks if current DateTime is New Year's Day<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <returns>true if it is New Year's Day</returns>
        bool IsNewYearsDay();

        /// <summary>
        /// Checks if given DateTime is New Year's Day<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check</param>
        /// <returns>true if it is New Year's Day</returns>
        bool IsNewYearsDay(DateTime dateTime);

        /// <summary>
        /// Checks if current DateTime is Martin Luther King Jr. Day<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <returns>true if it isMartin Luther King Jr. Day</returns>
        bool IsMartinLutherKingJrDay();

        /// <summary>
        /// Checks if given DateTime is Martin Luther King Jr. Day<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check</param>
        /// <returns>true if it isMartin Luther King Jr. Day</returns>
        bool IsMartinLutherKingJrDay(DateTime dateTime);

        /// <summary>
        /// Checks if current DateTime is Groundhog Day<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <returns>true if it is GroundHog Day</returns>
        bool IsGroundHogDay();

        /// <summary>
        /// Checks if given DateTime is Groundhog Day<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check</param>
        /// <returns>true if it is GroundHog Day</returns>
        bool IsGroundHogDay(DateTime dateTime);

        /// <summary>
        /// Checks if current DateTime is Lincoln's BirthDay<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <returns>true if it is Lincoln's Birthday</returns>
        bool IsLincolnsBirthday();

        /// <summary>
        /// Checks if given DateTime is Lincoln's BirthDay<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check</param>
        /// <returns>true if it is Lincoln's Birthday</returns>
        bool IsLincolnsBirthday(DateTime dateTime);

        /// <summary>
        /// Checks if current DateTime is Valentine's Day<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <returns>true if it is Valentine's Day</returns>
        bool IsValentinesDay();

        /// <summary>
        /// Checks if given DateTime is Valentine's Day<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check</param>
        /// <returns>true if it is Valentine's Day</returns>
        bool IsValentinesDay(DateTime dateTime);

        /// <summary>
        /// Checks if current DateTime is Washington's Birthday / President's Day<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <returns>true if it is Washington's Birthday / President's Day</returns>
        bool IsWashingtonsBirthdayPresidentsDay();

        /// <summary>
        /// Checks if given DateTime is Washington's Birthday / President's Day<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check</param>
        /// <returns>true if it is Washington's Birthday / President's Day</returns>
        bool IsWashingtonsBirthdayPresidentsDay(DateTime dateTime);

        /// <summary>
        /// Checks if current DateTime is the day of Daylight Savings Starting<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <returns>true if it is the day of Daylight Savings Starting</returns>
        bool IsDaylightSavingsStart();

        /// <summary>
        /// Checks if given DateTime is the day of Daylight Savings Starting<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check</param>
        /// <returns>true if it is the day of Daylight Savings Starting</returns>
        bool IsDaylightSavingsStart(DateTime dateTime);

        /// <summary>
        /// Checks if current DateTime is Saint Patrick's Day<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <returns>true if it is Saint Patrick's Day</returns>
        bool IsStPatricksDay();

        /// <summary>
        /// Checks if given DateTime is Saint Patrick's Day<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check</param>
        /// <returns>true if it is Saint Patrick's Day</returns>
        bool IsStPatricksDay(DateTime dateTime);

        /// <summary>
        /// Checks if current DateTime is April Fools Day<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <returns>true if it is April Fools Day</returns>
        bool IsAprilFoolsDay();

        /// <summary>
        /// Checks if given DateTime is April Fools Day<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check</param>
        /// <returns>true if it is April Fools Day</returns>
        bool IsAprilFoolsDay(DateTime dateTime);

        /// <summary>
        /// Checks if current DateTime is within the days and times of Passover<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// &#8224; Works for the dates of Passover in the years from 2020 - 2030<para/>
        /// &#8224; &#8224; Times are based on celestial sunset and sunrise. Not acurate for prayer times.
        /// </summary>
        /// <returns>true if it is within the days and times of Passover</returns>
        bool IsPassover();

        /// <summary>
        /// Checks if given DateTime is within the days and times of Passover<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// &#8224; Works for the dates of Passover in the years from 2020 - 2030<para/>
        /// &#8224; &#8224; Times are based on celestial sunset and sunrise. Not acurate for prayer times.
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check</param>
        /// <returns>true if it is within the days and times of Passover</returns>
        bool IsPassover(DateTime dateTime);

        /// <summary>
        /// Checks if current DateTime is Good FriDay<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <returns>true if it is Good Friday</returns>
        bool IsGoodFriday();

        /// <summary>
        /// Checks if given DateTime is Good FriDay<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check</param>
        /// <returns>true if it is Good Friday</returns>
        bool IsGoodFriday(DateTime dateTime);

        /// <summary>
        /// Checks if current DateTime is Easter<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <returns>true if it is Easter</returns>
        bool IsEaster();

        /// <summary>
        /// Checks if given DateTime is Easter<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check</param>
        /// <returns>true if it is Easter</returns>
        bool IsEaster(DateTime dateTime);

        /// <summary>
        /// Checks if current DateTime is Earth Day<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <returns>true if it is Earth Day</returns>
        bool IsEarthDay();

        /// <summary>
        /// Checks if given DateTime is Earth Day<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check</param>
        /// <returns>true if it is Earth Day</returns>
        bool IsEarthDay(DateTime dateTime);

        /// <summary>
        /// Checks if current DateTime is Cinco De Mayo<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <returns>true if it is Cinco De Mayo</returns>
        bool IsCincoDeMayo();

        /// <summary>
        /// Checks if given DateTime is Cinco De Mayo<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check</param>
        /// <returns>true if it is Cinco De Mayo</returns>
        bool IsCincoDeMayo(DateTime dateTime);

        /// <summary>
        /// Checks if current DateTime is Mother's Day<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <returns>true if it is Mother's Day</returns>
        bool IsMothersDay();

        /// <summary>
        /// Checks if given DateTime is Mother's Day<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check</param>
        /// <returns>true if it is Mother's Day</returns>
        bool IsMothersDay(DateTime dateTime);

        /// <summary>
        /// Checks if current DateTime is Memorial Day<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <returns>true if it is Memorial Day</returns>
        bool IsMemorialDay();

        /// <summary>
        /// Checks if given DateTime is Memorial Day<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check</param>
        /// <returns>true if it is Memorial Day</returns>
        bool IsMemorialDay(DateTime dateTime);

        /// <summary>
        /// Checks if current DateTime is Flag Day<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <returns>true if it is Flag Day</returns>
        bool IsFlagDay();

        /// <summary>
        /// Checks if given DateTime is Flag Day<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check</param>
        /// <returns>true if it is Flag Day</returns>
        bool IsFlagDay(DateTime dateTime);

        /// <summary>
        /// Checks if current DateTime is Father's Day<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <returns>true if it is Father's Day</returns>
        bool IsFathersDay();

        /// <summary>
        /// Checks if given DateTime is Father's Day<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check</param>
        /// <returns>true if it is Father's Day</returns>
        bool IsFathersDay(DateTime dateTime);

        /// <summary>
        /// Checks if current DateTime is Independence Day (the Fourth of July)<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <returns>true if it is Independence Day (the Fourth of July)</returns>
        bool IsIndependenceDay();

        /// <summary>
        /// Checks if given DateTime is Independence Day (the Fourth of July)<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check</param>
        /// <returns>true if it is Independence Day (the Fourth of July)</returns>
        bool IsIndependenceDay(DateTime dateTime);

        /// <summary>
        /// Checks if current DateTime is Labor Day<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <returns>true if it is Labor Day</returns>
        bool IsLaborDay();

        /// <summary>
        /// Checks if given DateTime is Labor Day<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check</param>
        /// <returns>true if it is Labor Day</returns>
        bool IsLaborDay(DateTime dateTime);

        /// <summary>
        /// Checks if current DateTime is Rosh Hashanah<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// &#8224; Works for the dates of Rosh Hashanah in the years from 2020 - 2030<para/>
        /// &#8224; &#8224; Times are based on celestial sunset and sunrise. Not acurate for prayer times.
        /// </summary>
        /// <returns>true if it is Rosh Hashanah</returns>
        bool IsRoshHashanah();

        /// <summary>
        /// Checks if given DateTime is Rosh Hashanah<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// &#8224; Works for the dates of Rosh Hashanah in the years from 2020 - 2030<para/>
        /// &#8224; &#8224; Times are based on celestial sunset and sunrise. Not acurate for prayer times.
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check</param>
        /// <returns>true if it is Rosh Hashanah</returns>
        bool IsRoshHashanah(DateTime dateTime);

        /// <summary>
        /// Checks if current DateTime is Sweetest Day<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <returns>true if it is Sweetest Day</returns>
        bool IsSweetestDay();

        /// <summary>
        /// Checks if given DateTime is Sweetest Day<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check</param>
        /// <returns>true if it is Sweetest Day</returns>
        bool IsSweetestDay(DateTime dateTime);

        /// <summary>
        /// Checks if current DateTime is Halloween<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <returns>true if it is Halloween</returns>
        bool IsHalloween();

        /// <summary>
        /// Checks if given DateTime is Halloween<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check</param>
        /// <returns>true if it is Halloween</returns>
        bool IsHalloween(DateTime dateTime);

        /// <summary>
        /// Checks if current DateTime is the day of Daylight Savings Ending<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <returns>true if it is the day of Daylight Savings Ending</returns>
        bool IsDaylightSavingsEnd();

        /// <summary>
        /// Checks if given DateTime is the day of Daylight Savings Ending<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check</param>
        /// <returns>true if it is the day of Daylight Savings Ending</returns>
        bool IsDaylightSavingsEnd(DateTime dateTime);

        /// <summary>
        /// Checks if current DateTime is Veteran's Day<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <returns>true if it is Veteran's Day</returns>
        bool IsVeteransDay();

        /// <summary>
        /// Checks if given DateTime is Veteran's Day<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check</param>
        /// <returns>true if it is Veteran's Day</returns>
        bool IsVeteransDay(DateTime dateTime);

        /// <summary>
        /// Checks if current DateTime is Thanksgiving<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <returns>true if it is Thanksgiving</returns>
        bool IsThanksgivingDay();

        /// <summary>
        /// Checks if given DateTime is Thanksgiving<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check</param>
        /// <returns>true if it is Thanksgiving</returns>
        bool IsThanksgivingDay(DateTime dateTime);

        /// <summary>
        /// Checks if current DateTime is the first day of Hanukkah<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// &#8224; Works for the dates of Hanukkah in the years from 2020 - 2030<para/>
        /// &#8224; &#8224; Times are based on celestial sunset and sunrise. Not acurate for prayer times.
        /// </summary>
        /// <returns>true if it is the first day of Hanukkah</returns>
        bool IsHanukkahDay1();

        /// <summary>
        /// Checks if given DateTime is the first day of Hanukkah<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// &#8224; Works for the dates of Hanukkah in the years from 2020 - 2030<para/>
        /// &#8224; &#8224; Times are based on celestial sunset and sunrise. Not acurate for prayer times.
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check</param>
        /// <returns>true if it is the first day of Hanukkah</returns>
        bool IsHanukkahDay1(DateTime dateTime);

        /// <summary>
        /// Checks if current DateTime is the second day of Hanukkah<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// &#8224; Works for the dates of Hanukkah in the years from 2020 - 2030<para/>
        /// &#8224; &#8224; Times are based on celestial sunset and sunrise. Not acurate for prayer times.
        /// </summary>
        /// <returns>true if it is the second day of Hanukkah</returns>
        bool IsHanukkahDay2();

        /// <summary>
        /// Checks if given DateTime is the second day of Hanukkah<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// &#8224; Works for the dates of Hanukkah in the years from 2020 - 2030<para/>
        /// &#8224; &#8224; Times are based on celestial sunset and sunrise. Not acurate for prayer times.
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check</param>
        /// <returns>true if it is the second day of Hanukkah</returns>
        bool IsHanukkahDay2(DateTime dateTime);

        /// <summary>
        /// Checks if current DateTime is the third day of Hanukkah<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// &#8224; Works for the dates of Hanukkah in the years from 2020 - 2030<para/>
        /// &#8224; &#8224; Times are based on celestial sunset and sunrise. Not acurate for prayer times.
        /// </summary>
        /// <returns>true if it is the third day of Hanukkah</returns>
        bool IsHanukkahDay3();

        /// <summary>
        /// Checks if given DateTime is the third day of Hanukkah<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// &#8224; Works for the dates of Hanukkah in the years from 2020 - 2030<para/>
        /// &#8224; &#8224; Times are based on celestial sunset and sunrise. Not acurate for prayer times.
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check</param>
        /// <returns>true if it is the third day of Hanukkah</returns>
        bool IsHanukkahDay3(DateTime dateTime);

        /// <summary>
        /// Checks if current DateTime is the fourth day of Hanukkah<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// &#8224; Works for the dates of Hanukkah in the years from 2020 - 2030<para/>
        /// &#8224; &#8224; Times are based on celestial sunset and sunrise. Not acurate for prayer times.
        /// </summary>
        /// <returns>true if it is the fourth day of Hanukkah</returns>
        bool IsHanukkahDay4();

        /// <summary>
        /// Checks if given DateTime is the fourth day of Hanukkah<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// &#8224; Works for the dates of Hanukkah in the years from 2020 - 2030<para/>
        /// &#8224; &#8224; Times are based on celestial sunset and sunrise. Not acurate for prayer times.
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check</param>
        /// <returns>true if it is the fourth day of Hanukkah</returns>
        bool IsHanukkahDay4(DateTime dateTime);

        /// <summary>
        /// Checks if current DateTime is the fifth day of Hanukkah<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// &#8224; Works for the dates of Hanukkah in the years from 2020 - 2030<para/>
        /// &#8224; &#8224; Times are based on celestial sunset and sunrise. Not acurate for prayer times.
        /// </summary>
        /// <returns>true if it is the fifth day of Hanukkah</returns>
        bool IsHanukkahDay5();

        /// <summary>
        /// Checks if given DateTime is the fifth day of Hanukkah<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// &#8224; Works for the dates of Hanukkah in the years from 2020 - 2030<para/>
        /// &#8224; &#8224; Times are based on celestial sunset and sunrise. Not acurate for prayer times.
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check</param>
        /// <returns>true if it is the fifth day of Hanukkah</returns>
        bool IsHanukkahDay5(DateTime dateTime);

        /// <summary>
        /// Checks if current DateTime is the sixth day of Hanukkah<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// &#8224; Works for the dates of Hanukkah in the years from 2020 - 2030<para/>
        /// &#8224; &#8224; Times are based on celestial sunset and sunrise. Not acurate for prayer times.
        /// </summary>
        /// <returns>true if it is the sixth day of Hanukkah</returns>
        bool IsHanukkahDay6();

        /// <summary>
        /// Checks if given DateTime is the sixth day of Hanukkah<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// &#8224; Works for the dates of Hanukkah in the years from 2020 - 2030<para/>
        /// &#8224; &#8224; Times are based on celestial sunset and sunrise. Not acurate for prayer times.
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check</param>
        /// <returns>true if it is the sixth day of Hanukkah</returns>
        bool IsHanukkahDay6(DateTime dateTime);

        /// <summary>
        /// Checks if current DateTime is the seventh day of Hanukkah<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// &#8224; Works for the dates of Hanukkah in the years from 2020 - 2030<para/>
        /// &#8224; &#8224; Times are based on celestial sunset and sunrise. Not acurate for prayer times.
        /// </summary>
        /// <returns>true if it is the seventh day of Hanukkah</returns>
        bool IsHanukkahDay7();

        /// <summary>
        /// Checks if given DateTime is the seventh day of Hanukkah<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// &#8224; Works for the dates of Hanukkah in the years from 2020 - 2030<para/>
        /// &#8224; &#8224; Times are based on celestial sunset and sunrise. Not acurate for prayer times.
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check</param>
        /// <returns>true if it is the seventh day of Hanukkah</returns>
        bool IsHanukkahDay7(DateTime dateTime);

        /// <summary>
        /// Checks if current DateTime is the eighth day of Hanukkah<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// &#8224; Works for the dates of Hanukkah in the years from 2020 - 2030<para/>
        /// &#8224; &#8224; Times are based on celestial sunset and sunrise. Not acurate for prayer times.
        /// </summary>
        /// <returns>true if it is the eighth day of Hanukkah</returns>
        bool IsHanukkahDay8();

        /// <summary>
        /// Checks if given DateTime is the eighth day of Hanukkah<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// &#8224; Works for the dates of Hanukkah in the years from 2020 - 2030<para/>
        /// &#8224; &#8224; Times are based on celestial sunset and sunrise. Not acurate for prayer times.
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check</param>
        /// <returns>true if it is the eighth day of Hanukkah</returns>
        bool IsHanukkahDay8(DateTime dateTime);

        /// <summary>
        /// Checks if current DateTime is Christmas Eve Day<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <returns>true if it is Christmas Eve Day</returns>
        bool IsChristmasEve();

        /// <summary>
        /// Checks if given DateTime is Christmas Eve Day<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check</param>
        /// <returns>true if it is Christmas Eve Day</returns>
        bool IsChristmasEve(DateTime dateTime);

        /// <summary>
        /// Checks if current DateTime is Christmas Day<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <returns>true if it is Christmas Day</returns>
        bool IsChristmasDay();

        /// <summary>
        /// Checks if given DateTime is Christmas Day<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check</param>
        /// <returns>true if it is Christmas Day</returns>
        bool IsChristmasDay(DateTime dateTime);

        /// <summary>
        /// Checks if current DateTime is New Year's Eve<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <returns>true if it is New Year's Eve</returns>
        bool IsNewYearsEve();

        /// <summary>
        /// Checks if given DateTime is New Year's Eve<para />
        /// <font color="#F00"> WARNING SEE BELOW!</font> <para />
        /// &#8800; &#177; Works on DateTime.Now so the time aspect will be subject to server location &#177; &#8800;
        /// </summary>
        /// <param name="dateTime">DateTime which you would like to check</param>
        /// <returns>true if it is New Year's Eve</returns>
        bool IsNewYearsEve(DateTime dateTime);
    }
}
