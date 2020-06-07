using System;
using System.Data;

namespace NLECA_Core_Newsletter_App.Models.Event
{
    public class EventModel
    {
        public int EventId { get; set; }
        public string AddedByUserId { get; set; }
        public string AddedByUserName { get; set; }
        public DateTime DateAdded { get; set; }
        public string EventTitle { get; set; }
        public bool IsAllDayEvent { get; set; }
        public bool IsMultiDayEvent { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime EventDateEnd { get; set; }
        public bool DateIsFinalized { get; set; }
        public string EventLocation { get; set; }
        public string EventHost { get; set; }
        public string EventShortDetails { get; set; }
        public string EventLongDetails { get; set; }
        public string EventImageLocation { get; set; }
        public bool IsPublished { get; set; }

        public EventModel() { }       

        public EventModel(string userId, string userName)
        {
            AddedByUserId = userId;
            AddedByUserName = userName;
        }

        public EventModel(DataRow dataRow)
        {
            EventId = (int)dataRow["EventId"];
            AddedByUserId = dataRow["AddedByUserId"].ToString();
            AddedByUserName = dataRow["AddedByUserName"].ToString();
            DateAdded = (DateTime)dataRow["DateAdded"];
            EventTitle = dataRow["EventTitle"].ToString();
            IsAllDayEvent = (bool)dataRow["IsAllDayEvent"];
            IsMultiDayEvent = (bool)dataRow["IsMultiDayEvent"];
            EventDate = (DateTime)dataRow["EventDate"];
            EventDateEnd = (DateTime)dataRow["EventDateEnd"];
            DateIsFinalized = (bool)dataRow["DateIsFinalized"];
            EventLocation = dataRow["EventLocation"].ToString();
            EventHost = dataRow["EventHost"].ToString();
            EventShortDetails = dataRow["EventShortDetails"].ToString();
            EventLongDetails = dataRow["EventLongDetails"].ToString();
            EventImageLocation = (dataRow["EventImageLocation"] == DBNull.Value) ? string.Empty
                : dataRow["EventImageLocation"].ToString();
            IsPublished = (bool)dataRow["IsPublished"];
        }
    }
}
