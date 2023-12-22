using Microsoft.AspNetCore.Identity;
using Subscription.API.Service.Interface;
using Subscription.DATA.Context;
using Subscription.DATA.Repository.Interface;
using Subscription.MODEL.DTO;
using Subscription.MODEL.Entities;

namespace Subscription.API.Service.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepo _accountRepo;
        private readonly ILogger<AccountService> _logger;
        private readonly IGenerateJwt _generateJwt;
        private readonly SubscriptionContextDb _context;

        public AccountService(IAccountRepo accountRepo, ILogger<AccountService> logger, IGenerateJwt generateJwt, SubscriptionContextDb context)
        {
            _accountRepo = accountRepo;
            _logger = logger;
            _generateJwt = generateJwt;
            _context = context;
        }

        public async Task<ResponseDto<LoginResultDto>> LoginUser(SignInModel signIn)
        {
            var response = new ResponseDto<LoginResultDto>();
            try
            {
                var checkUserExist = await _accountRepo.FindUserByServiceIdAsync(signIn.Service_Id);
                if (checkUserExist == null)
                {
                    response.ErrorMessages = new List<string>() { "Invalid service id" };
                    response.StatusCode = 404;
                    response.DisplayMessage = "Error";
                    return response;
                }

                var checkPassword = await _accountRepo.CheckAccountPassword(checkUserExist, signIn.Password);
                if (checkPassword == false)
                {
                    response.ErrorMessages = new List<string>() { "Invalid Service Id credential" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }

                var generateToken = await _generateJwt.GenerateToken(checkUserExist);
                if (generateToken == null)
                {
                    response.ErrorMessages = new List<string>() { "Error in generating jwt for user" };
                    response.StatusCode = 501;
                    response.DisplayMessage = "Error";
                    return response;
                }
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Successfully login";
                response.Result = new LoginResultDto() { Jwt = generateToken };
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in login with service id" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }

        public async Task<ResponseDto<string>> RegisterUser(SignUp signUp)
        {
            var response = new ResponseDto<string>();
            try
            {
                var checkUserExist = await _accountRepo.FindUserByServiceIdAsync(signUp.ServiceId);
                if (checkUserExist != null)
                {
                    response.ErrorMessages = new List<string>() { "Service Id already exist" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var mapAccount = new ServiceUser();
                
               
                mapAccount.ServiceId = signUp.ServiceId;
                mapAccount.UserName = signUp.ServiceId;
                mapAccount.Email = signUp.ServiceId +"@gmail.com";
              

                var createUser = await _accountRepo.SignUpAsync(mapAccount, signUp.Password);
                if (createUser == null)
                {
                    response.ErrorMessages = new List<string>() { "User not created successfully" };
                    response.StatusCode = StatusCodes.Status501NotImplemented;
                    response.DisplayMessage = "Error";
                    return response;
                }
              
                
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Successful";
                response.Result = "Service id successfully created";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in creating service id" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }

    }
}
