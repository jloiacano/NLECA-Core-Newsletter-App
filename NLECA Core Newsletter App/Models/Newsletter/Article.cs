using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLECA_Core_Newsletter_App.Models.Newsletter
{
    public class Article
    {
        public int ArticleId { get; set; }
        public int NewsletterId { get; set; }
        public string Text { get; set; }

    }
}
