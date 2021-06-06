using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLECA_Core_Newsletter_App.Data;
using NLECA_Core_Newsletter_App.Service.Interfaces;

namespace NLECA_Core_Newsletter_App.Controllers
{
    public class UserAdminController : Controller
    {

        private readonly ILogger<UserAdminController> _logger;
        private readonly IUserAdminService _userAdminService;
        private readonly UserManager<ApplicationIdentityUser> _userManager;

        public UserAdminController(
            ILogger<UserAdminController> logger
            , IUserAdminService userAdminService
            , UserManager<ApplicationIdentityUser> userManager
            )
        {
            _logger = logger;
            _userAdminService = userAdminService;
            _userManager = userManager;
        }

        [Authorize(Roles = "SuperAdmin,Admin,ReadOnlyUser")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
