using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtBearAuthentication.JwtToken;
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
        public IActionResult Token(string key)
        {
            IActionResult response = Unauthorized();
            var token = GenerateToken.GetToken(key);
            response = Ok(token);
            return response;
        }
    }
}
