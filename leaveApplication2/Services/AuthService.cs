using leaveApplication2.Dtos;
using leaveApplication2.Models;
using leaveApplication2.Repostories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace leaveApplication2.Services
{
    public class AuthService : IAuthService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IConfiguration _config;
  
        private readonly TimeSpan _tokenLifetime;
        private readonly TimeSpan _refreshTokenLifetime;


        public AuthService(IEmployeeRepository employeeRepository, IConfiguration config)
        {
            _employeeRepository = employeeRepository;
            _config = config;
          
            _tokenLifetime = TimeSpan.Parse(config["Jwt:TokenLifetime"]);

            _refreshTokenLifetime = TimeSpan.Parse(config["Jwt:RefreshTokenLifetime"]);
        }
        public async Task<LoginDetailDto> AuthenticateUser(string username, string password)
        {
            var employee = await _employeeRepository.EmployeeLoginAsync(new Employee() { emailAddress = username, employeePassword = password });



            if (employee == null)
                throw new UnauthorizedAccessException("Invalid username or password.");


            //var userUpdated = _mapper.Map<UserDTO>(user1);
           var empDto  =  new LoginDetailDto(employee.employeeId,employee.RoleAssignId,employee.emailAddress, employee.employeePassword,employee.firstName, employee.lastName);
          var updateDto =   GenerateTokens(empDto);
            return updateDto;
        }
        public LoginDetailDto GenerateTokens(LoginDetailDto user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:SecretKey"]);
            var tokenExpiration = DateTime.UtcNow.Add(_tokenLifetime);
            var refreshTokenExpiration = DateTime.UtcNow.Add(_refreshTokenLifetime);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.EmployeeId.ToString())
        }),
                Expires = tokenExpiration,
                Audience = _config["Jwt:Audience"],
                Issuer = _config["Jwt:Issuer"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var refreshTokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = refreshTokenExpiration,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = tokenHandler.CreateToken(refreshTokenDescriptor);

            //user.Token = tokenHandler.WriteToken(token);
            //user.RefreshToken = tokenHandler.WriteToken(refreshToken);
            //user.RefreshTokenExpiryTime = refreshTokenExpiration;


            // Create a new instance of LoginDetailDto using an object initializer
            var updatedUser = new LoginDetailDto(
                EmployeeId: user.EmployeeId,
                RoleAssignId: user.RoleAssignId,
                UserName: user.UserName,
                Password: user.Password,
                firtsName: user.firtsName,
                lastName: user.lastName,
                Token: tokenHandler.WriteToken(token),
                refreshToken: tokenHandler.WriteToken(refreshToken),
                RefreshTokenExpiryTime :refreshTokenExpiration
            );

            // return updatedUser;

            return updatedUser;
        }




    }
}
