using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Identity.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace NLECA_Core_Newsletter_App.Data
{
    public class ApplicationIdentityUserManager : UserManager<ApplicationIdentityUser>
    {
        public ApplicationIdentityUserManager(
            IUserStore<ApplicationIdentityUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<ApplicationIdentityUser> passwordHasher,
            IEnumerable<IUserValidator<ApplicationIdentityUser>> userValidators,
            IEnumerable<IPasswordValidator<ApplicationIdentityUser>> passwordValidators,
            ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors,
            IServiceProvider services, ILogger<UserManager<ApplicationIdentityUser>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {

        }

        public async Task<string> GetContactNameAsync(ApplicationIdentityUser user)
        {
            var currentUser = await base.FindByNameAsync(user.UserName);
            return currentUser.ContactName;
        }

        public async Task<IdentityResult> SetContactNameAsync(ApplicationIdentityUser user, string contactName)
        {
            user.ContactName = contactName;
            return await UpdateUserAsync(user);
        }
    }
}