using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLECA_Core_Newsletter_App.Data;
using NLECA_Core_Newsletter_App.Models.Alert;
using NLECA_Core_Newsletter_App.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NLECA_Core_Newsletter_App.Controllers
{
    public class AlertController : Controller
    {
        private readonly ILogger<AlertController> _logger;
        private readonly IAlertService _alertService;
        private readonly UserManager<ApplicationIdentityUser> _userManager;

        public AlertController(ILogger<AlertController> logger, IAlertService articleService, UserManager<ApplicationIdentityUser> userManager)
        {
            _logger = logger;
            _alertService = articleService;
            _userManager = userManager;
        }

        [Authorize(Roles = "SuperAdmin,Admin,ReadOnlyUser")]
        public IActionResult Index()
        {

            return RedirectToAction("AlertManager");
        }

        [Authorize(Roles = "SuperAdmin,Admin,ReadOnlyUser")]
        public IActionResult AlertManager()
        {
            IEnumerable<AlertModel> alerts = _alertService.GetAllAlerts();
            return View(alerts);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult AddAlert()
        {
            AlertModel alert = new AlertModel(_userManager.GetUserId(this.User), this.User.Identity.Name);
            int alertId = _alertService.AddAlert(alert);

            return RedirectToAction("EditAlert", new { alertId });
        }

        [Authorize(Roles = "SuperAdmin,Admin,ReadOnlyUser")]
        public IActionResult EditAlert(int alertId)
        {
            AlertModel alert = _alertService.GetAlertById(alertId);

            return View(alert);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult UpdateAlert(AlertModel alert)
        {
            // TODO - J - validate data: dates order etc.
            bool successfullUpdate = _alertService.UpdateAlert(alert);

            return RedirectToAction("AlertManager");
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult PublishAlert(int alertId)
        {
            bool successfullPublish = _alertService.PublishAlert(alertId);

            return RedirectToAction("AlertManager");
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult UnpublishAlert(int alertId)
        {
            bool successfullUnpublish = _alertService.UnpublishAlert(alertId);

            return RedirectToAction("AlertManager");
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult DeleteAlert(int alertId)
        {
            bool successfullDelete = _alertService.DeleteAlert(alertId);

            return RedirectToAction("AlertManager");
        }

        public IActionResult AlertDetails(int alertId)
        {
            AlertModel alert = _alertService.GetAlertById(alertId);
            return View(alert);
        }

        public IActionResult HideAlert(int alertId)
        {
            try
            {
                string hiddenAlerts = HttpContext.Request.Cookies["HiddenAlerts"];
                IEnumerable<AlertModel> currentAlerts = _alertService.GetAllCurrentAlerts();
                List<int> currentAlertIds = currentAlerts.Select(x => x.AlertId).ToList();
                var maxDateTime = currentAlerts.OrderByDescending(x => x.AlertDateEnd).First().AlertDateEnd;

                if (string.IsNullOrEmpty(hiddenAlerts) == false)
                {
                    List<string> hiddenAlertIds = hiddenAlerts.Split('-').ToList();
                    List<string> idsToRemove = new List<string>();
 
                    foreach (string id in hiddenAlertIds)
                    {
                        if (currentAlertIds.Contains(Int32.Parse(id)) == false)
                        {
                            idsToRemove.Add(id);
                        }
                    }
                    foreach (string id in idsToRemove)
                    {
                        hiddenAlertIds.Remove(id);
                    }
                    hiddenAlertIds.Add(alertId.ToString());
                    hiddenAlerts = String.Join("-", hiddenAlertIds);
                }
                else
                {
                    hiddenAlerts = alertId.ToString();
                }

                return Json(new
                {
                    success = true,
                    responseText = "Alert successfully hidden",
                    newCookie = hiddenAlerts,
                    cookieExpiration = maxDateTime
                });
            }
            catch (Exception ex)
            {
                string error = "There was an error adding the alert to HideAlerts cookie.";
                _logger.LogError(error, ex);
                return Json(new { success = false, responseText = error });
            }
        }
    }
}