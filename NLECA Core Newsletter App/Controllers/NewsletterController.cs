using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLECA_Core_Newsletter_App.Models.Newsletter;
using NLECA_Core_Newsletter_App.Service.Interfaces;
using System.Collections.Generic;

namespace NLECA_Core_Newsletter_App.Controllers
{
    public class NewsletterController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        private readonly INewsletterService _newsletter;

        public NewsletterController(ILogger<HomeController> logger, IConfiguration config, INewsletterService newsletter)
        {
            _logger = logger;
            _config = config;
            _newsletter = newsletter;
        }

        [Authorize(Roles = "SuperAdmin,Admin,ReadOnlyUser")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "SuperAdmin,Admin,ReadOnlyUser")]
        public IActionResult NewsletterManager()
        {
            List<Newsletter> model = new List<Newsletter>();
            return View(model);
        }

        [Authorize(Roles = "SuperAdmin,Admin,ReadOnlyUser")]
        public IActionResult AlertManager()
        {
            List<Newsletter> model = new List<Newsletter>();
            return View(model);
        }
    }
}