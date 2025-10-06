
using AbadiAgroApi.Model.General;
using AbadiAgroApi.Model.UserInfo;
namespace AbadiAgroApi.Service.Security
{
    public interface IUser
    {
        Task<UserModel> GetUserByLoginID(string loginID);
        Task<UserModel> GetUserByLoginIDandPassword(string loginID, string password);


        Task<Result> LoginUser(string loginID);
        Task<Result> OTPSubmit(string loginID, ApiAuthInfo authInfo);
        Task<Result> OTPSubmitIfForgot(string loginID, ApiAuthInfo authInfo);
        Task<Result> ChangePassword(string loginID, Password password);
        Task<Result> ChangePasswordIfForgot(Password password);
        Task<Result> OTP_Generate(UserModel user, string authKey, string authValue);
        Task<Result> ResetPassword(string loginID, ResetPasswordRequest request);
    }
}
