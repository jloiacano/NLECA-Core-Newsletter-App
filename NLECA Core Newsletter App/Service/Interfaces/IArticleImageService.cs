using NLECA_Core_Newsletter_App.Models.Newsletter;
using System.Collections;
using System.Collections.Generic;

namespace NLECA_Core_Newsletter_App.Service.Interfaces
{
    public interface IArticleImageService
    {
        /// <summary>
        /// Checks database to see if image has already been uploaded (regardless of file name)
        /// </summary>
        /// <param name="articleImage"></param>
        /// <returns>true if article image has already been uploaded</returns>
        public bool ExistsInAritcleImages(string simpleCheckSum);

        /// <summary>
        /// Gets all the Article Images which are currently in Articles
        /// </summary>
        /// <returns>An IEnumerable of the article images and properties</returns>
        public IEnumerable<ArticleImageInArticleModel> GetArticleImagesInArticles();

        /// <summary>
        /// Sets "IsCurrent" to false in the ArticleImages table.
        /// Does NOT remove the image from previous articles or the file system.
        /// </summary>
        /// <param name="imageLocation">The file hierarchy of the image location in the server</param>
        /// <returns>true if image has been successfully removed</returns>
        public bool RemoveArticleImage(string imageLocation);

        /// <summary>
        /// Gets all images in the article images table regardless of IsCurrent status
        /// or if the image is actually used in any articles
        /// </summary>
        /// <returns>An IEnumerable of the article images and properties</returns>
        public IEnumerable<ArticleImageInArticleModel> GetAllArticleImages();

        /// <summary>
        /// Removes the article image from the ArticleImages table.
        /// Removes the ImageFileLoction and sets ArticleType to 0 for any articles which
        /// contain the image.
        /// Deletes the image at the specified location from the file system hierarchy.
        /// </summary>
        /// <param name="imageLocation">The file hierarchy of the image location in the server</param>
        /// <returns>true if image has been successfully deleted</returns>
        public bool DeleteArticleImage(string imageLocation);

        /// <summary>
        /// Gets Articles with the same simple checksum
        /// </summary>
        /// <param name="simpleCheckSum"></param>
        /// <returns></returns>
        public IEnumerable<ArticleImageInArticleModel> GetArticleImagesWithSameCheckSum(string simpleCheckSum);

        /// <summary>
        /// Uploads the ArticleImage to the system file structure and lists its properties in the database.
        /// </summary>
        /// <param name="articleImage"></param>
        /// <returns></returns>
        public bool UploadArticleImage(ArticleImageModel articleImage);
    }
}
