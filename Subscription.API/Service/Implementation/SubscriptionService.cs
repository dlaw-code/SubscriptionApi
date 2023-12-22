using Subscription.API.Service.Interface;
using Subscription.DATA.Repository.Interface;
using Subscription.MODEL.DTO;
using Subscription.MODEL.Entities;
using Subscription.MODEL.Enum;

namespace Subscription.API.Service.Implementation
{

    public class SubscriptionService : ISubscriptionService
    {
        private readonly IAccountRepo _accountRepo;
        private readonly ISubscriptionRepo _subscriptionRepo;
        private readonly ILogger<SubscriptionService> _logger;

        public SubscriptionService(IAccountRepo accountRepo, ISubscriptionRepo subscriptionRepo, ILogger<SubscriptionService> logger)
        {
            _accountRepo = accountRepo;
            _subscriptionRepo = subscriptionRepo;
            _logger = logger;
        }
        public async Task<ResponseDto<SubscriptionDTO>> SubscribeAsync(SubscribeDto subscribe)
        {
            var response = new ResponseDto<SubscriptionDTO>();
            try
            {
                var checkService = await _accountRepo.FindUserByServiceIdAsync(subscribe.ServiceId);
                if (checkService == null)
                {
                    response.ErrorMessages = new List<string>() { "Invalid service id" };
                    response.StatusCode = 404;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var checkSub = await _subscriptionRepo.GetSubscriptionbyServiceUserId(checkService.Id);
                if (checkSub != null)
                {
                    if (checkSub.SubscriptionStatus == Status.Subscribe.ToString())
                    {
                        response.ErrorMessages = new List<string>() { $"Service Id already subscribed on {checkSub.SubcribeDate}" };
                        response.StatusCode = 400;
                        response.DisplayMessage = "Error";
                        return response;
                    }
                    if (checkSub.PhoneNumber != subscribe.PhoneNumber)
                    {

                        checkSub.PhoneNumber = subscribe.PhoneNumber;

                    }
                    checkSub.SubcribeDate = DateTime.UtcNow;
                    checkSub.SubscriptionStatus = Status.Subscribe.ToString();
                    var updateSub = _subscriptionRepo.UpdateSubscription(checkSub);
                    var results = new SubscriptionDTO()
                    {
                        PhoneNumber = subscribe.PhoneNumber,
                        ServiceId = subscribe.ServiceId,
                        SubscriptionDate = checkSub.SubcribeDate,
                        SubscriptionId = checkSub.SubscriptionId
                    };
                    response.DisplayMessage = "Successfully subscribe with a new phone number";
                    response.StatusCode = StatusCodes.Status200OK;
                    response.Result = results;
                    return response;

                }
                var newSub = new Subscriptions()
                {
                    PhoneNumber = subscribe.PhoneNumber,
                    ServiceUserId = checkService.Id,
                    SubcribeDate = DateTime.UtcNow,
                    SubscriptionStatus = Status.Subscribe.ToString()
                };
                var sub = await _subscriptionRepo.AddSubscription(newSub);
                if (sub == null)
                {
                    response.ErrorMessages = new List<string>() { "Subscription not successful" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var result = new SubscriptionDTO()
                {
                    PhoneNumber = subscribe.PhoneNumber,
                    ServiceId = subscribe.ServiceId,
                    SubscriptionDate = sub.SubcribeDate,
                    SubscriptionId = sub.SubscriptionId
                };
                response.DisplayMessage = "Subscribe successfully";
                response.StatusCode = StatusCodes.Status200OK;
                response.Result = result;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in using subscription service, please check back later" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;

            }


        }

        public async Task<ResponseDto<SubscriptionStatus>> SubscribeStatusAsync(SubscribeDto status)
        {
            var response = new ResponseDto<SubscriptionStatus>();
            try
            {
                var checkService = await _accountRepo.FindUserByServiceIdAsync(status.ServiceId);
                if (checkService == null)
                {
                    response.ErrorMessages = new List<string>() { "Invalid service id" };
                    response.StatusCode = 404;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var checkSub = await _subscriptionRepo.GetSubscriptionbyServiceUserId(checkService.Id);
                if (checkSub == null)
                {
                    response.ErrorMessages = new List<string>() { "Cannot unsubscribe service id not subscribed" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                if (checkSub.PhoneNumber != status.PhoneNumber)
                {
                    response.ErrorMessages = new List<string>() { "Can not get subscription status that does not match subscription phone number" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var result = new SubscriptionStatus()
                {
                    PhoneNumber = status.PhoneNumber,
                    ServiceId = status.ServiceId,
                    SubscriptionId = checkSub.SubscriptionId,
                    Status = checkSub.SubscriptionStatus
                };
                response.Result = result;
                response.DisplayMessage = "Success";
                response.StatusCode = 200;
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in getting subscription status, please check back later" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }

        public async Task<ResponseDto<UnSubcribeDTO>> UnSubscribeAsync(SubscribeDto unsubscribe)
        {
            var response = new ResponseDto<UnSubcribeDTO>();
            try
            {
                var checkService = await _accountRepo.FindUserByServiceIdAsync(unsubscribe.ServiceId);
                if (checkService == null)
                {
                    response.ErrorMessages = new List<string>() { "Invalid service id" };
                    response.StatusCode = 404;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var checkSub = await _subscriptionRepo.GetSubscriptionbyServiceUserId(checkService.Id);
                if (checkSub == null)
                {
                    response.ErrorMessages = new List<string>() { "Cannot unsubscribe service id not subscribed" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                if (checkSub.PhoneNumber != unsubscribe.PhoneNumber)
                {
                    response.ErrorMessages = new List<string>() { "Cannot unsubscribe service id that does not match the phone number on subcription" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                if (checkSub.SubscriptionStatus == Status.Unsubscribe.ToString())
                {
                    response.ErrorMessages = new List<string>() { $"Service Id already unsubscribed on {checkSub.UnSubscribeDate}" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                checkSub.SubscriptionStatus = Status.Unsubscribe.ToString();
                checkSub.UnSubscribeDate = DateTime.UtcNow;
                var update = await _subscriptionRepo.UpdateSubscription(checkSub);

                var result = new UnSubcribeDTO()
                {
                    PhoneNumber = checkSub.PhoneNumber,
                    ServiceId = checkSub.ServiceUserId,
                    UnSubscriptionDate = checkSub.UnSubscribeDate,
                    SubscriptionId = checkSub.SubscriptionId
                };
                response.DisplayMessage = "UnSubscribe successfully";
                response.StatusCode = StatusCodes.Status200OK;
                response.Result = result;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in using unsubscription service, please check back later" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
    }
}
