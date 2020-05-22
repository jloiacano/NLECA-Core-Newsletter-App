using Microsoft.AspNetCore.Mvc.Rendering;
using NLECA_Core_Newsletter_App.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NLECA_Core_Newsletter_App.Models.Newsletter
{
    public class EditArticleModel
    {
        public ArticleModel Article { get; set; }
        public IEnumerable<NewsletterArticleType> ArticleTypes { get; set; }

        public EditArticleModel(ArticleModel article)
        {
            Article = article;
            ArticleTypes = NewsletterArticleType.GetArticleTypes();
        }
    }
}
