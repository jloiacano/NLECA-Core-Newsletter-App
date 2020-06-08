using Microsoft.Extensions.Logging;
using NLECA_Core_Newsletter_App.Models.Event;
using NLECA_Core_Newsletter_App.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace NLECA_Core_Newsletter_App.Service.Services
{
    public class EventService : IEventService
    {
        private readonly ILogger<EventService> _logger;
        private readonly ISQLHelperService _sql;

        public EventService(ILogger<EventService> logger, ISQLHelperService sql)
        {
            _logger = logger;
            _sql = sql;
        }
        public int AddEvent(EventModel eventModel)
        {

            int newEventId = 0;

            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@addedByUserId", eventModel.AddedByUserId)
                    ,new SqlParameter("@addedByUserName", eventModel.AddedByUserName)

                };
                newEventId = _sql.GetReturnValueFromStoredProcedure("AddEvent", parameters);
            }
            catch (Exception ex)
            {
                string error = string.Format(
                    "There was an error entering Event #{0} into database EventService/AddEvent",
                    eventModel.EventId);
                _logger.LogError(error, ex);
            }


            return newEventId;
        }

        public EventModel GetEventById(int eventId)
        {
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@eventId", eventId) };
            DataSet GetEventByIdResult = _sql.GetDatasetFromStoredProcedure("GetEventById", parameters);

            try
            {
                DataRow eventResult = GetEventByIdResult.Tables[0].AsEnumerable().FirstOrDefault();

                return new EventModel(eventResult);
            }
            catch (Exception ex)
            {
                string error = string.Format(
                   "There was an error retrieving the EventModel #{0} in EventService/GetEventById",
                   eventId.ToString());
                _logger.LogError(error, ex);
            }

            return null;
        }

        public IEnumerable<EventModel> GetAllEvents()
        {
            DataSet eventsDataSet = _sql.GetDatasetFromStoredProcedure("GetAllEvents");

            List<EventModel> events = new List<EventModel>();

            try
            {
                IEnumerable<DataRow> eventResults = eventsDataSet.Tables[0].AsEnumerable();

                foreach (var eventResult in eventResults)
                {
                    EventModel eventModel = new EventModel(eventResult);
                    events.Add(eventModel);
                }
            }
            catch (Exception ex)
            {
                string error = string.Format(
                    "There was an error retrieving all Events in EventService/GetAllEvents");
                _logger.LogError(error, ex);
            }
            return events.AsEnumerable();
        }

        public IEnumerable<EventModel> GetAllPublishedEvents()
        {
            DataSet eventsDataSet = _sql.GetDatasetFromStoredProcedure("GetAllPublishedEvents");

            List<EventModel> events = new List<EventModel>();

            try
            {
                IEnumerable<DataRow> eventResults = eventsDataSet.Tables[0].AsEnumerable();

                foreach (var eventResult in eventResults)
                {
                    EventModel eventModel = new EventModel(eventResult);
                    events.Add(eventModel);
                }
            }
            catch (Exception ex)
            {
                string error = string.Format(
                    "There was an error retrieving all Events in EventService/GetAllPublishedEvents");
                _logger.LogError(error, ex);
            }
            return events.AsEnumerable();
        }

        public IEnumerable<EventModel> GetAllFutureEvents()
        {
            DataSet eventsDataSet = _sql.GetDatasetFromStoredProcedure("GetAllFutureEvents");

            List<EventModel> events = new List<EventModel>();

            try
            {
                IEnumerable<DataRow> eventResults = eventsDataSet.Tables[0].AsEnumerable();

                foreach (var eventResult in eventResults)
                {
                    EventModel eventModel = new EventModel(eventResult);
                    events.Add(eventModel);
                }
            }
            catch (Exception ex)
            {
                string error = string.Format(
                    "There was an error retrieving all Events in EventService/GetAllFutureEvents");
                _logger.LogError(error, ex);
            }
            return events.AsEnumerable();
        }

        public IEnumerable<EventModel> GetEventsForDay(DateTime day)
        {
            List<EventModel> events = new List<EventModel>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                    {
                    new SqlParameter("@day", _sql.ConvertDateTimeForSQL(day))
                    };

                DataSet eventsDataSet = _sql.GetDatasetFromStoredProcedure("GetEventsForDay", parameters);

                IEnumerable<DataRow> eventResults = eventsDataSet.Tables[0].AsEnumerable();

                foreach (var eventResult in eventResults)
                {
                    EventModel eventModel = new EventModel(eventResult);
                    events.Add(eventModel);
                }
            }
            catch (Exception ex)
            {
                string error = string.Format(
                    "There was an retrieving events for {0} in EventService/GetEventsForDay",
                    day.ToString());
                _logger.LogError(error, ex);
            }
            return events.AsEnumerable();
        }

        public IEnumerable<EventModel> GetEventsInDateRange(DateTime start, DateTime end)
        {
            List<EventModel> events = new List<EventModel>();

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                    {
                    new SqlParameter("@start", _sql.ConvertDateTimeForSQL(start))
                    ,new SqlParameter("@end", _sql.ConvertDateTimeForSQL(end))
                    };

                DataSet eventsDataSet = _sql.GetDatasetFromStoredProcedure("GetEventsInDateRange", parameters);

                IEnumerable<DataRow> eventResults = eventsDataSet.Tables[0].AsEnumerable();

                foreach (var eventResult in eventResults)
                {
                    EventModel eventModel = new EventModel(eventResult);
                    events.Add(eventModel);
                }
            }
            catch (Exception ex)
            {
                string error = string.Format(
                    "There was an retrieving events between {0} and {1} in EventService/GetEventsInDateRange",
                    start.ToString(),
                    end.ToString());
                _logger.LogError(error, ex);
            }
            return events.AsEnumerable();
        }

        public bool UpdateEvent(EventModel eventModel)
        {
            int rowseffected = 0;

            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@eventId", eventModel.EventId)
                    ,new SqlParameter("@addedByUserId", eventModel.AddedByUserId)
                    ,new SqlParameter("@addedByUserName", eventModel.AddedByUserName)
                    ,new SqlParameter("@dateAdded", _sql.ConvertDateTimeForSQL(eventModel.DateAdded))
                    ,new SqlParameter("@eventTitle", eventModel.EventTitle)
                    ,new SqlParameter("@isAllDayEvent", eventModel.IsAllDayEvent)
                    ,new SqlParameter("@isMultiDayEvent", eventModel.IsMultiDayEvent)
                    ,new SqlParameter("@eventDate", _sql.ConvertDateTimeForSQL(eventModel.EventDate))
                    ,new SqlParameter("@eventDateEnd", _sql.ConvertDateTimeForSQL(eventModel.EventDateEnd))
                    ,new SqlParameter("@dateIsFinalized", eventModel.DateIsFinalized)
                    ,new SqlParameter("@eventLocation", eventModel.EventLocation)
                    ,new SqlParameter("@eventHost", eventModel.EventHost)
                    ,new SqlParameter("@eventShortDetails", eventModel.EventShortDetails)
                    ,new SqlParameter("@eventLongDetails", eventModel.EventLongDetails)
                    ,new SqlParameter("@eventImageLocation", eventModel.EventImageLocation)
                };
                rowseffected = _sql.GetReturnValueFromStoredProcedure("UpdateEvent", parameters);
            }
            catch (Exception ex)
            {
                string error = string.Format(
                    "There was an error updating Event #{0} into database EventService/UpdateEvent",
                    eventModel.EventId);
                _logger.LogError(error, ex);
            }

            return rowseffected > 0;
        }

        public bool PublishEvent(int eventId)
        {
            int rowseffected = 0;
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@eventId", eventId)
                };

                rowseffected = _sql.GetReturnValueFromStoredProcedure("PublishEvent", parameters);
            }
            catch (Exception ex)
            {
                string error = string.Format(
                    "There was an error publishing Event #{0} in EventService/PublishEvent",
                    eventId);
                _logger.LogError(error, ex);
            }

            return rowseffected > 0;
        }

        public bool UnpublishEvent(int eventId)
        {
            int rowseffected = 0;
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@eventId", eventId)
                };

                rowseffected = _sql.GetReturnValueFromStoredProcedure("UnpublishEvent", parameters);
            }
            catch (Exception ex)
            {
                string error = string.Format(
                    "There was an error unpublishing Event #{0} in EventService/UnpublishEvent",
                    eventId);
                _logger.LogError(error, ex);
            }

            return rowseffected > 0;
        }

        public bool DeleteEvent(int eventId)
        {
            int rowseffected = 0;
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@eventId", eventId)
                };

                rowseffected = _sql.GetReturnValueFromStoredProcedure("DeleteEvent", parameters);
            }
            catch (Exception ex)
            {
                string error = string.Format(
                    "There was an error deleting Event #{0} in EventService/DeleteEvent",
                    eventId);
                _logger.LogError(error, ex);
            }

            return rowseffected > 0;
        }
    }
}
