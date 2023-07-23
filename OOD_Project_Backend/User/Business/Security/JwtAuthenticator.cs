using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using OOD_Project_Backend.User.Business.Abstractions;

namespace OOD_Project_Backend.User.Business.Security;

public class JwtAuthenticator : Authenticator
{
    private readonly IConfiguration _configuration;

    public JwtAuthenticator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(int userId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JwtPrivateKey"));
        var jwt = new JwtSecurityToken(
            claims: new Claim[]
            {
                new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new("uid",userId.ToString()),
            },
            expires: DateTime.Now.AddSeconds(7200),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        );
        return tokenHandler.WriteToken(jwt);
    }
}