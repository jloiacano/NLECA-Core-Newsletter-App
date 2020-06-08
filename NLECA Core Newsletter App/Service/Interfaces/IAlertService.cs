using NLECA_Core_Newsletter_App.Models.Alert;
using System.Collections;
using System.Collections.Generic;

namespace NLECA_Core_Newsletter_App.Service.Interfaces
{
    public interface IAlertService
    {
        /// <summary>
        /// Adds an alert to the database
        /// </summary>
        /// <param name="alertModel">AlertModel to be added</param>
        /// <returns>true if AlertModel was successfully added to database</returns>
        bool AddAlert(AlertModel alertModel);

        /// <summary>
        /// Gets a single alert, and its details, from the database
        /// </summary>
        /// <param name="AlertId">The Id of the Alert</param>
        /// <returns>A single AlertModel with all the alert details</returns>
        AlertModel GetAlertById(int AlertId);

        /// <summary>
        /// Gets all of the Alerts in the database
        /// </summary>
        /// <returns>All of the Alerts in the database</returns>
        IEnumerable<AlertModel> GetAllAlerts();

        /// <summary>
        /// Gets all of the Alerts in the database which have been published
        /// </summary>
        /// <returns>All of the Alerts in the database which have been published</returns>
        IEnumerable<AlertModel> GetAllPublishedAlerts();

        /// <summary>
        /// Gets all of the Alerts in the database occuring after now
        /// </summary>
        /// <returns>All of the Alerts in the database occuring after now</returns>
        IEnumerable<AlertModel> GetAllFutureAlerts();

        /// <summary>
        /// Updates an existing AlertModel in the database
        /// </summary>
        /// <param name="alertModel">AlertModel with the updates</param>
        /// <returns>true if AlertModel was successfully updated</returns>
        bool UpdateAlert(AlertModel alertModel);

        /// <summary>
        /// Publishes the Alert by its AlertId
        /// </summary>
        /// <param name="alertId">Id of the Alert to publish</param>
        /// <returns>true if the alert was successfully published</returns>
        bool PublishAlert(int alertId);

        /// <summary>
        /// Unpublishes the Alert by its AlertId
        /// </summary>
        /// <param name="alertId">Id of the Alert to unpublish</param>
        /// <returns>true if the alert was successfully unpublishted</returns>
        bool UnpublishAlert(int alertId);

        /// <summary>
        /// Deletes the Alert by its AlertId
        /// </summary>
        /// <param name="alertId">Id of the Alert to delete</param>
        /// <returns>true if the alert was successfully deleted</returns>
        bool DeleteAlert(int alertId);

    }
}
