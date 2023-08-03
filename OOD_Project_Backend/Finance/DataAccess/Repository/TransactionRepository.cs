﻿using OOD_Project_Backend.Core.DataAccess;
using OOD_Project_Backend.Core.DataAccess.Repository;
using OOD_Project_Backend.Finanace.DataAccess.Entities;

namespace OOD_Project_Backend.Finanace.DataAccess.Repository;

public class TransactionRepository : BaseRepository<TransactionEntity>
{
    public TransactionRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}