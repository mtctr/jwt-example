using jwt_example.Models.User;

namespace jwt_example.Repositories.User
{
    public interface IUserRepository
    {
        UserDTO GetUser(UserModel userModel);
    }
}
