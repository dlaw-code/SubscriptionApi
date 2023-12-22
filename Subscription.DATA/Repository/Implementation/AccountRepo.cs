using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Subscription.DATA.Context;
using Subscription.DATA.Repository.Interface;
using Subscription.MODEL.Entities;

namespace Subscription.DATA.Repository.Implementation
{
    public class AccountRepo : IAccountRepo
    {

        private readonly UserManager<ServiceUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SubscriptionContextDb _context;

        public AccountRepo(UserManager<ServiceUser> userManager, RoleManager<IdentityRole> roleManager, SubscriptionContextDb context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<bool> CheckAccountPassword(ServiceUser user, string password)
        {
            var checkUserPassword = await _userManager.CheckPasswordAsync(user, password);
            return checkUserPassword;
        }

        

        public async Task<ServiceUser> FindUserByServiceIdAsync(string Serviceid)
        {
            var findUser = await _context.Users.FirstOrDefaultAsync(u=> u.ServiceId == Serviceid);
            return findUser;
        }

        public async Task<ServiceUser> SignUpAsync(ServiceUser user, string Password)
        {
            var result = await _userManager.CreateAsync(user, Password);
            if (result.Succeeded)
            {
                return user;
            }
            return null;
        }

     
    }
}
