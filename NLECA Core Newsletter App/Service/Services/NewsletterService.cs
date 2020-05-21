using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLECA_Core_Newsletter_App.Models.Newsletter;
using NLECA_Core_Newsletter_App.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;

namespace NLECA_Core_Newsletter_App.Service.Services
{
    public class NewsletterService : INewsletterService
    {
        private readonly string dbConnectionString;
        private readonly ILogger<NewsletterService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISQLHelperService _sql;
        public NewsletterService(IConfiguration config, ILogger<NewsletterService> logger, ISQLHelperService sqlHelper, IHttpContextAccessor httpContextAccessor)
        {
            dbConnectionString = config["ConnectionStrings:DefaultConnection"];
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _sql = sqlHelper;
        }

        #region // CREATE
        /// <summary>
        /// Adds a newsletter to the database along with all it's articles
        /// </summary>
        /// <param name="newsletter"></param>
        /// <returns>true if newsletter was added successfully</returns>
        public int AddNewsletter()
        {
            NewsletterModel newsletter = new NewsletterModel();

            string currentUser = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            int currentUserId = -1;

            if (string.IsNullOrEmpty(currentUser))
            {
                _logger.LogError("CurrentUserId was unable to be acquired in NewsletterService/AddNewsletter", null);
            }
            else
            {
                currentUserId = string.IsNullOrEmpty(currentUser) ? -1 : Int32.Parse(currentUser);
            }

            int returnedNewsletterId = -1;

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@createdDate", _sql.ConvertDateTimeForSQL(DateTime.Now)),
                    new SqlParameter("@createdBy", currentUserId),
                    new SqlParameter("@memo", "Please Update"),
                    new SqlParameter("@displayDate", DateTime.Now.ToString("yyyy-MM")),
                    new SqlParameter("@newsletterId", returnedNewsletterId)
                };

                returnedNewsletterId = _sql.GetReturnValueFromStoredProcedure("AddNewsletter", parameters);
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error Adding/Inserting the newsletter model in the Newsletter Service", ex);
            }

