using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtBearAuthentication.JwtToken;
using JwtBearAuthentication.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace JwtBearAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IConfiguration _config;
        public AuthenticationController(IConfiguration config)
        {
            _config = config;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("gettoken")]
        public IActionResult Token([FromBody] UserModel user)
        {
            IActionResult response = Unauthorized();
            if(user.UserName == _config["AuthenUser:UserName"] && user.Password == _config["AuthenUser:Password"])
            {
                var token = GenerateToken.GetToken(_config["Jwt:Secret"], user);
                response = Ok(token);
            }
            return response;
        }
    }
}
