using BackEnd.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Core.JwtHelpers
{
    public static class JwtHelper
    {
        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts,Guid Id, string role)
        {
            
            var claims = new List<Claim>()
                    {
                new Claim("Id", userAccounts.Id.ToString()),
                new Claim(ClaimTypes.Name, userAccounts.UserName),
                 new Claim(ClaimTypes.Role, userAccounts.Roles),
                new Claim(ClaimTypes.Email, userAccounts.EmailId),
                new Claim(ClaimTypes.NameIdentifier, userAccounts.Id),
                new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyyy HH:mm:ss tt")),
                    };
          
            return claims;
        }
         
        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, out Guid Id, string role)
        {
            Id = new Guid();
            
            return GetClaims(userAccounts,Id,role);
        }


        public static UserTokens GenTokenkey(UserTokens model, JwtSettings jwtSettings)
        {
            var roles = model.Roles;
            try
            {
                var userToken = new UserTokens();
                if (model == null) throw new ArgumentException(nameof(model));

                // Get secret key
                var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);
                Guid Id = Guid.Empty;
                DateTime expireTime = DateTime.UtcNow.AddDays(1);
                userToken.Validaty = expireTime.TimeOfDay;


                


                var JWToken = new JwtSecurityToken(
                    issuer: jwtSettings.ValidIssuer,
                    audience: jwtSettings.ValidAudience,
                    claims: GetClaims(model,out Id, roles),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(expireTime).DateTime,
                    signingCredentials: new SigningCredentials
                    (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                );

                userToken.Token = new JwtSecurityTokenHandler().WriteToken(JWToken);
                var idRefreshToken = Guid.NewGuid();


                userToken.UserName = model.UserName;
                userToken.Id = model.Id;
                userToken.GuidId = Id;
                userToken.Roles = model.Roles;
                return userToken;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
