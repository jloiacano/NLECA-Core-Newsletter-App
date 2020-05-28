using NLECA_Core_Newsletter_App.Service.Services;

namespace NLECA_Core_Newsletter_App.Service.Interfaces
{
    public interface IArticleImageService
    {
        /// <summary>
        /// Checks database to see if image has already been uploaded (regardless of file name)
        /// </summary>
        /// <param name="articleImage"></param>
        /// <returns>true if article image has already been uploaded</returns>
        public bool ExistsInAritcleImages(ArticleImage articleImage);

        /// <summary>
        /// Uploads the ArticleImage to the system file structure and lists its properties in the database.
        /// </summary>
        /// <param name="articleImage"></param>
        /// <returns></returns>
        public bool UploadArticleImage(ArticleImage articleImage);
    }
}
