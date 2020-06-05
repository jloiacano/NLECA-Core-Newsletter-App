using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLECA_Core_Newsletter_App.Models.Event
{
    public class EventModel
    {
        public int EventId { get; set; }
        public int AddedByUserId { get; set; }
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
    }
}
