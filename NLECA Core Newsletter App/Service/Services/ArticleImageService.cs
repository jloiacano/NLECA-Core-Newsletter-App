using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NLECA_Core_Newsletter_App.Models.Newsletter;
using NLECA_Core_Newsletter_App.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace NLECA_Core_Newsletter_App.Service.Services
{
    public class ArticleImageService : IArticleImageService
    {
        private readonly ILogger<ArticleImageService> _logger;
        private readonly ISQLHelperService _sql;

        public ArticleImageService(ILogger<ArticleImageService> logger, ISQLHelperService sql)
        {
            _logger = logger;
            _sql = sql;
        }

        public IEnumerable<ArticleImageInArticleModel> GetArticleImagesWithSameCheckSum(string simpleCheckSum)
        {
            SqlParameter[] parameters = { new SqlParameter("@simpleCheckSum", simpleCheckSum) };
            DataSet MatchingArticleImages = _sql.GetDatasetFromStoredProcedure("GetArticleImagesWithSameCheckSum", parameters);

            List<ArticleImageInArticleModel> images = new List<ArticleImageInArticleModel>();

            try
            {
                IEnumerable<DataRow> articleImageResults = MatchingArticleImages.Tables[0].AsEnumerable();

                foreach (var articleImageResult in articleImageResults)
                {
                    ArticleImageInArticleModel image = new ArticleImageInArticleModel(articleImageResult);
                    images.Add(image);
                }
            }
            catch (Exception ex)
            {
                string error = string.Format(
                    "There was an error checking for CheckSum {0} in ArticleImageService/GetArticleImagesWithSameCheckSum"
                    , simpleCheckSum);
                _logger.LogError(error, ex);
            }
            return images.AsEnumerable();
        }


        public IEnumerable<ArticleImageInArticleModel> GetArticleImagesInArticles()
        {
            DataSet articleImagesDataSet = _sql.GetDatasetFromStoredProcedure("GetArticleImagesInArticles");

            List<ArticleImageInArticleModel> images = new List<ArticleImageInArticleModel>();

            try
            {
                IEnumerable<DataRow> articleImageResults = articleImagesDataSet.Tables[0].AsEnumerable();

                foreach (var articleImageResult in articleImageResults)
                {
                    ArticleImageInArticleModel image = new ArticleImageInArticleModel(articleImageResult);
                    images.Add(image);
                }
            }
            catch (Exception ex)
            {
                string error = string.Format(
                    "There was an error retrieving images in ArticleImageService/GetArticleImagesInArticles");
                _logger.LogError(error, ex);
            }
            return images.AsEnumerable();
        }
        public IEnumerable<ArticleImageInArticleModel> GetAllArticleImages()
        {
            DataSet articleImagesDataSet = _sql.GetDatasetFromStoredProcedure("GetAllArticleImages");

            List<ArticleImageInArticleModel> images = new List<ArticleImageInArticleModel>();

            try
            {
                IEnumerable<DataRow> articleImageResults = articleImagesDataSet.Tables[0].AsEnumerable();

                foreach (var articleImageResult in articleImageResults)
                {
                    ArticleImageInArticleModel image = new ArticleImageInArticleModel(articleImageResult);
                    images.Add(image);
                }
            }
            catch (Exception ex)
            {
                string error = string.Format(
                    "There was an error retrieving images in ArticleImageService/GetArticleImagesInArticles");
                _logger.LogError(error, ex);
            }
            return images.AsEnumerable();
        }

        public bool ExistsInAritcleImages(string simpleCheckSum)
        {
            SqlParameter[] parameters = { new SqlParameter("@simpleCheckSum", simpleCheckSum) };
            int numberOfRows = _sql.GetReturnValueFromStoredProcedure("CheckIfArticleImageExists", parameters);

            return numberOfRows > 0;
        }


        public bool UploadArticleImage(ArticleImageModel image)
        {
            // Upload file to file structure
            string imageLocation = SaveFile(image);
            // Add entry in database with file location and checksum
            bool success = EnterImageIntoDatabase(image);
            return success;
        }


        private string SaveFile(ArticleImageModel image)
        {
            string path = "";
            try
            {
                path = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot\\Images\\ArticleImages",
                    image.StoredImageName);

                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    image.ImageFile.CopyTo(fileStream);
                }
            }
            catch (Exception ex)
            {
                string error = string.Format("There was an error Saving {0} in ArticleImageService/SaveFile", image.ImageName);
                _logger.LogError(error, ex);
            }

            return path;
        }

        private bool EnterImageIntoDatabase(ArticleImageModel image)
        {
            int rowseffected = 0;
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
{
                    new SqlParameter("@uploadedByUserId", image.UploadedByUserId),
                    new SqlParameter("@uploadedByUserName", image.UploadedByUserName),
                    new SqlParameter("@uploadedDate", _sql.ConvertDateTimeForSQL(DateTime.Now)),
                    new SqlParameter("@simpleCheckSum", image.SimpleCheckSum),
                    new SqlParameter("@imageName", image.ImageName),
                    new SqlParameter("@imageLocation", image.ImageLocation)
};

                rowseffected = _sql.GetReturnValueFromStoredProcedure("AddImageToArticleImages", parameters);
            }
            catch (Exception ex)
            {
                string error = string.Format(
                    "There was an error entering {0} into database ArticleImageService/EnterImageIntoDatabase",
                    image.ImageName);
                _logger.LogError(error, ex);
            }
            return rowseffected > 0;
        }
    }
}