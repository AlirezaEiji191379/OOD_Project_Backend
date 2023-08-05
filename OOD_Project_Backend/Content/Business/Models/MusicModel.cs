﻿using Microsoft.AspNetCore.Mvc;
using NAudio.Utils;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using OOD_Project_Backend.Content.Business.Models.Contract;
using OOD_Project_Backend.Content.DataAccess.Entities.Enums;
using OOD_Project_Backend.Content.DataAccess.Repository.Contracts;

namespace OOD_Project_Backend.Content.Business.Models;

public class MusicModel : IContentModel
{
    private readonly IMusicRepository _musicRepository;
    
    
    public MusicModel(IMusicRepository musicRepository)
    {
        _musicRepository = musicRepository;
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

    public Task Delete(int contentId)
    {
        throw new NotImplementedException();
    }
}