using dating_backend.Entities;
using dating_backend.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace dating_backend.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key; // A key to decrypt and encrypt the data.
        private readonly UserManager<User> _userManager;
        public TokenService(IConfiguration config, UserManager<User> userManager)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
            _userManager = userManager;
        }

        public async Task<string> CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new(JwtRegisteredClaimNames.UniqueName, user.UserName),

            };

            var roles = await _userManager.GetRolesAsync(user); // Getting the roles of the user.
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role))); // Adding the roles to the claims.
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature); // Credentials to sign the Token.
                                                                                              // Getting the key and the algorithm to encrypt the key.

            // Description about the token.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
            };

            var tokenHandler = new JwtSecurityTokenHandler(); // Created a token handler with the package we installed.

            var token = tokenHandler.CreateToken(tokenDescriptor); // Creating our token using the handler and the descriptor we created.

            return tokenHandler.WriteToken(token);
        }
    }
}
