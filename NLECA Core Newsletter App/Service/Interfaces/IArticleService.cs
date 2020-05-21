using NLECA_Core_Newsletter_App.Models.Newsletter;

namespace NLECA_Core_Newsletter_App.Service.Interfaces
{
    public interface IArticleService
    {
        /// <summary>
        /// Adds an article to the database
        /// </summary>
        /// <param name="article"></param>
        /// <returns>The Id of the Article which was added</returns>
        int AddArticleToNewsletter(int newsletterId);

        /// <summary>
        /// Gets an Article by it's id
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns>the article requested</returns>
        ArticleModel GetArticleByArticleId(int articleId);

        /// <summary>
        /// Updates the sequence number for an article in a newsletter
        /// </summary>
        /// <param name="articleId">Id of the article to be updated</param>
        /// <param name="updatedArticleSequence">the number to update the sequence number to</param>
        /// <returns>true if the update is successful</returns>
        bool UpdateArticleSequence(int articleId, int updatedArticleSequence);

        /// <summary>
        /// Updates an article
        /// </summary>
        /// <param name="article"></param>
        /// <returns>true if article was updated successfully</returns>
        bool UpdateArticle(ArticleModel article);

        /// <summary>
        /// Deletes an article entry from the database
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns>true if article was deleted successfully</returns>
        bool DeleteArticle(int articleId);
    }
}
