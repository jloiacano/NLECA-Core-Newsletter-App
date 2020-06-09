using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NLECA_Core_Newsletter_App.Models.Alert;
using NLECA_Core_Newsletter_App.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NLECA_Core_Newsletter_App.ViewComponents
{
    public class AlertsViewComponent : ViewComponent
    {
        private readonly IAlertService _alerts;
        private List<int> hiddenAlerts = new List<int>();

        public AlertsViewComponent(IAlertService alertService)
        {
            _alerts = alertService;
        }
        public IViewComponentResult Invoke()
        {
            if (HttpContext.Request.Cookies["HiddenAlerts"] != null)
            {
                hiddenAlerts = HttpContext.Request.Cookies["HiddenAlerts"].Split('-')
                    .Select(Int32.Parse).ToList();
            }

            IEnumerable<AlertModel> currentAlerts = _alerts.GetAllCurrentAlerts()
                .Where(x => hiddenAlerts.Contains(x.AlertId) == false);
            return View("Default", currentAlerts);
        }
    }
}
