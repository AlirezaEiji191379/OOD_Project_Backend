using OOD_Project_Backend.Channel.ChannelCore.DataAccess.Entities;
using OOD_Project_Backend.Core.DataAccess.Contracts;

namespace OOD_Project_Backend.Channel.ChannelCore.DataAccess.Repositories.Contracts;

public interface INonPremiumUsersPremiumContentsRepository : IBaseRepository<NonPremiumUsersPremiumContentsEntity>
{
    ValueTask<NonPremiumUsersPremiumContentsEntity?> Find(int contentId, int userId);
}