using NLECA_Core_Newsletter_App.Models.Newsletter;
using System.Collections.Generic;

namespace NLECA_Core_Newsletter_App.Models.ArticleImage
{
    public class ArticleImageManagerModel
    {
        public int ArticleId { get; set; }
        public bool IsSuperAdmin { get; set; }
        public IEnumerable<ArticleImageInArticleModel> ArticleImages { get; set; }
    }
}
