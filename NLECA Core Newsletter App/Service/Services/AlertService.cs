using Microsoft.Extensions.Logging;
using NLECA_Core_Newsletter_App.Models.Alert;
using NLECA_Core_Newsletter_App.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public bool AddAlert(AlertModel alertModel)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAlert(int alertId)
        {
            throw new NotImplementedException();
        }

        public AlertModel GetAlertById(int AlertId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AlertModel> GetAllAlerts()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AlertModel> GetAllFutureAlerts()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AlertModel> GetAllPublishedAlerts()
        {
            throw new NotImplementedException();
        }

        public bool PublishAlert(int alertId)
        {
            throw new NotImplementedException();
        }

        public bool UnpublishAlert(int alertId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateAlert(AlertModel alertModel)
        {
            throw new NotImplementedException();
        }
    }
}
