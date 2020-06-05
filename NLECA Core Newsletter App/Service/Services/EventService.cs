using NLECA_Core_Newsletter_App.Models.Event;
using NLECA_Core_Newsletter_App.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLECA_Core_Newsletter_App.Service.Services
{
    public class EventService : IEventService
    {
        public bool AddEvent(EventModel eventModel)
        {
            throw new NotImplementedException();
        }

        public EventModel GetEventById(int eventId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EventModel> GetAllEvents()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EventModel> GetEventsForDay(DateTime day)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EventModel> GetEventsInDateRange(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public bool UpdateEvent(int eventId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteEvent(int eventId)
        {
            throw new NotImplementedException();
        }
    }
}
