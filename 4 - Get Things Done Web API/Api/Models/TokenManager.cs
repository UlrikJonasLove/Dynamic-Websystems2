using Hanssens.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Models
{
    public class TokenManager : ITokenManager
    {
        private readonly string _key = "1234567890-GetThingsDone";
        private readonly string _issuer = "GetThingsDone.com";

        private readonly IConfiguration _config;

        public static LocalStorageConfiguration config = new LocalStorageConfiguration()
        {
            AutoSave = true,
            AutoLoad = true
        };

        public static LocalStorage loc = new LocalStorage(config, "mvc");

        public TokenManager(IConfiguration config)
        {
            _config = config;
        }

        public string TokenGenerator(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                    new Claim(JwtRegisteredClaimNames.Jti, user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email)
                };

            var token = new JwtSecurityToken
            (
                issuer: _issuer,
                audience: _issuer,
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );

            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            loc.Store<string>("securityToken", encodetoken);
            return encodetoken;
        }

        public string GetToken()
        {
            if (loc.Exists("securityToken"))
                return loc.Get<string>("securityToken");

            return String.Empty;
        }

        public void RemoveToken()
        {
            loc.Clear();
        }
    }
}
