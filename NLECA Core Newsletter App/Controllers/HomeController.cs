using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLECA_Core_Newsletter_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NLECA_Core_Newsletter_App.Models;

namespace NLECA_Core_Newsletter_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            PrivacyViewModel model = new PrivacyViewModel();
            string connectionString = _config.GetConnectionString("DefaultConnection");
            model.Exception = CheckIfDatabaseExists(connectionString);

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private string CheckIfDatabaseExists(string connectionString)
        {
            string toReturn = "no database";

            try
            {
                Microsoft.Data.SqlClient.SqlConnection temporaryConnection = new Microsoft.Data.SqlClient.SqlConnection(connectionString);

                using (temporaryConnection)
                {
                    temporaryConnection.Open();
                    temporaryConnection.Close();
                    toReturn = "connected....";
                }

            }
            catch (System.Exception ex)
            {
                return "there was an exception! " + ex.ToString();
            }
            return toReturn;
        }
    }
}
