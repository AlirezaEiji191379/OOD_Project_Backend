namespace OOD_Project_Backend.User.DataAccess.Repositories.Contract;

public interface ITokenRepository
{
    Task SaveBlackListedTokenId(string tokenId);
    Task<bool> IsTokenBlackListed(string tokenId);
}