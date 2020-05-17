using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NLECA_Core_Newsletter_App.Data
{
    public class NLECAUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationIdentityUser, ApplicationRole>
    {
        public NLECAUserClaimsPrincipalFactory(
            UserManager<ApplicationIdentityUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationIdentityUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("ContactName", user.ContactName ?? ""));
            return identity;
        }
    }
}