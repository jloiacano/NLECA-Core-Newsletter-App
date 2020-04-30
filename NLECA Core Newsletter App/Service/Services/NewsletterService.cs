using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLECA_Core_Newsletter_App.Data.SQLHelperTypes;
using NLECA_Core_Newsletter_App.Models.Newsletter;
using NLECA_Core_Newsletter_App.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public int AddNewsletter(NewsletterModel newsletter)
        {
            int returnedNewsletterId = -1;

            try
            {
                string insertStatement = _sql.CreateInsertStatement("Newsletters", new Dictionary<string, string>()
                    {
                        { "CreatedDate", _sql.ConvertDateTimeForSQL(newsletter.CreatedDate) }
                        ,{ "CreatedBy", newsletter.CreatedBy.ToString() }
                        ,{ "Memo", newsletter.Memo }
                        ,{ "DisplayDate", newsletter.DisplayDate }
                        ,{ "PublishedDate", _sql.ConvertDateTimeForSQL(newsletter.PublishedDate) }
                        ,{ "IsCurrent", newsletter.IsCurrent == true ? "1" : "0" }
                    }
                    , "NewsletterId");

                returnedNewsletterId = _sql.ExecuteInsertStatementWithReturn(insertStatement);
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

            SqlConnection connection = new SqlConnection(dbConnectionString);
            string querryForNewsletter = "SELECT TOP 1 * FROM Newsletters ORDER BY CreatedDate DESC";
            //TODO - J - Update select statement to reflect "published"

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(querryForNewsletter, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    newsletter.NewsletterId = Int32.Parse(reader["NewsletterId"].ToString());
                    newsletter.CreatedDate = DateTime.Parse(reader["CreatedDate"].ToString());
                    newsletter.CreatedBy = Int32.Parse(reader["CreatedBy"].ToString());
                    newsletter.Memo = reader["Memo"].ToString();
                    newsletter.DisplayDate = reader["DisplayDate"].ToString();
                    newsletter.PublishedDate = DateTime.Parse(reader["PublishedDate"].ToString());
                    newsletter.IsCurrent = reader["IsCurrent"].ToString() == "0" ? false : true;
                }

                connection.Close();

                if (newsletter.NewsletterId != 0)
                {
                    List<ArticleModel> articles = new List<ArticleModel>();

                    command.CommandText = _sql.CreateSelectStatement("Articles", 
                        new WhereClause(
                            new SqlHelperComparableDictionary(
                                SqlComparator.IsEqualTo,
                                new Dictionary<string, string>()
                                    {
                                        { "NewsletterId", newsletter.NewsletterId.ToString() }
                                    }
                                )
                            )
                        );

                    connection.Open();
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ArticleModel article = new ArticleModel();
                        article.ArticleId = Int32.Parse(reader["ArticleId"].ToString());
                        article.NewsletterId = newsletter.NewsletterId;
                        article.Text = reader["Text"].ToString();

                        articles.Add(article);
                    }

                    newsletter.Articles = articles;

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error retrieving the newsletter model in NewsletterService/GetPublishedNewsletter", ex);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
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

            SqlConnection connection = new SqlConnection(dbConnectionString);
            string queryForNewsletters = _sql.CreateSelectStatement("Newsletters", null);

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryForNewsletters, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    NewsletterModel newsletterToAdd = new NewsletterModel()
                    {
                        NewsletterId = Int32.Parse(reader["NewsletterId"].ToString()),
                        CreatedDate = DateTime.Parse(reader["CreatedDate"].ToString()),
                        CreatedBy = Int32.Parse(reader["CreatedBy"].ToString()),
                        Memo = reader["Memo"].ToString(),
                        DisplayDate = reader["DisplayDate"].ToString(),
                        PublishedDate = DateTime.Parse(reader["PublishedDate"].ToString()),
                        IsCurrent = reader["IsCurrent"].ToString() == "0" ? false : true,
                    };

                    newslettersToReturn.Add(newsletterToAdd);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error retrieving newsletters in NewsletterService/GetAllNewsletters", ex);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
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

            SqlConnection connection = new SqlConnection(dbConnectionString);
            string selectStatement = _sql.CreateSelectStatement("Newsletters", 
                new WhereClause(
                    new SqlHelperComparableDictionary(
                        SqlComparator.IsEqualTo,
                        new Dictionary<string, string>()
                            {
                                { "NewsletterId", newsletterId.ToString() }
                            }
                        )
                    )
                );

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(selectStatement, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    newsletter = new NewsletterModel()
                    {
                        NewsletterId = Int32.Parse(reader["NewsletterId"].ToString()),
                        CreatedDate = DateTime.Parse(reader["CreatedDate"].ToString()),
                        CreatedBy = Int32.Parse(reader["CreatedBy"].ToString()),
                        Memo = reader["Memo"].ToString(),
                        DisplayDate = reader["DisplayDate"].ToString(),
                        PublishedDate = DateTime.Parse(reader["PublishedDate"].ToString()),
                        IsCurrent = reader["IsCurrent"].ToString() == "0" ? false : true,
                    };
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error retrieving the newsletter in NewsletterService/GetNewsletterById with the Id: " + newsletterId, ex);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
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
            string updateStatement = _sql.CreateUpdateStatement(
                "Newsletters"
                , new Dictionary<string, string>()
                {
                    {"Memo", newsletter.Memo },
                    {"DisplayDate", newsletter.DisplayDate }
                }
                , new WhereClause(
                    new SqlHelperComparableDictionary(
                        SqlComparator.IsEqualTo,
                        new Dictionary<string, string>()
                            {
                                { "NewsletterId", newsletter.NewsletterId.ToString() }
                            }
                        )
                    )
                );
            return _sql.ExecuteUpdateStatement(updateStatement);
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
            bool success = false;

            string deleteStatement = _sql.CreateDeleteStatement("Newsletters",
                new WhereClause(
                    new SqlHelperComparableDictionary(
                        SqlComparator.IsEqualTo,
                        new Dictionary<string, string>()
                            {
                                { "NewsletterId", newsletterId.ToString() }
                            }
                        )
                    )
                );

            try
            {
                success = _sql.ExecuteDeleteStatement(deleteStatement);
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error deleting the newsletter in NewsletterService/DeleteNewsletter with the Id: " + newsletterId, ex);
            }

            return success;
        }
        #endregion
    }
}
