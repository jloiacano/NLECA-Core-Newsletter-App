using Microsoft.AspNetCore.Identity;

namespace NLECA_Core_Newsletter_App.Data
{
    public class ApplicationIdentityUser : IdentityUser<int>
    {
        public string ContactName { get; set; }

    }
}
