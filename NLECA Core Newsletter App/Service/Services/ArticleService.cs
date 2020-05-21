using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLECA_Core_Newsletter_App.Models.Newsletter;
using NLECA_Core_Newsletter_App.Service.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;

namespace NLECA_Core_Newsletter_App.Service.Services
{
    public class ArticleService : IArticleService
    {
        private readonly string dbConnectionString;
        private readonly ILogger<ArticleService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISQLHelperService _sql;
        public ArticleService(IConfiguration config, ILogger<ArticleService> logger, IHttpContextAccessor httpContextAccessor, ISQLHelperService sqlHelper)
        {
            dbConnectionString = config["ConnectionStrings:DefaultConnection"];
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _sql = sqlHelper;
        }

        /// <summary>
        /// Adds an article to the database
        /// </summary>
        /// <param name="article"></param>
        /// <returns>The Id of the Article which was added</returns>
        public int AddArticleToNewsletter(int newsletterId)
        {
            string currentUser = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            int currentUserId = -1;

            if (string.IsNullOrEmpty(currentUser))
            {
                _logger.LogError("CurrentUserId was unable to be acquired in ArticleService/AddArticle", null);
            }
            else
            {
                currentUserId = string.IsNullOrEmpty(currentUser) ? -1 : Int32.Parse(currentUser);
            }

            int returnedArticleId = 0;
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@newsletterId", newsletterId),
                    new SqlParameter("@addedBy", currentUserId),
                    new SqlParameter("@dateAdded", _sql.ConvertDateTimeForSQL(DateTime.Now)),
                    new SqlParameter("@articleId", returnedArticleId)
                };

                returnedArticleId = _sql.GetReturnValueFromStoredProcedure("AddArticle", parameters);
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error Adding/Inserting the article model in the Article Service", ex);
            }

            return returnedArticleId;
        }

        /// <summary>
        /// Gets an Article by it's id
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns>the article requested</returns>
        public ArticleModel GetArticleByArticleId(int articleId)
        {
            ArticleModel article = new ArticleModel();

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@articleId", articleId) };
            DataSet GetArticleByArticleIdResult = _sql.GetDatasetFromStoredProcedure("GetArticleByArticleId", parameters);

            try
            {
                DataRow articleResult = GetArticleByArticleIdResult.Tables[0].AsEnumerable().FirstOrDefault();

                article.ArticleId = Int32.Parse(articleResult["ArticleId"].ToString());
                article.NewsletterId = Int32.Parse(articleResult["NewsletterId"].ToString());
                article.ArticleSequence = Int32.Parse(articleResult["ArticleSequence"].ToString());
                article.ImageFileLocation = articleResult["ImageFileLocation"].ToString();
                article.ArticleType = Int32.Parse(articleResult["ArticleType"].ToString());
                article.ArticleTableOfContentsText = articleResult["ArticleTableOfContentsText"].ToString();
                article.ArticleTitle = articleResult["ArticleTitle"].ToString();
                article.ArticleText = articleResult["ArticleText"].ToString();
                article.AddedBy = Int32.Parse(articleResult["AddedBy"].ToString());
                article.DateAdded = DateTime.Parse(articleResult["DateAdded"].ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error retrieving the article model in the Article Service", ex);
            }

            return article;
        }

        /// <summary>
        /// Updates the sequence number for an article in a newsletter
        /// </summary>
        /// <param name="articleId">Id of the article to be updated</param>
        /// <param name="updatedArticleSequence">the number to update the sequence number to</param>
        /// <returns>true if the update is successful</returns>
        public bool UpdateArticleSequence(int articleId, int updatedArticleSequence)
        {
            int rowsEffected = 0;

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@articleId", articleId),
                    new SqlParameter("@updatedArticleSequence", updatedArticleSequence)
                };

                rowsEffected = _sql.GetReturnValueFromStoredProcedure("UpdateArticleSequence", parameters);
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an updating the article sequence in the Article Service", ex);
            }


            return rowsEffected > 0 ? true : false;
        }

        /// <summary>
        /// Updates an article
        /// </summary>
        /// <param name="article"></param>
        /// <returns>true if article was updated successfully</returns>
        public bool UpdateArticle(ArticleModel article)
        {
            int rowsEffected = 0;

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@articleId", article.ArticleId),
                    new SqlParameter("@newsletterId", article.NewsletterId),
                    new SqlParameter("@articleSequence", article.ArticleSequence),
                    new SqlParameter("@imageFileLocation", article.ImageFileLocation),
                    new SqlParameter("@articleType", article.ArticleType),
                    new SqlParameter("@articleTableOfContentsText", article.ArticleTableOfContentsText),
                    new SqlParameter("@articleTitle", article.ArticleTitle),
                    new SqlParameter("@articleText", article.ArticleText),
                    new SqlParameter("@addedBy", article.AddedBy),
                    new SqlParameter("@dateAdded", _sql.ConvertDateTimeForSQL(article.DateAdded))
                };

                rowsEffected = _sql.GetReturnValueFromStoredProcedure("UpdateArticle", parameters);
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an updating the article in the Article Service", ex);
            }


            return rowsEffected > 0 ? true : false;
        }

        /// <summary>
        /// Deletes an article entry from the database
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns>true if article was deleted successfully</returns>
        public bool DeleteArticle(int articleId)
        {
            int rowsEffected = 0;

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@articleId", articleId)
                };

                rowsEffected = _sql.GetReturnValueFromStoredProcedure("DeleteArticle", parameters);
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error deleting the article in the Article Service", ex);
            }


            return rowsEffected > 0 ? true : false;
        }
    }
}
