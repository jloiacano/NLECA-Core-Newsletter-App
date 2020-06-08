using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLECA_Core_Newsletter_App.Data;
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

            return View();
        }
    }
}