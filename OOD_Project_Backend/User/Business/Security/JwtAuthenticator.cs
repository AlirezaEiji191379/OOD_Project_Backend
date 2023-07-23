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
        var secretKey = _configuration.GetValue<string>("JwtPrivateKey");
        var key = Encoding.UTF8.GetBytes(secretKey);
        var jwt = new JwtSecurityToken(
            claims: new Claim[]
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new("uid", userId.ToString()),
            },
            expires: DateTime.Now.AddSeconds(7200),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        );
        return tokenHandler.WriteToken(jwt);
    }

    public int FindUserId(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token);
        var tokenS = jsonToken as JwtSecurityToken;
        var uid = tokenS.Claims.First(claim => claim.Type == "uid").Value;
        return Convert.ToInt32(uid);
    }

    public bool ValidateToken(string token)
    {
        try
        {
            var secretKey = _configuration.GetValue<string>("JwtPrivateKey");
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            return true;
        }
        catch (Exception exception)
        {
            return false;
        }
    }
}