using jwt_example.Models.User;

namespace jwt_example.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private List<UserDTO> _users = new ();
        public UserRepository()
        {
            _users.Add(new UserDTO("username","password","admin"));
        }
        public UserDTO GetUser(UserModel userModel)
        {
            return _users.Single(x => string.Equals(x.UserName, userModel.UserName) && x.Password == userModel.Password);
        }
    }
}
