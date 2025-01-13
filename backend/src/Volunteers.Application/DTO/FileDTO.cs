﻿namespace Volunteers.Application.DTO;

public record FileDTO(
    Stream? Stream,
    string? BucketName,
    string FileName,
    string? ContentType,
    int Expiry = 0);