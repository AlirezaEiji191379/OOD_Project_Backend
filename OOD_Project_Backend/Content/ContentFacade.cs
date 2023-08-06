using OOD_Project_Backend.Content.ContentCore.Business.Contracts;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories.Contracts;

namespace OOD_Project_Backend.Content
{
    public class ContentFacade : IContentFacade
    {
        private readonly IContentMetaDataRepository _contentMetaDataRepository;

        public ContentFacade(IContentMetaDataRepository contentMetaDataRepository)
        {
            this._contentMetaDataRepository = contentMetaDataRepository;
        }

        public async Task<ContentDetailsContract> GetContentDetails(int contentId)
        {
            var contentMetaData = await _contentMetaDataRepository.FindByContentId(contentId);
            return new ContentDetailsContract()
            {
                Price = contentMetaData.Price,
                ChannelId = contentMetaData.ChannelId,
                ContentId = contentMetaData.ContentId
            };
        }
        
    }
}
