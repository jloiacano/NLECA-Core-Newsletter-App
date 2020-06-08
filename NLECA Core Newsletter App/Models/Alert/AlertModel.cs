using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace NLECA_Core_Newsletter_App.Models.Alert
{
    public class AlertModel
    {
        public int AlertId { get; set; }
        public string AddedByUserId { get; set; }
        public string AddedByUserName { get; set; }
        public DateTime DateAdded { get; set; }
        public string AlertTitle { get; set; }
        public DateTime AlertDate { get; set; }
        public DateTime AlertDateEnd { get; set; }
        public string AlertShortDetails { get; set; }
        public string AlertLongDetails { get; set; }


        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string AlertImageLocation { get; set; }

        public bool IsPublished { get; set; }

        public AlertModel() { }

        public AlertModel(string userId, string userName)
        {
            AddedByUserId = userId;
            AddedByUserName = userName;
        }

        public AlertModel(DataRow dataRow)
        {
            AlertId = (int)dataRow["AlertId"];
            AddedByUserId = dataRow["AddedByUserId"].ToString();
            AddedByUserName = dataRow["AddedByUserName"].ToString();
            DateAdded = (DateTime)dataRow["DateAdded"];
            AlertTitle = dataRow["AlertTitle"].ToString();
            AlertDate = (DateTime)dataRow["AlertDate"];
            AlertDateEnd = (DateTime)dataRow["AlertDateEnd"];
            AlertShortDetails = dataRow["AlertShortDetails"].ToString();
            AlertLongDetails = dataRow["AlertLongDetails"].ToString();
            AlertImageLocation = (dataRow["AlertImageLocation"] == DBNull.Value) ? string.Empty
                : dataRow["AlertImageLocation"].ToString();
            IsPublished = (bool)dataRow["IsPublished"];
        }
    }
}
