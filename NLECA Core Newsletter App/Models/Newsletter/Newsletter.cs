using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLECA_Core_Newsletter_App.Models.Newsletter
{
    public class Newsletter
    {
        public int NewsletterId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public string Memo { get; set; }
        public DateTime PublishedDate { get; set; }
        public bool IsCurrent { get; set; }

        public IEnumerable<Article> Articles { get; set; }

        public Newsletter()
        {
            Articles = new List<Article>();
        }
    }
}
