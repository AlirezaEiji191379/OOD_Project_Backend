﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NAudio.Wave;
using OOD_Project_Backend.Content.Business.Models.Contract;
using OOD_Project_Backend.Content.DataAccess.Entities;
using OOD_Project_Backend.Content.DataAccess.Entities.Enums;
using OOD_Project_Backend.Content.DataAccess.Repository.Contracts;

namespace OOD_Project_Backend.Content.Business.Models;

public class VideoModel : IContentModel
{
    private readonly IVideoEntityRepository _videoEntityRepository;
    private readonly IFileEntityRepository _fileEntityRepository;
    private readonly IContentRepository _contentRepository;
    public VideoModel(IVideoEntityRepository videoEntityRepository, IFileEntityRepository fileEntityRepository, IContentRepository contentRepository)
    {
        _videoEntityRepository = videoEntityRepository;
        _fileEntityRepository = fileEntityRepository;
        _contentRepository = contentRepository;
    }

    public ContentType ContentType => ContentType.Video;
    public async Task<FileResult> ShowPreview(int contentId)
    {
        var videoEntity = await _videoEntityRepository.FindById(contentId);
        var filePath = videoEntity.File.FilePath;
        var contentType = "video/mp4";
        var appendString = "_preview";
        var fileName = Path.GetFileNameWithoutExtension(filePath);
        var newFileName = fileName + appendString;
        var fileDirectory = Path.GetDirectoryName(filePath);
        var fileExtension = Path.GetExtension(filePath);
        var previewFilePath = Path.Combine(fileDirectory, newFileName + fileExtension);
        return new FileContentResult(await File.ReadAllBytesAsync(previewFilePath),contentType);
    }

    public async Task<FileResult> ShowNormal(int contentId)
    {
        var videoEntity = await _videoEntityRepository.FindById(contentId);
        var filePath = videoEntity.File.FilePath;
        var contentType = "video/mp4";
        return new FileContentResult(await File.ReadAllBytesAsync(filePath),contentType);
    }

    public async Task Delete(int contentId)
    {
        var videoEntity = await _videoEntityRepository.FindById(contentId);
        var fileEntity = videoEntity.File;
        File.Delete(videoEntity.File.FilePath);
        _contentRepository.Delete(new ContentEntity()
        {
            Id = contentId
        });
        _fileEntityRepository.Delete(fileEntity);
        await _fileEntityRepository.SaveChangesAsync();
    }
}