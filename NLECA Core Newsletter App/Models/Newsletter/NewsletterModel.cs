using System;
using System.Collections.Generic;
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
    }
}
