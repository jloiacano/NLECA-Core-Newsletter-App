using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLECA_Core_Newsletter_App.Models.Alert
{
    public class AlertModel
    {
        public int ArticleId { get; set; }
        public int AddedByUserId { get; set; }
        public string AddedByUserName { get; set; }
        public DateTime DateAdded { get; set; }
        public string AlertTitle { get; set; }
        public DateTime AlertDate { get; set; }
        public DateTime AlertDateEnd { get; set; }
        public string AlertShortDetails { get; set; }
        public string AlertLongDetails { get; set; }
        public string AlertImageLocation { get; set; }
        public bool IsPublished { get; set; }
    }
}
