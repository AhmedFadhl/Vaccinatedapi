using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Vaccinatedapi.data;
using Vaccinatedapi.models;
using Vaccinatedapi.multiModels;
using Vaccinatedapi.Repository.Abstract;


namespace Vaccinatedapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        dbdatacontexts? _dbdatacontexts = null;



        public AuthController(IConfiguration configuration, IUserService userService, dbdatacontexts dbdatacontexts)
        {
            _configuration = configuration;
            _userService = userService;
            _dbdatacontexts = dbdatacontexts;

        }

        [HttpGet, Authorize(Roles = "Admin")]
        public ActionResult<string> GetMe()
        {
            var userName = _userService.GetMyName();
            return Ok(userName);
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.user_name = request.user_name;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            try
            {
                if (_dbdatacontexts != null)
                {
                    _dbdatacontexts.users.Add(user);
                    await _dbdatacontexts.SaveChangesAsync();
                }
                return Ok(user);
            }
            catch (Exception)
            {
                return BadRequest("did not add user successfully");
            }

        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            if (_dbdatacontexts != null)
            {

                User? user_id = await _dbdatacontexts.users.FirstOrDefaultAsync(x => x.user_name == request.user_name);
                if (user_id == null || user_id.user_name != request.user_name)
                {
                    return BadRequest("User not found.");
                }

                if (!VerifyPasswordHash(request.Password, user_id.PasswordHash, user_id.PasswordSalt))
                {
                    return BadRequest("Wrong password.");
                }

                string token = CreateToken(user);

                var refreshToken = GenerateRefreshToken();
                SetRefreshToken(refreshToken);
                User us = new User();

                us = user;
                us.user_name = user_id.user_name;
                us.ID = user_id.ID;
                us.token = token;
                return Ok(us);
            }
            else
            {

                return Ok();
            }
        }



        [HttpPost("mobile_login")]
        public async Task<ActionResult<string>> mobile_login(UserDto request)
        {
            try
            {
                parents user_id = await _dbdatacontexts.parents.FirstOrDefaultAsync(x => x.user_name == request.user_name && x.password == request.Password);
                if (user_id == null)
                {
                    return NotFound("User not found.");
                }
                string token = CreateToken(user);
                var refreshToken = GenerateRefreshToken();
                SetRefreshToken(refreshToken);
                User us = new User();
                parent_user parent_User=new parent_user();
                us = user;
                us.user_name = user_id.user_name;
                 parent_User.name=user_id.name;
                parent_User.image_path="uploads/files"+user_id.image_path;
                parent_User.ID = user_id.ID;
                parent_User.password=user_id.password;
                us.token = token;
                return Ok(parent_User);
            }
            catch (Exception)
            {
                return NotFound("Error User not found");
            }
        }


        [HttpPost("refresh-token")]
        public async Task<ActionResult<User>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (!user.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid Refresh Token.");
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                return Unauthorized("Token expired.");
            }

            string token = CreateToken(user);
            var newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(newRefreshToken);
            User us = new User();
            us = user;
            us.token = token;

            return Ok(us);
        }








        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        private void SetRefreshToken(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.user_name),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(JwtRegisteredClaimNames.Aud,_configuration["AppSettings:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Aud,_configuration["AppSettings:Audience"])
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Issuer"],
                audience: _configuration["Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
