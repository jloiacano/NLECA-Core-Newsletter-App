using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        public NewsletterService(IConfiguration config, ILogger<NewsletterService> logger, IHttpContextAccessor httpContextAccessor)
        {
            dbConnectionString = config["ConnectionStrings:DefaultConnection"];
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Gets the most recent published newsletter
        /// </summary>
        /// <returns>The most recent published newsletter</returns>
        public Newsletter GetNewsletter()
        {
            Newsletter newsletter = new Newsletter();

            SqlConnection connection = new SqlConnection(dbConnectionString);
            string querryForNewsletter = "SELECT TOP 1 * FROM Newsletters ORDER BY CreatedDate DESC";

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(querryForNewsletter, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    newsletter.NewsletterId = Int32.Parse(reader["NewsletterId"].ToString());
                    newsletter.CreatedDate = DateTime.Parse(reader["CreatedDate"].ToString());
                    newsletter.CreatedBy = Int32.Parse(reader["CreatedDate"].ToString());
                    newsletter.Memo = reader["Memo"].ToString();
                    newsletter.PublishedDate = DateTime.Parse(reader["PublishedDate"].ToString());
                    newsletter.IsCurrent = reader["IsCurrent"].ToString() == "0" ? false : true;
                }

                if (newsletter.NewsletterId != 0)
                {
                    List<Article> articles = new List<Article>();
                    string querryForArticles = string.Format("SELECT * FROM Articles WHERE NewsletterId = {0}", newsletter.NewsletterId.ToString());
                    command.CommandText = querryForArticles;
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Article article = new Article();
                        article.ArticleId = Int32.Parse(reader["ArticleId"].ToString());
                        article.NewsletterId = newsletter.NewsletterId;
                        article.Text = reader["Text"].ToString();

                        articles.Add(article);
                    }

                    newsletter.Articles = articles;
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error retrieving the newsletter model in the Newsletter Service", ex);
            }

            return newsletter;
        }

        /// <summary>
        /// Adds a newsletter to the database along with all it's articles
        /// </summary>
        /// <param name="newsletter"></param>
        /// <returns>true if newsletter was added successfully</returns>
        public bool AddNewsletter(Newsletter newsletter)
        {
            SqlConnection connection = new SqlConnection(dbConnectionString);
            string statementForNewsletter = string.Format(
                "INSERT INTO Newsletters ({0}, {1}, {2}, {3}, {4}) OUTPUT Inserted.NewsletterId VALUES ('{5}', {6}, '{7}', '{8}', {9})"
                , "CreatedDate"
                , "CreatedBy"
                , "Memo"
                , "PublishedDate"
                , "IsCurrent"
                , newsletter.CreatedDate
                , newsletter.CreatedBy
                , newsletter.Memo
                , newsletter.PublishedDate
                , newsletter.IsCurrent == true ? "1" : "0"
                );

            connection.Open();
            SqlCommand command = new SqlCommand(statementForNewsletter, connection);
            int insertedNewsletterId = (int)command.ExecuteNonQuery();

            foreach (var article in newsletter.Articles)
            {
                string statemetForArticle = string.Format(
                "INSERT INTO Articles ({0}, {1}) VALUES ({2}, '{3}')"
                , "NewsletterId"
                , "Text"
                , insertedNewsletterId
                , article.Text
                );

                command.CommandText = statemetForArticle;
                command.ExecuteNonQuery();
            }

            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }

            return true;
        }

        /// <summary>
        /// Updates a newsletterto the newsletter passed in by it's id 
        /// </summary>
        /// <param name="newsletter"></param>
        /// <returns>true if newsletter was updated successfully</returns>
        public bool EditNewsletter(Newsletter newsletter)
        {
            return true;
        }

        /// <summary>
        /// Deletes a newsletter entry from the database
        /// </summary>
        /// <param name="newsletterId"></param>
        /// <returns>true if newsletter was deleted successfully</returns>
        public bool DeleteNewsletter(int newsletterId)
        {
            return true;
        }


        //TEMPORARY
        //TODO - J - Remove once used.
        private bool AddMockNewsletter(int id, int numberOfArticles)
        {
            Newsletter newsletter = CreateMockNewsletter(id, numberOfArticles);

            return true;
        }

        //TEMPORARY
        //TODO - J - Remove once used.
        private Newsletter CreateMockNewsletter(int id, int numberOfArticles)
        {
            List<Article> articles = new List<Article>();
            string currentUser = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            int currentUserId = string.IsNullOrEmpty(currentUser) ? 1 : Int32.Parse(currentUser);

            for (int i = 0; i < numberOfArticles; i++)
            {
                Article toAdd = new Article()
                {
                    ArticleId = i,
                    NewsletterId = id,
                    Text = string.Format("This is mock article #{0}.", i.ToString())
                };
                articles.Add(toAdd);

            }
            Newsletter newsletter = new Newsletter()
            {
                NewsletterId = id,
                CreatedBy = currentUserId,
                CreatedDate = DateTime.Now,
                Memo = "This is a mock newsletter",
                PublishedDate = DateTime.Now,
                IsCurrent = true,
                Articles = articles
            };

            return newsletter;
        }
    }
}
