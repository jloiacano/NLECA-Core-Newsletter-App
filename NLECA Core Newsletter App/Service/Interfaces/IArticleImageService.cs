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

        public IEnumerable<ArticleImageInArticleModel> GetArticleImagesInArticles();
        public IEnumerable<ArticleImageInArticleModel> GetAllArticleImages();

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
