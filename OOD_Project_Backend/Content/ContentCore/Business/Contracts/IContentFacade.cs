namespace OOD_Project_Backend.Content.ContentCore.Business.Contracts
{
    public interface IContentFacade
    {
        Task<ContentDetailsContract> GetContentDetails(int contentId);
    }
}
