

using AbadiAgroApi.Model.AuthJWT;
using AbadiAgroApi.Model.Security;

namespace AgroErp.Security
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(LoginInfo loginInfo);
    }
}
