using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Users_Product_Security.DTO;
using Users_Product_Security.Models;
using Users_User_Security.Interfaces;
using Users_User_Security.Repository;

namespace Users_Product_Security.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [AllowAnonymous]
    public class AuthController:Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUser _userRepo;

        public AuthController(IConfiguration configuration, IUser userRepo)
        {
            _configuration = configuration;
            _userRepo = userRepo;
        }


        [AllowAnonymous]
        [HttpPost("LoginUser")]
        public IActionResult Login([FromBody] UserDTO userlogin)
        {
            var user = Authenticate(userlogin);
            if (user != null)
            {
                var token = generate(user);
                return Ok(token);
            }
            return NotFound("User not found");
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("RegisterUser")]
        public List<User> CreateUser(User user)
        {
            return _userRepo.CreateUsers(user);
        }

        ///These methods below used to generate tokens based on claims .
        private string generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)

            };
            var tokens = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(tokens);
        }
        private User Authenticate(UserDTO userlogin)
        {
            var currentUser = _userRepo.GetUsers().
                FirstOrDefault(o => o.Name.Equals(userlogin.Name, StringComparison.OrdinalIgnoreCase)
                && o.Password.Equals(userlogin.password));
            if (currentUser != null)
            {
                return currentUser;

            }
            return null;
        }

        private UserDTO getCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new UserDTO
                {
                    Name = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value
                };
            }
            return null;
        }
    }
}
///Once Login a Token is generated just paste it in 
///the bearer,so that can access now.
///HTTPSTATUS CODES:
///400 ===>BAD REQUEST
///401 ===>UN-AUTHORIZED
///403 ===>FORBIDDEN