using Microsoft.AspNetCore.Mvc;
using NAudio.Utils;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using OOD_Project_Backend.Content.Business.Models.Contract;
using OOD_Project_Backend.Content.DataAccess.Entities;
using OOD_Project_Backend.Content.DataAccess.Entities.Enums;
using OOD_Project_Backend.Content.DataAccess.Repository.Contracts;

namespace OOD_Project_Backend.Content.Business.Models;

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
    public async Task<FileResult> ShowPreview(int contentId)
    {
        var musicEntity = await _musicRepository.FindById(contentId);
        var filePath = musicEntity.File.FilePath;
        var contentType = "audio/mpeg";
        using (var reader = new AudioFileReader(filePath))
        {
            int desiredSampleCount = (int)(reader.WaveFormat.SampleRate * 10);

            var buffer = new float[desiredSampleCount];
            int bytesRead = reader.Read(buffer, 0, desiredSampleCount);
            var memoryStream = new MemoryStream();
            using (var writer = new WaveFileWriter(new IgnoreDisposeStream(memoryStream), reader.WaveFormat))
            {
                writer.WriteSamples(buffer, 0, bytesRead);
            }
            memoryStream.Position = 0;
            return new FileContentResult(memoryStream.ToArray(), contentType);
        }
    }

    public async Task<FileResult> ShowNormal(int contentId)
    {
        var musicEntity = await _musicRepository.FindById(contentId);
        var filePath = musicEntity.File.FilePath;
        var contentType = "audio/mpeg";
        return new FileContentResult(await File.ReadAllBytesAsync(filePath),contentType);
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