using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NLECA_Core_Newsletter_App.Models;

namespace NLECA_Core_Newsletter_App.ViewComponents
{
    public class HeaderMenuViewComponent : ViewComponent
    {
        private readonly IConfiguration config;
        private readonly string dbExists;

        public HeaderMenuViewComponent(IConfiguration config)
        {
            this.config = config;
            string connectionString = config.GetConnectionString("DefaultConnection");
            this.dbExists = CheckIfDatabaseExists(connectionString);
        }
        public IViewComponentResult Invoke()
        {
            HeaderMenuModel headerMenu = new HeaderMenuModel()
            {
                BrandName = this.dbExists
            };

            return View(headerMenu);
        }

        private string CheckIfDatabaseExists(string connectionString)
        {
            string toReturn = "no database";

            try
            {
                SqlConnection temporaryConnection = new SqlConnection(connectionString);

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
