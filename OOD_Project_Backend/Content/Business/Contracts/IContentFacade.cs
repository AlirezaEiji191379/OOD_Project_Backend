﻿namespace OOD_Project_Backend.Content.Business.Contracts
{
    public interface IContentFacade
    {
        Task<ContentDetailsContract> GetContentDetails(int contentId);
    }
}
