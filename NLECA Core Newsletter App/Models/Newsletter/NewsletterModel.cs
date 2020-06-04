using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

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

        public bool IsEdit { get; set; } = false;

        public IEnumerable<ArticleModel> Articles { get; set; }

        public NewsletterModel()
        {
            Articles = new List<ArticleModel>();
        }

        public NewsletterModel(DataRow dataRow)
        {
            NewsletterId = (int)dataRow["NewsletterId"];
            CreatedDate = (DateTime)dataRow["CreatedDate"];
            CreatedBy = (int)dataRow["CreatedBy"];
            Memo = dataRow["Memo"].ToString();
            DisplayDate = dataRow["DisplayDate"].ToString();
            //PublishedDate = (DateTime)dataRow["PublishedDate"];
            IsCurrent = (bool)dataRow["IsCurrent"];
            //IsCurrent = dataRow["IsCurrent"].ToString() == "0" ? false : true;

            if (dataRow.IsNull("PublishedDate") == false)
            {
                PublishedDate = (DateTime)dataRow["PublishedDate"];
            }
        }
    }
}
