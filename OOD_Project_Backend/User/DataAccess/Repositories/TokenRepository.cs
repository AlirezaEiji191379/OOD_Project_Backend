using OOD_Project_Backend.User.DataAccess.Repositories.Contract;
using StackExchange.Redis;

namespace OOD_Project_Backend.User.DataAccess.Repositories;

public class TokenRepository : ITokenRepository
{
    private readonly IConnectionMultiplexer _redis;

    public TokenRepository(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public async Task SaveBlackListedTokenId(string tokenId)
    {
        var db = _redis.GetDatabase();
        var value = true;
        var ttl = TimeSpan.FromMinutes(120);
        await db.StringSetAsync(tokenId, value, ttl);
    }

    public async Task<bool> IsTokenBlackListed(string tokenId)
    {
        var db = _redis.GetDatabase();
        var key = tokenId.ToString();
        var value = await db.StringGetAsync(key);
        return !value.IsNull;
    }

    public async Task SaveVerificationCode(int userId, int code)
    {
        var db = _redis.GetDatabase();
        var value = code;
        var ttl = TimeSpan.FromMinutes(15);
        await db.StringSetAsync(userId.ToString(), code, ttl);
    }

    public async Task<int?> GetVerificationCode(int userId)
    {
        var db = _redis.GetDatabase();
        var key = userId.ToString();
        var value = await db.StringGetAsync(key);
        int.TryParse(value,out int code);
        return code;
    }
}