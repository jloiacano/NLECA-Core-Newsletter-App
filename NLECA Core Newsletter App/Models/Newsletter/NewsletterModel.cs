using NLECA_Core_Newsletter_App.Models.Event;
using System;
using System.Collections.Generic;
using System.Data;

namespace NLECA_Core_Newsletter_App.Models.Newsletter
{
    public class NewsletterModel
    {
        public int NewsletterId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public string Memo { get; set; }
        public string DisplayDate { get; set; }
        public DateTime PublishedDate { get; set; }
        public bool IsCurrent { get; set; }
        public bool HasBeenPublished { get; set; }
        public DateTime EventsStartDate { get; set; }
        public DateTime EventsEndDate { get; set; }


        public bool IsEdit { get; set; } = false;

        public IEnumerable<ArticleModel> Articles { get; set; }
        public IEnumerable<EventModel> Events { get; set; }

        public NewsletterModel()
        {
            Articles = new List<ArticleModel>();
            Events = new List<EventModel>();
        }

        public NewsletterModel(DataRow dataRow)
        {
            NewsletterId = (int)dataRow["NewsletterId"];
            CreatedDate = (DateTime)dataRow["CreatedDate"];
            CreatedBy = (int)dataRow["CreatedBy"];
            Memo = dataRow["Memo"].ToString();
            DisplayDate = dataRow["DisplayDate"].ToString();
            IsCurrent = (bool)dataRow["IsCurrent"];
            HasBeenPublished = (bool)dataRow["HasBeenPublished"];
            EventsStartDate = (DateTime)dataRow["EventsStartDate"];
            EventsEndDate = (DateTime)dataRow["EventsEndDate"];

            if (dataRow.IsNull("PublishedDate") == false)
            {
                PublishedDate = (DateTime)dataRow["PublishedDate"];
            }
        }
    }
}
