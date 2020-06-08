using Microsoft.Extensions.Logging;
using NLECA_Core_Newsletter_App.Models.Alert;
using NLECA_Core_Newsletter_App.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace NLECA_Core_Newsletter_App.Service.Services
{
    public class AlertService : IAlertService
    {
        private readonly ILogger<AlertService> _logger;
        private readonly ISQLHelperService _sql;

        public AlertService(ILogger<AlertService> logger, ISQLHelperService sql)
        {
            _logger = logger;
            _sql = sql;
        }

        public int AddAlert(AlertModel alertModel)
        {
            int newAlertId = 0;

            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@addedByUserId", alertModel.AddedByUserId)
                    ,new SqlParameter("@addedByUserName", alertModel.AddedByUserName)

                };
                newAlertId = _sql.GetReturnValueFromStoredProcedure("AddAlert", parameters);
            }
            catch (Exception ex)
            {
                string error = string.Format(
                    "There was an error entering Alert #{0} into database AlertService/AddAlert",
                    alertModel.AlertId);
                _logger.LogError(error, ex);
            }

            return newAlertId;
        }

        public AlertModel GetAlertById(int alertId)
        {
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@alertId", alertId) };
            DataSet GetAlertByIdResult = _sql.GetDatasetFromStoredProcedure("GetAlertById", parameters);

            try
            {
                DataRow alertResult = GetAlertByIdResult.Tables[0].AsEnumerable().FirstOrDefault();

                return new AlertModel(alertResult);
            }
            catch (Exception ex)
            {
                string error = string.Format(
                   "There was an error retrieving the AlertModel #{0} in AlertService/GetAlertById",
                   alertId.ToString());
                _logger.LogError(error, ex);
            }

            return null;
        }

        public IEnumerable<AlertModel> GetAllAlerts()
        {
            DataSet alertsDataSet = _sql.GetDatasetFromStoredProcedure("GetAllAlerts");

            List<AlertModel> alerts = new List<AlertModel>();

            try
            {
                IEnumerable<DataRow> alertResults = alertsDataSet.Tables[0].AsEnumerable();

                foreach (var alertResult in alertResults)
                {
                    AlertModel alertModel = new AlertModel(alertResult);
                    alerts.Add(alertModel);
                }
            }
            catch (Exception ex)
            {
                string error = string.Format(
                    "There was an error retrieving all Alerts in AlertService/GetAllAlerts");
                _logger.LogError(error, ex);
            }
            return alerts.AsEnumerable();
        }

        public IEnumerable<AlertModel> GetAllFutureAlerts()
        {
            DataSet alertsDataSet = _sql.GetDatasetFromStoredProcedure("GetAllFutureAlerts");

            List<AlertModel> alerts = new List<AlertModel>();

            try
            {
                IEnumerable<DataRow> alertResults = alertsDataSet.Tables[0].AsEnumerable();

                foreach (var alertResult in alertResults)
                {
                    AlertModel alertModel = new AlertModel(alertResult);
                    alerts.Add(alertModel);
                }
            }
            catch (Exception ex)
            {
                string error = string.Format(
                    "There was an error retrieving all Alerts in AlertService/GetAllFutureAlerts");
                _logger.LogError(error, ex);
            }
            return alerts.AsEnumerable();
        }

        public IEnumerable<AlertModel> GetAllPublishedAlerts()
        {
            DataSet alertsDataSet = _sql.GetDatasetFromStoredProcedure("GetAllPublishedAlerts");

            List<AlertModel> alerts = new List<AlertModel>();

            try
            {
                IEnumerable<DataRow> alertResults = alertsDataSet.Tables[0].AsEnumerable();

                foreach (var alertResult in alertResults)
                {
                    AlertModel alertModel = new AlertModel(alertResult);
                    alerts.Add(alertModel);
                }
            }
            catch (Exception ex)
            {
                string error = string.Format(
                    "There was an error retrieving all Alerts in AlertService/GetAllPublishedAlerts");
                _logger.LogError(error, ex);
            }
            return alerts.AsEnumerable();
        }

        public IEnumerable<AlertModel> GetAllCurrentAlerts()
        {
            DataSet alertsDataSet = _sql.GetDatasetFromStoredProcedure("GetAllCurrentAlerts");

            List<AlertModel> alerts = new List<AlertModel>();

            try
            {
                IEnumerable<DataRow> alertResults = alertsDataSet.Tables[0].AsEnumerable();

                foreach (var alertResult in alertResults)
                {
                    AlertModel alertModel = new AlertModel(alertResult);
                    alerts.Add(alertModel);
                }
            }
            catch (Exception ex)
            {
                string error = string.Format(
                    "There was an error retrieving all Alerts in AlertService/GetAllCurrentAlerts");
                _logger.LogError(error, ex);
            }
            return alerts.AsEnumerable();
        }

        public bool UpdateAlert(AlertModel alertModel)
        {
            int rowseffected = 0;

            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@alertId", alertModel.AlertId)
                    ,new SqlParameter("@alertTitle", alertModel.AlertTitle)
                    ,new SqlParameter("@alertDate", _sql.ConvertDateTimeForSQL(alertModel.AlertDate))
                    ,new SqlParameter("@alertDateEnd", _sql.ConvertDateTimeForSQL(alertModel.AlertDateEnd))
                    ,new SqlParameter("@alertShortDetails", alertModel.AlertShortDetails)
                    ,new SqlParameter("@alertLongDetails", alertModel.AlertLongDetails)
                    ,new SqlParameter("@alertImageLocation", alertModel.AlertImageLocation)
                };
                rowseffected = _sql.GetReturnValueFromStoredProcedure("UpdateAlert", parameters);
            }
            catch (Exception ex)
            {
                string error = string.Format(
                    "There was an error updating Alert #{0} into database AlertService/UpdateAlert",
                    alertModel.AlertId);
                _logger.LogError(error, ex);
            }

            return rowseffected > 0;
        }

        public bool PublishAlert(int alertId)
        {
            int rowseffected = 0;
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@alertId", alertId)
                };

                rowseffected = _sql.GetReturnValueFromStoredProcedure("PublishAlert", parameters);
            }
            catch (Exception ex)
            {
                string error = string.Format(
                    "There was an error publishing Alert #{0} in AlertService/PublishAlert",
                    alertId);
                _logger.LogError(error, ex);
            }

            return rowseffected > 0;
        }

        public bool UnpublishAlert(int alertId)
        {
            int rowseffected = 0;
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@alertId", alertId)
                };

                rowseffected = _sql.GetReturnValueFromStoredProcedure("UnpublishAlert", parameters);
            }
            catch (Exception ex)
            {
                string error = string.Format(
                    "There was an error unpublishing Alert #{0} in AlertService/UnpublishAlert",
                    alertId);
                _logger.LogError(error, ex);
            }

            return rowseffected > 0;
        }

        public bool DeleteAlert(int alertId)
        {
            int rowseffected = 0;
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@alertId", alertId)
                };

                rowseffected = _sql.GetReturnValueFromStoredProcedure("DeleteAlert", parameters);
            }
            catch (Exception ex)
            {
                string error = string.Format(
                    "There was an error deleting Alert #{0} in AlertService/DeleteAlert",
                    alertId);
                _logger.LogError(error, ex);
            }

            return rowseffected > 0;
        }
    }
}
