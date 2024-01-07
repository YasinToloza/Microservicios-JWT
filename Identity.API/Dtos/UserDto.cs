using Identity.API.Models;

namespace Identity.API.Dtos
{
    public class UserDto
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public static class UserDtoExtensions
    {
        public static User ToUser(this UserDto userDto)
        {
            return new User()
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                UserName = userDto.UserName,
                Email = userDto.Email
            };
        }
    }
}
