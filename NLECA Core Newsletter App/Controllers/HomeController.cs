using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLECA_Core_Newsletter_App.Models;
using NLECA_Core_Newsletter_App.Models.Newsletter;
using NLECA_Core_Newsletter_App.Service.Interfaces;
using System.Diagnostics;
using System;
using Microsoft.AspNetCore.Authorization;

namespace NLECA_Core_Newsletter_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        private readonly INewsletterService _newsletter;

        public HomeController(ILogger<HomeController> logger, IConfiguration config, INewsletterService newsletter)
        {
            _logger = logger;
            _config = config;
            _newsletter = newsletter;
        }

        public IActionResult Index()
        {
            NewsletterModel model = new NewsletterModel();

            try
            {
                model = _newsletter.GetPublishedNewsletter();
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error retrieving the newsletter model in the Home Controller", ex);
            }

            return View(model);
        }

        [Authorize(Roles = "SuperAdmin,Admin,ReadOnlyUser")]
        public IActionResult Privacy()
        {
            PrivacyViewModel model = new PrivacyViewModel();
            model.Message = "This is the privacy page.";

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}