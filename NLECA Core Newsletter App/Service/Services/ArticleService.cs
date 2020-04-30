using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLECA_Core_Newsletter_App.Data.SQLHelperTypes;
using NLECA_Core_Newsletter_App.Models.Newsletter;
using NLECA_Core_Newsletter_App.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

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
        /// <returns>true if article was added successfully</returns>
        public bool AddArticle(ArticleModel article)
        {
            bool success = false;
            string insertStatement = _sql.CreateInsertStatement("Articles", new Dictionary<string, string>()
            {
                { "ArticleId", article.ArticleId.ToString() }
                ,{ "NewsletterId", article.NewsletterId.ToString() }
                ,{ "Sequence", article.Sequence.ToString() }
                ,{ "ImageFileLocation", article.ImageFileLocation }
                ,{ "ArticleType", article.ArticleType.ToString() }
                ,{ "Text", article.Text }
                ,{ "AddedBy", article.AddedBy.ToString() }
                ,{ "DateAdded", article.DateAdded.ToString() }
            });

            try
            {
                success = _sql.ExecuteInsertStatement(insertStatement);
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error inserting the article using:" + insertStatement, ex);
            }

            return success;
        }

        /// <summary>
        /// Gets an Article by it's id
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns>the article requested</returns>
        public ArticleModel GetArticleById(int articleId)
        {
            ArticleModel article = new ArticleModel();

            SqlConnection connection = new SqlConnection(dbConnectionString);
            string queryForArticle = _sql.CreateSelectStatement("Articles",
                        new WhereClause(
                            new SqlHelperComparableDictionary(
                                SqlComparator.IsEqualTo,
                                new Dictionary<string, string>()
                                    {
                                        { "ArticleId", articleId.ToString() }
                                    }
                                )
                            )
                        );

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryForArticle, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    article.NewsletterId = Int32.Parse(reader["NewsletterId"].ToString());
                    article.ArticleId = Int32.Parse(reader["ArticleId"].ToString());
                    article.Sequence = Int32.Parse(reader["Sequence"].ToString());
                    article.ImageFileLocation = reader["ImageFileLocation"].ToString();
                    article.ArticleType = Int32.Parse(reader["ArticleType"].ToString());
                    article.Text = reader["Text"].ToString();
                    article.AddedBy = Int32.Parse(reader["AddedBy"].ToString());
                    article.DateAdded = DateTime.Parse(reader["DateAdded"].ToString());
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error retrieving the article model in the Article Service", ex);
            }

            return article;
        }

        /// <summary>
        /// Updates an article
        /// </summary>
        /// <param name="article"></param>
        /// <returns>true if article was updated successfully</returns>
        public bool EditArticle(ArticleModel article)
        {
            string updateStatement = _sql.CreateUpdateStatement(
                "Articles"
                , new Dictionary<string, string>()
                {
                    {"Sequence", article.Sequence.ToString() },
                    {"ImageFileLocation", article.ImageFileLocation },
                    {"ArticleType", article.ArticleType.ToString() },
                    {"Text", article.Text }
                }
                ,new WhereClause(
                    new SqlHelperComparableDictionary(
                        SqlComparator.IsEqualTo,
                        new Dictionary<string, string>()
                            {
                                { "ArticleId", article.ArticleId.ToString() },
                                { "NewsletterId", article.NewsletterId.ToString() }
                            }
                        )
                    )
                );
            return _sql.ExecuteUpdateStatement(updateStatement);
        }

        /// <summary>
        /// Deletes an article entry from the database
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns>true if article was deleted successfully</returns>
        public bool DeleteArticle(int articleId)
        {
            string deleteStatement = _sql.CreateDeleteStatement("Articles", 
                new WhereClause(
                    new SqlHelperComparableDictionary(
                        SqlComparator.IsEqualTo,
                        new Dictionary<string, string>()
                            {
                                {"ArticleId", articleId.ToString() }
                            }
                        )
                    )
                );

            //TODO - J - implement functionality to delete article by id
            throw new NotImplementedException();
        }
    }
}
