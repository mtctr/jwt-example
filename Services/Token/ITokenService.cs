using jwt_example.Models.User;

namespace jwt_example.Services.Token
{
    public interface ITokenService
    {
        string BuildToken(string key, UserDTO user);
    }
}
