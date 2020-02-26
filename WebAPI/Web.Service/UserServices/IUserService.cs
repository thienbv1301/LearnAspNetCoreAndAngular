using Web.Service.DtoModels;

namespace Web.Service.UserServices
{
    public interface IUserService
    {
        UserDto GetUserByName(string name);
    }
}
