using Identity.API.Dtos;

namespace Identity.API.Repositories
{
    public interface IUserRepository
    {
        public Task<ResponseDto<string>> Login(LoginDto loginDto);
        public Task<ResponseDto<UserDto>> Register(UserDto userDto);
    }
}
