using System.ComponentModel.DataAnnotations;

namespace jwt_example.Models.User
{
    public record UserModel
    {
        [Required]
        public string UserName {  get; set; }
        [Required]
        public string Password { get; set; }

    }
    public record UserDTO(string UserName, string Password, string Role)
    {

    }
}
