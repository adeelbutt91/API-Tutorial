using API_Tutorial.Data;
using API_Tutorial.Interfaces;
using API_Tutorial.Models.DTO;
using API_Tutorial.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Tutorial.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {

        [HttpPost("addUser")]
        public IActionResult AddUser(UserDTO obj)
        {
            var obj1 = authService.RegisterUser(obj);
            return Ok(obj1);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> GetUserAsync(UserDTO user)
        {
            var Token = await authService.login(user);
            return Ok(Token);
        }


    }
}

