using API_Tutorial.Data;
using API_Tutorial.Interfaces;
using API_Tutorial.Models.DTO;
using API_Tutorial.Models.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Tutorial.Services
{
    public class AuthService(IConfiguration configuration, ApplicationDBContext dBContext) : IAuthService
    {
        public async Task<string> login(UserDTO userDTO)
        {
            var user1 = dBContext.Users.FirstOrDefault(x => x.Email == userDTO.Email && x.Password == userDTO.Password);
            if (user1 == null)
            {
                return null;
            }
            string Token = Create_JWT_Token(userDTO);
            return Token;
        }

        public async Task<User?> RegisterUser(UserDTO userDTO)
        {
            var UserExist =  dBContext.Users.FirstOrDefault(x =>  x.Email == userDTO.Email);
            if (UserExist != null)
            {
                return null;
            }

            var obj1 = new User()
            {
                Email = userDTO.Email,
                Password = userDTO.Password
            };
            dBContext.Users.Add(obj1);
            dBContext.SaveChanges();
            return await Task.FromResult(obj1);
        }


        private string Create_JWT_Token(UserDTO user)
        {
            var claims = new List<Claim>
            {
                new Claim("Email", user.Email),
                new Claim("Password" , user.Password)
            };

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("JWTSettings:Key")));
            var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("JWTSettings:Issuer"),
                audience: configuration.GetValue<string>("JWTSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(configuration.GetValue<string>("JWTSettings:DurationInMinutes"))),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token).ToString();
        }
    }

}

