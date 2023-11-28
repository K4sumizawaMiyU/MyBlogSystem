using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyBlog.JWT.Utilities;
using MyBlog.JWT.Utilities.ApiResults;
using MyBlog.Service;

namespace MyBlog.JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private IAuthorInfoService _authorInfoService;
        public AuthorizeController(IAuthorInfoService authorInfoService)
        {
            _authorInfoService = authorInfoService;
        }
        [HttpPost("Login")]
        public async Task<ApiResult> Login(string username, string password)
        {
            var pwd = Md5Helper.Md5Encrypt32(password);
            var user = await _authorInfoService.SelectAsync(c => 
                c.UserName == username && c.UserPwd == pwd);
            if (user != null)
            {
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.Name),
                    new Claim("Id",user.Id.ToString()),
                    new Claim("UserName",user.UserName)
                };
                // var hmac = new HMACSHA256();
                // var s = Convert.ToBase64String(hmac.Key);
                // var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(s));
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("em2TPkYYc7FNbAS7mmjUAm4oQ7dczVPgrhoWHjryrUf5aUVI3zZnnZdrfNlBcMXykL1hacUXcczeTzP7jx23iw=="));
                var token = new JwtSecurityToken(
                   issuer:"http://localhost:6060;https://localhost:6066",
                   audience:"http://localhost:5000;https://localhost:5100",
                   claims:claims,
                   notBefore:DateTime.Now,
                   expires:DateTime.Now.AddHours(1),
                   signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                return ApiResultHelper.Success(jwtToken);
            } 
            else
            {
                return ApiResultHelper.Error("用户名或密码错误了喵~");
            }
            
        }
    }
}
