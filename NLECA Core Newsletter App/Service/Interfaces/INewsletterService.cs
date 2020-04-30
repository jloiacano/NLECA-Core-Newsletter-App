using NLECA_Core_Newsletter_App.Models.Newsletter;
using System.Collections.Generic;

namespace NLECA_Core_Newsletter_App.Service.Interfaces
{
    public interface INewsletterService
    {

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
        /// Adds a newsletter to the database
        /// </summary>
        /// <param name="newsletter"></param>
        /// <returns>true if newsletter was added successfully</returns>
        int AddNewsletter(NewsletterModel newsletter);

        /// <summary>
        /// Updates a newsletter to the newsletter passed in by it's id 
        /// </summary>
        /// <param name="newsletter"></param>
        /// <returns>true if newsletter was updated successfully</returns>
        bool UpdateNewsletter(NewsletterModel newsletter);

        /// <summary>
        /// Deletes a newsletter entry from the database
        /// </summary>
        /// <param name="newsletterId"></param>
        /// <returns>true if newsletter was deleted successfully</returns>
        bool DeleteNewsletter(int newsletterId);
    }
}
