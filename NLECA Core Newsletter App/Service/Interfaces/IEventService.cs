using NLECA_Core_Newsletter_App.Models.Event;
using System;
using System.Collections.Generic;

namespace NLECA_Core_Newsletter_App.Service.Interfaces
{
    public interface IEventService
    {
        /// <summary>
        /// Adds An Event to the database
        /// </summary>
        /// <param name="eventModel">the event details</param>
        /// <returns>true if event was successfully added</returns>
        int AddEvent(EventModel eventModel);

        /// <summary>
        /// Gets a single event, and its details, from the database
        /// </summary>
        /// <param name="eventId">the id of the event</param>
        /// <returns>A single EventModel with all the events details</returns>
        EventModel GetEventById(int eventId);

        /// <summary>
        /// Gets all the events in the database
        /// </summary>
        /// <returns>All the EventModels in the database</returns>
        IEnumerable<EventModel> GetAllEvents();

        /// <summary>
        /// Gets all the events in the database which have been published
        /// </summary>
        /// <returns>All the EventModels in the database which have been published</returns>
        IEnumerable<EventModel> GetAllPublishedEvents();

        /// <summary>
        /// Gets all the events in the database occuring after now
        /// </summary>
        /// <returns>All the EventModels in the database occuring after now</returns>
        IEnumerable<EventModel> GetAllFutureEvents();

        /// <summary>
        /// Gets all events, either occuring on a day, or the day is within the
        /// time range of the events start and end dates
        /// </summary>
        /// <param name="day">day to check for events which are occuring</param>
        /// <returns>IEnumerable of all EventModels which could be occuring on the day provided</returns>
        IEnumerable<EventModel> GetEventsForDay(DateTime day);

        /// <summary>
        /// Gets all events which fit one of the following conditions:
        /// <list type="bullet">
        /// <item>Event is happening on a day within the range of start and end date parameters</item>
        /// <item>Event has an end date on or after the start date parameter</item>
        /// <item>Event has a start date on or before the end date parameter</item>
        /// </list>
        /// </summary>
        /// <param name="start">Start DateTime of the range of dates to check</param>
        /// <param name="end">End DateTime of the range of dates to check</param>
        /// <returns>IEnumerable of all EventModels which could be occuring within the date range provided</returns>
        IEnumerable<EventModel> GetEventsInDateRange(DateTime start, DateTime end);

        /// <summary>
        /// Updates an existing EventModel in the database
        /// </summary>
        /// <param name="eventModel">EventModel with the updates</param>
        /// <returns>true if EventModel was successfully updated</returns>
        bool UpdateEvent(EventModel eventModel);

        /// <summary>
        /// Publishes the event by its id
        /// </summary>
        /// <param name="eventId">Id of the event you would like to publish</param>
        /// <returns>true if event was successfully published</returns>
        bool PublishEvent(int eventId);

        /// <summary>
        /// Unpublishes the event by its id
        /// </summary>
        /// <param name="eventId">Id of the event you would like to unpublish</param>
        /// <returns>true if event was successfully unpublished</returns>
        bool UnpublishEvent(int eventId);

        /// <summary>
        /// Deletes an existing EventModel in the database
        /// </summary>
        /// <param name="eventId">Id of the event to be deleted</param>
        /// <returns>true if the event was successfully deleted</returns>
        bool DeleteEvent(int eventId);

    }
}
