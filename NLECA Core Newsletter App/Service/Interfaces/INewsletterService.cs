using NLECA_Core_Newsletter_App.Models.Newsletter;
using System.Collections.Generic;

namespace NLECA_Core_Newsletter_App.Service.Interfaces
{
    public interface INewsletterService
    {
        /// <summary>
        /// Adds a newsletter to the database
        /// </summary>
        /// <param name="newsletter"></param>
        /// <returns>true if newsletter was added successfully</returns>
        int AddNewsletter();

        /// <summary>
        /// Gets the most recent published newsletter
        /// </summary>
        /// <returns>The most recent published newsletter</returns>
        NewsletterModel GetPublishedNewsletter();

        /// <summary>
        /// Gets all the newsletters
        /// </summary>
        /// <returns>all the newsletters</returns>
        List<NewsletterModel> GetAllNewsletters();

        /// <summary>
        /// Gets a newsletter by it's id number
        /// </summary>
        /// <returns>the requested newsletter</returns>
        NewsletterModel GetNewsletterById(int newsletterId);

        /// <summary>
        /// Updates a newsletter to the newsletter passed in by it's id 
        /// </summary>
        /// <param name="newsletter"></param>
        /// <returns>true if newsletter was updated successfully</returns>
        bool UpdateNewsletter(NewsletterModel newsletter);

        /// <summary>
        /// Publishes the newsletter by newsletter id
        /// </summary>
        /// <param name="newsletterId">id of the newsletter to publish</param>
        /// <returns>true if the newsletter was published successfully</returns>
        bool PublishNewsletter(int newsletterId);

        /// <summary>
        /// Unpublishes the newsletter by newsletter id
        /// </summary>
        /// <param name="newsletterId">id of the newsletter to unpublish</param>
        /// <returns>true if the newsletter was unpublished successfully</returns>
        bool UnpublishNewsletter(int newsletterId);

        /// <summary>
        /// Deletes a newsletter entry from the database
        /// </summary>
        /// <param name="newsletterId"></param>
        /// <returns>true if newsletter was deleted successfully</returns>
        bool DeleteNewsletter(int newsletterId);
    }
}
