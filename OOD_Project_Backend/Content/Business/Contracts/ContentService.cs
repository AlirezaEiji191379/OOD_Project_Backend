﻿using OOD_Project_Backend.Content.Business.Requests;
using OOD_Project_Backend.Core.Context;

namespace OOD_Project_Backend.Content.Business.Abstractions;

public interface ContentService
{
    Task<Response> Add(ContentCreationRequest request);
    Task<Response> GetChannelContentsMetadata(int channelId);
}