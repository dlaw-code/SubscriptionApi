using Subscription.MODEL.Entities;

namespace Subscription.API.Service.Interface
{
    public interface IGenerateJwt
    {
        Task<string> GenerateToken(ServiceUser user);
    }
}
