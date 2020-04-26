using NLECA_Core_Newsletter_App.Models.Newsletter;

namespace NLECA_Core_Newsletter_App.Service.Interfaces
{
    public interface INewsletterService
    {

        /// <summary>
        /// Gets the most recent published newsletter
        /// </summary>
        /// <returns>The most recent published newsletter</returns>
        Newsletter GetNewsletter();

        /// <summary>
        /// Adds a newsletter to the database along with all it's articles
        /// </summary>
        /// <param name="newsletter"></param>
        /// <returns>true if newsletter was added successfully</returns>
        bool AddNewsletter(Newsletter newsletter);

        /// <summary>
        /// Updates a newsletterto the newsletter passed in by it's id 
        /// </summary>
        /// <param name="newsletter"></param>
        /// <returns>true if newsletter was updated successfully</returns>
        bool EditNewsletter(Newsletter newsletter);

        /// <summary>
        /// Deletes a newsletter entry from the database
        /// </summary>
        /// <param name="newsletterId"></param>
        /// <returns>true if newsletter was deleted successfully</returns>
        bool DeleteNewsletter(int newsletterId);
    }
}
