using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtBearAuthentication.Extension
{
    public static class AuthenticationServiceCollectionExtensions
    {
        public static void AddJWTBearerAuthentication(this IServiceCollection service, string key)
        {
            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options =>
                   {
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateLifetime = true,
                           ValidateAudience = false,
                           ValidateIssuer = false,
                           RequireExpirationTime = true,
                           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                           ClockSkew = TimeSpan.Zero
                       };
                       options.Events = new JwtBearerEvents()
                       {
                           OnAuthenticationFailed = c =>
                           {
                               c.Response.StatusCode = 401;
                               c.Response.ContentType = "text/plain";
                               c.Response.WriteAsync("Access denied").Wait();
                               return Task.CompletedTask;
                           },
                       };
                   });
        }
    }
}
