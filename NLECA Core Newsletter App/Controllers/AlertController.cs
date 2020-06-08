using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLECA_Core_Newsletter_App.Data;
using NLECA_Core_Newsletter_App.Models.Alert;
using NLECA_Core_Newsletter_App.Service.Interfaces;

namespace NLECA_Core_Newsletter_App.Controllers
{
    public class AlertController : Controller
    {
        private readonly IAlertService _alertService;
        private readonly UserManager<ApplicationIdentityUser> _userManager;

        public AlertController(IAlertService articleService, UserManager<ApplicationIdentityUser> userManager)
        {
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
    }
}