using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CRUD.Helpers
{
    public class JwtServices
    {
        private string secureKey = "this is a very secure key for authentication purposes only";

        public string Generate(int id)
        {
            var symmetric = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey));
            var credential = new SigningCredentials(symmetric, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credential);

            var payload = new JwtPayload(id.ToString(), null, null, null, DateTime.Today.AddDays(1));
            var securityToken = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public JwtSecurityToken Verify(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secureKey);
            handler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;
        }
    }
}