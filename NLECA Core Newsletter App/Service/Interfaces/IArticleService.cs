﻿using NLECA_Core_Newsletter_App.Models.Newsletter;

namespace NLECA_Core_Newsletter_App.Service.Interfaces
{
    public interface IArticleService
    {
        /// <summary>
        /// Adds an article to the database
        /// </summary>
        /// <param name="article"></param>
        /// <returns>true if article was added successfully</returns>
        bool AddArticle(ArticleModel article);

        /// <summary>
        /// Gets an Article by it's id
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns>the article requested</returns>
        ArticleModel GetArticleById(int articleId);

        /// <summary>
        /// Updates an article
        /// </summary>
        /// <param name="article"></param>
        /// <returns>true if article was updated successfully</returns>
        bool EditArticle(ArticleModel article);

        /// <summary>
        /// Deletes an article entry from the database
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns>true if article was deleted successfully</returns>
        bool DeleteArticle(int articleId);
    }
}