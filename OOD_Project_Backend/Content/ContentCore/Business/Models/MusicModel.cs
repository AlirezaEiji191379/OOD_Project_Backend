using Microsoft.AspNetCore.Mvc;
using NAudio.Utils;
using NAudio.Wave;
using OOD_Project_Backend.Content.ContentCore.Business.Contracts;
using OOD_Project_Backend.Content.ContentCore.Business.Models.Contract;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities.Enums;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories.Contracts;

namespace OOD_Project_Backend.Content.ContentCore.Business.Models;

public class MusicModel : IContentModel
{
    private readonly IMusicRepository _musicRepository;
    private readonly IFileEntityRepository _fileEntityRepository;
    private readonly IContentRepository _contentRepository;
    public MusicModel(IMusicRepository musicRepository, IFileEntityRepository fileEntityRepository, IContentRepository contentRepository)
    {
        _musicRepository = musicRepository;
        _fileEntityRepository = fileEntityRepository;
        _contentRepository = contentRepository;
    }

    public ContentType ContentType => ContentType.Music;
    public async Task<ShowContentDto> ShowPreview(int contentId)
    {
        var musicEntity = await _musicRepository.FindById(contentId);
        var filePath = musicEntity.File.FilePath;
        var contentType = "audio/mpeg";
        var appendString = "_preview";
        var fileName = Path.GetFileNameWithoutExtension(filePath);
        var newFileName = fileName + appendString;
        var fileDirectory = Path.GetDirectoryName(filePath);
        var fileExtension = Path.GetExtension(filePath);
        var previewFilePath = Path.Combine(fileDirectory, newFileName + fileExtension);
        if (!File.Exists(previewFilePath))
        {
            using (var reader = new AudioFileReader(filePath))
            {
                int desiredSampleCount = (int)(reader.WaveFormat.SampleRate * 10);

                var buffer = new float[desiredSampleCount];
                int bytesRead = reader.Read(buffer, 0, desiredSampleCount);
                using var fileStream = new FileStream(previewFilePath,FileMode.Create);
                using (var writer = new WaveFileWriter(new IgnoreDisposeStream(fileStream), reader.WaveFormat))
                {
                    writer.WriteSamples(buffer, 0, bytesRead);
                }
                //memoryStream.Position = 0;
            }
        }

        return new ShowContentDto()
        {
            ContentType = ContentType.Music.ToString(),
            Value = newFileName + fileExtension
        };
    }

    public async Task<ShowContentDto> ShowNormal(int contentId)
    {
        var musicEntity = await _musicRepository.FindById(contentId);
        var filePath = musicEntity.File.FilePath;
        var contentType = "audio/mpeg";
        return new ShowContentDto()
        {
            ContentType = ContentType.Music.ToString(),
            Value = Path.GetFileName(filePath) 
        };
        //return new FileContentResult(await File.ReadAllBytesAsync(filePath),contentType);
    }

    public async Task Delete(int contentId)
    {
        var musicEntity = await _musicRepository.FindById(contentId);
        _fileEntityRepository.Delete(musicEntity.File);
        _contentRepository.Delete(new ContentEntity()
        {
            Id = contentId
        });
        File.Delete(musicEntity.File.FilePath);
        await _fileEntityRepository.SaveChangesAsync();
    }
}