            return returnedNewsletterId;
        }
        #endregion

        #region // READ
        /// <summary>
        /// Gets the most recent published newsletter
        /// </summary>
        /// <returns>The most recent published newsletter</returns>
        public NewsletterModel GetPublishedNewsletter()
        {
            NewsletterModel newsletter = new NewsletterModel();

            DataSet GetPublishedNewsletterResult = _sql.GetDatasetFromStoredProcedure("GetPublishedNewsletter");
            DataRow newsletterResult = GetPublishedNewsletterResult.Tables[0].AsEnumerable().FirstOrDefault();

            if (newsletterResult != null)
            {
                try
                {
                    newsletter.NewsletterId = Int32.Parse(newsletterResult["NewsletterId"].ToString());
                    newsletter.CreatedDate = DateTime.Parse(newsletterResult["CreatedDate"].ToString());
                    newsletter.CreatedBy = Int32.Parse(newsletterResult["CreatedBy"].ToString());
                    newsletter.Memo = newsletterResult["Memo"].ToString();
                    newsletter.DisplayDate = newsletterResult["DisplayDate"].ToString();
                    newsletter.PublishedDate = DateTime.Parse(newsletterResult["PublishedDate"].ToString());
                    newsletter.IsCurrent = newsletterResult["IsCurrent"].ToString() == "0" ? false : true;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Unable to transcribe newsletterResult to newsletter Model in NewsletterService/GetPublishedNewsletter", ex);
                }

                SqlParameter[] getArticlesParameters = { new SqlParameter("@newsletterId", newsletter.NewsletterId) };

                DataSet GetArticlesByNewsletterIdResults = _sql.GetDatasetFromStoredProcedure("GetArticlesByNewsletterId", getArticlesParameters);

                try
                {
                    IEnumerable<DataRow> articleResults = GetArticlesByNewsletterIdResults.Tables[0].AsEnumerable();

                    List<ArticleModel> articles = new List<ArticleModel>();

                    foreach (DataRow row in articleResults)
                    {
                        ArticleModel article = new ArticleModel()
                        {
                            ArticleId = Int32.Parse(row["ArticleId"].ToString()),
                            NewsletterId = newsletter.NewsletterId,
                            ArticleSequence = Int32.Parse(row["ArticleSequence"].ToString()),
                            ImageFileLocation = row["ImageFileLocation"].ToString(),
                            ArticleType = Int32.Parse(row["ArticleType"].ToString()),
                            ArticleText = row["ArticleText"].ToString(),
                            AddedBy = Int32.Parse(row["AddedBy"].ToString()),
                            DateAdded = DateTime.Parse(row["DateAdded"].ToString())
                        };

                        articles.Add(article);
                    }

                    newsletter.Articles = articles.OrderBy(a => a.ArticleSequence);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Unable to transcribe GetArticlesByNewsletterIdResults to articles in NewsletterService/GetPublishedNewsletter", ex);
                }
            }
            else
            {
                _logger.LogWarning("newsletterResult was null in NewsletterService/GetPublishedNewsletter");
            }

            return newsletter;
        }

        /// <summary>
        /// Gets all the newsletters
        /// </summary>
        /// <returns>all the newsletters</returns>
        public List<NewsletterModel> GetAllNewsletters()
        {
            List<NewsletterModel> newslettersToReturn = new List<NewsletterModel>();

            DataSet GetAllNewslettersResult = _sql.GetDatasetFromStoredProcedure("GetAllNewsletters");

            try
            {
                IEnumerable<DataRow> newsletterResults = GetAllNewslettersResult.Tables[0].AsEnumerable();

                foreach (DataRow newsletterRow in newsletterResults)
                {
                    NewsletterModel newsletterToAdd = new NewsletterModel()
                    {
                        NewsletterId = Int32.Parse(newsletterRow["NewsletterId"].ToString()),
                        CreatedDate = DateTime.Parse(newsletterRow["CreatedDate"].ToString()),
                        CreatedBy = Int32.Parse(newsletterRow["CreatedBy"].ToString()),
                        Memo = newsletterRow["Memo"].ToString(),
                        DisplayDate = newsletterRow["DisplayDate"].ToString(),
                        PublishedDate = DateTime.Parse(newsletterRow["PublishedDate"].ToString()),
                        IsCurrent = newsletterRow["IsCurrent"].ToString() == "0" ? false : true
                    };

                    newslettersToReturn.Add(newsletterToAdd);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to transcribe GetAllNewslettersResult to newsletters in NewsletterService/GetAllNewsletters", ex);
            }

            return newslettersToReturn;
        }


        /// <summary>
        /// Gets a newsletter by it's id number
        /// </summary>
        /// <returns>the requested newsletter</returns>
        public NewsletterModel GetNewsletterById(int newsletterId)
        {
            NewsletterModel newsletter = new NewsletterModel();

            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@newsletterId", newsletterId) };            
            DataSet GetNewsletterByNewsletterIdResult = _sql.GetDatasetFromStoredProcedure("GetNewsletterByNewsletterId", parameters);

            try
            {
                DataRow newsletterResult = GetNewsletterByNewsletterIdResult.Tables[0].AsEnumerable().FirstOrDefault();

                newsletter.NewsletterId = Int32.Parse(newsletterResult["NewsletterId"].ToString());
                newsletter.CreatedDate = DateTime.Parse(newsletterResult["CreatedDate"].ToString());
                newsletter.CreatedBy = Int32.Parse(newsletterResult["CreatedBy"].ToString());
                newsletter.Memo = newsletterResult["Memo"].ToString();
                newsletter.DisplayDate = newsletterResult["DisplayDate"].ToString();
                newsletter.PublishedDate = DateTime.Parse(newsletterResult["PublishedDate"].ToString());
                newsletter.IsCurrent = newsletterResult["IsCurrent"].ToString() == "0" ? false : true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to transcribe GetNewsletterByNewsletterIdResult to newsletter in NewsletterService/GetNewsletterById with the Id: " + newsletterId, ex);
            }

            SqlParameter[] getArticlesParameters = { new SqlParameter("@newsletterId", newsletter.NewsletterId) };

            DataSet GetArticlesByNewsletterIdResults = _sql.GetDatasetFromStoredProcedure("GetArticlesByNewsletterId", getArticlesParameters);

            try
            {
                IEnumerable<DataRow> articleResults = GetArticlesByNewsletterIdResults.Tables[0].AsEnumerable();

                List<ArticleModel> articles = new List<ArticleModel>();

                foreach (DataRow row in articleResults)
                {
                    ArticleModel article = new ArticleModel()
                    {
                        ArticleId = Int32.Parse(row["ArticleId"].ToString()),
                        NewsletterId = newsletter.NewsletterId,
                        ArticleSequence = Int32.Parse(row["ArticleSequence"].ToString()),
                        ImageFileLocation = row["ImageFileLocation"].ToString(),
                        ArticleType = Int32.Parse(row["ArticleType"].ToString()),
                        ArticleText = row["ArticleText"].ToString(),
                        AddedBy = Int32.Parse(row["AddedBy"].ToString()),
                        DateAdded = DateTime.Parse(row["DateAdded"].ToString())
                    };

                    articles.Add(article);
                }

                newsletter.Articles = articles.OrderBy(a => a.ArticleSequence);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to transcribe GetArticlesByNewsletterIdResults to articles in NewsletterService/GetPublishedNewsletter", ex);
            }

            return newsletter;
        }
        #endregion

        #region // UPDATE
        /// <summary>
        /// Updates a newsletterto the newsletter passed in by it's id 
        /// </summary>
        /// <param name="newsletter"></param>
        /// <returns>true if newsletter was updated successfully</returns>
        public bool UpdateNewsletter(NewsletterModel newsletter)
        {
            int rowsEffected = 0;

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@newsletterId", newsletter.NewsletterId),
                    new SqlParameter("@createdDate", _sql.ConvertDateTimeForSQL(newsletter.CreatedDate)),
                    new SqlParameter("@createdBy", newsletter.CreatedBy),
                    new SqlParameter("@memo", newsletter.Memo),
                    new SqlParameter("@displayDate", newsletter.DisplayDate),
                    new SqlParameter("@publishedDate", _sql.ConvertDateTimeForSQL(newsletter.PublishedDate)),
                    new SqlParameter("@isCurrent", newsletter.IsCurrent == true ? "1" : "0")
                };

                rowsEffected = _sql.GetReturnValueFromStoredProcedure("UpdateNewsletter", parameters);
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an updating the newsletter in the Newsletter Service", ex);
            }


            return rowsEffected > 0 ? true : false;
        }
        #endregion

        #region // DELETE
        /// <summary>
        /// Deletes a newsletter entry from the database
        /// </summary>
        /// <param name="newsletterId"></param>
        /// <returns>true if newsletter was deleted successfully</returns>
        public bool DeleteNewsletter(int newsletterId)
        {
            int rowsEffected = 0;

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@newsletterId", newsletterId)
                };

                rowsEffected = _sql.GetReturnValueFromStoredProcedure("DeleteNewsletter", parameters);
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error deleting the newsletter in the Newsletter Service", ex);
            }


            return rowsEffected > 0 ? true : false;
        }
        #endregion
    }
}
