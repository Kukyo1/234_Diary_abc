using Asp.netWeb_API.Data;
using Asp.netWeb_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Asp.netWeb_API.ViewModels;

namespace Asp.netWeb_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Auth : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public Auth(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }
        [HttpPost]
        [Route("Register")]
        public async Task<IResult> Register(User user)
        {
            if (await _context.Users.AnyAsync(e => e.Email == user.Email))
            {
                return Results.Conflict(new {message= "This email is already used"});
            }
            var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
            user.Password = Convert.ToBase64String(hashBytes);
            user.Notes = new List<Note>();
            _context.Users.Add(user);

            if(_context.SaveChanges() > 0)
            {
                return Results.Ok(new { message = "User successfully created" });
            }
            else
            {
                return Results.StatusCode(500);
            }
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IResult> Login(UserVM pre_user)
        {
            var user = await _context.Users.FirstOrDefaultAsync(e => e.Email == pre_user.Email);
            if(user == null)
            {
                return Results.NotFound(new { message = "User not found" });
            }
            var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(pre_user.Password));
            pre_user.Password = Convert.ToBase64String(hashBytes);
            if(user.Password != pre_user.Password)
            {
                return Results.BadRequest(new { message = "Wrong password" });
            }
            var claims = new Claim[]
            {
                new(JwtRegisteredClaimNames.Email, pre_user.Email),
                new(JwtRegisteredClaimNames.Name, user.Name)
            };
            var creds = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                SecurityAlgorithms.HmacSha256
                );
            var token = new JwtSecurityToken(
                "https://localhost:7210",
                "https://localhost:7210",
                claims,
                null,
                DateTime.Now.AddHours(3),
                creds);
            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return Results.Ok(new {token =  tokenValue, name = user.Name});
        }
    }
}
