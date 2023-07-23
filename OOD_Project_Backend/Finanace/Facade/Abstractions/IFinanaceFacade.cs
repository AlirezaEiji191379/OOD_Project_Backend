using OOD_Project_Backend.User.DataAccess.Entities;

namespace OOD_Project_Backend.Finanace.Facade.Abstractions;

public interface IFinanaceFacade
{
    Task CreateWallet(UserEntity user,int initBalance = 100);
}