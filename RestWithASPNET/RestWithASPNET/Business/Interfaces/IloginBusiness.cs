using RestWithASPNET.Data.VO;

namespace RestWithASPNET.Business.Interfaces
{
    public interface IloginBusiness
    {
        TokenVO ValidateCredentials(UserVO user);
        TokenVO ValidateCredentials(TokenVO token);
        bool RevokeToken(string userName);
    }
}
