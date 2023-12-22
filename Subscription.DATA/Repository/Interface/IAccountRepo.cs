using Subscription.MODEL.Entities;

namespace Subscription.DATA.Repository.Interface
{
    public interface IAccountRepo
    {
        Task<ServiceUser> SignUpAsync(ServiceUser user, string Password);

        Task<bool> CheckAccountPassword(ServiceUser user, string password);

        Task<ServiceUser> FindUserByServiceIdAsync(string Serviceid);
    }
}
