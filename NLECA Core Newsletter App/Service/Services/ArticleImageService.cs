using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLECA_Core_Newsletter_App.Models.Newsletter;
using NLECA_Core_Newsletter_App.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;

namespace NLECA_Core_Newsletter_App.Service.Services
{
    public class ArticleImageService : IArticleImageService
    {
        private IConfiguration _configuration;
        private string AzureConncetionString { get; }
        private readonly IAzureStorageService _azureStorageService;
        private readonly ILogger<ArticleImageService> _logger;
        private readonly ISQLHelperService _sql;
        private readonly string _wwwRoot;

        public ArticleImageService(
            IConfiguration configuration
            , IAzureStorageService azureStorageService
            , ILogger<ArticleImageService> logger
            , ISQLHelperService sql
            , IWebHostEnvironment env)
        {
            _configuration = configuration;
            AzureConncetionString = _configuration["AzureStorageConnectionString"];
            _azureStorageService = azureStorageService;
            _logger = logger;
            _sql = sql;
            _wwwRoot = env.ContentRootPath;
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
            bool success;

            try
            {
                var container = _azureStorageService.GetBlobContainer(AzureConncetionString, "article-images");
                var content = ContentDispositionHeaderValue.Parse(image.ImageFile.ContentDisposition);
                var fileName = content.FileName.Trim('"');

                // Get a reference to a Block Blob
                var blockBlob = container.GetBlockBlobReference(fileName);
                success = blockBlob.UploadFromStreamAsync(image.ImageFile.OpenReadStream()).IsCompletedSuccessfully;
                EnterImageIntoDatabase(image);

            }
            catch (Exception ex)
            {
                _logger.LogError("Error in ArticleImageService/UploadArticleImage", ex);
                throw ex;
            }

            return success;
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

        public bool RemoveArticleImage(string imageLocation)
        {
            int rowseffected = 0;
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@imageLocation", imageLocation)
                };

                rowseffected = _sql.GetReturnValueFromStoredProcedure("RemoveArticleImage", parameters);
            }
            catch (Exception ex)
            {
                string error = string.Format(
                    "There was an error removing the image at {0} in ArticleImageService/RemoveArticleImage",
                    imageLocation);
                _logger.LogError(error, ex);
            }
            return rowseffected > 0;
        }

        public bool DeleteArticleImage(string imageLocation)
        {
            bool imageDeletedFromSystem = false;
            try
            {
                IEnumerable<string> directories = imageLocation.Split('/').Where(item => string.Compare(item, "..", true) != 0);
                string fullImagePath = _wwwRoot + "\\wwwroot";

                foreach (var directory in directories)
                {
                    fullImagePath += "\\" + directory;
                }

               
                if (File.Exists(fullImagePath))
                {
                    File.Delete(fullImagePath);
                    imageDeletedFromSystem = true;
                }
            }
            catch (Exception ex)
            {
                string error = string.Format(
                    "There was an error deleting the image at {0} from the SYSTEM in ArticleImageService/DeleteArticleImage",
                    imageLocation);
                _logger.LogError(error, ex);
            }

            int rowseffected = 0;
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@imageLocation", imageLocation)
                };

                rowseffected = _sql.GetReturnValueFromStoredProcedure("DeleteArticleImage", parameters);
            }
            catch (Exception ex)
            {
                string error = string.Format(
                    "There was an error deleting the image at {0} from the DATABASE in ArticleImageService/DeleteArticleImage",
                    imageLocation);
                _logger.LogError(error, ex);
            }
            bool success = imageDeletedFromSystem && rowseffected > 0;
            return success;
        }
    }
}