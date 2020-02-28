using Web.Data.EntityModels;
using Web.Service.DtoModels;

namespace Web.Service.UserServices
{
    public interface IUserService
    {
        UserDto GetUserByName(string name);
        void Register(UserRegisterModel newUserInfo);
        bool AccountIsExist(string accName);
        User Authenticate(UserLoginModel userLogin);
        string CreateToken(User user);
    }
}
