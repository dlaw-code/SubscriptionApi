using Subscription.MODEL.DTO;

namespace Subscription.API.Service.Interface
{
    public interface IAccountService
    {
        Task<ResponseDto<string>> RegisterUser(SignUp signUp);

        Task<ResponseDto<LoginResultDto>> LoginUser(SignInModel signIn);
    }
}
