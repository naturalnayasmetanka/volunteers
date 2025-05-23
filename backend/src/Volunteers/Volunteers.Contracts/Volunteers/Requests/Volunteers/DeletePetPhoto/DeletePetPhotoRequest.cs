﻿using Shared.Core.DTO;
using Volunteers.Application.Volunteers.Commands.DeletePetPhoto.Commands;

namespace Volunteers.Contracts.Volunteers.Requests.Volunteers.DeletePetPhoto;

public record DeletePetPhotoRequest
{
    public string FileName { get; set; } = string.Empty;

    public static DeletePetPhotoCommand ToCommand(
        Guid volunteerId,
        Guid petId,
        string BUCKET_NAME,
        DeletePetPhotoRequest request)
    {
        var fileData = new FileDTO(
            Stream: null,
            BucketName: BUCKET_NAME,
            FileName: request.FileName,
            ContentType: null);

        var command = new DeletePetPhotoCommand(
            VolunteerId: volunteerId,
            PetId: petId,
            FileData: fileData);

        return command;
    }
}
