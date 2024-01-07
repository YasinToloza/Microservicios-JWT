using Identity.API.Dtos;
using Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Identity.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;
        public UserRepository(UserManager<User> userManager, IConfiguration configuration) 
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public async Task<ResponseDto<UserDto>> Register(UserDto userDto)
        {
            var user = userDto.ToUser();
            var result = await userManager.CreateAsync(user, userDto.Password);
            var response = new ResponseDto<UserDto>()
            {
                Success = result.Succeeded
            };
            if (result.Succeeded)
            {
                userDto.Id = user.Id;
                userDto.Password = "";
                response.Data = userDto;
            }
            else 
            {
                response.Error = result.Errors.ToString();
            }
            return response;    
        }

        public async Task<ResponseDto<string>> Login(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            var response = new ResponseDto<string>();

            if (user != null && await userManager.CheckPasswordAsync(user, loginDto.Password))
            { 
                response.Success = true;
                response.Data = GetToken();
                return response;
            }
            response.Success = false;
            response.Error = $"User {loginDto.Email} not found or password incorrect";
            return response;
        }

        private string GetToken()
        {
            //captura la semilla y lo transforma a un array de bytes
            var authSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration["JWTSettings:Secret"]));

            var token = new JwtSecurityToken(
            issuer: configuration["JWTSettings:ValidIssuer"],
            audience: configuration["JWTSettings:ValidAudience"],
            expires: DateTime.Now.AddHours(3),
            //claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey,
            SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
