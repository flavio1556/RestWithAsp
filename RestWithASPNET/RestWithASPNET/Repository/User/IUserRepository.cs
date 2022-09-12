using RestWithASPNET.Data.VO;
using RestWithASPNET.Models.Entites;

namespace RestWithASPNET.Repository.User
{
    public interface IUserRepository
    {
        Models.Entites.User ValidateCredentials(UserVO user);
        Models.Entites.User ValidateCredentials(string username);
        Models.Entites.User RefreshUserInfor(Models.Entites.User user);
        bool RevokeToken(string username);
    }
}
