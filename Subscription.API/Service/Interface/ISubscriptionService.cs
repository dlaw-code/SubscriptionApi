using Subscription.MODEL.DTO;

namespace Subscription.API.Service.Interface
{
    public interface ISubscriptionService
    {
        Task<ResponseDto<SubscriptionDTO>> SubscribeAsync(SubscribeDto subscribe);
        Task<ResponseDto<SubscriptionStatus>> SubscribeStatusAsync(SubscribeDto status);
        Task<ResponseDto<UnSubcribeDTO>> UnSubscribeAsync(SubscribeDto unsubscribe);
    }
}
