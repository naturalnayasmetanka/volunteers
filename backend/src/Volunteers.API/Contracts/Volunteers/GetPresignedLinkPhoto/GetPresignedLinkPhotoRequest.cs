using Volunteers.Application.DTO;
using Volunteers.Application.Volunteers.GetPresignedLinkPhoto.Commands;

namespace Volunteers.API.Contracts.Volunteers.GetPresignedLinkPhoto;

public record GetPresignedLinkPhotoRequest
{
    public string FileName { get; set; } = string.Empty;
    public int Expiry { get; set; } = 60 * 60 * 24;

    public static GetPresignedLinkPhotoCommand ToCommand(
        string BUCKET_NAME,
        GetPresignedLinkPhotoRequest request)
    {
        var fileData = new FileDTO(
            Stream: null,
            BucketName: BUCKET_NAME,
            FileName: request.FileName,
            ContentType: null,
            Expiry: request.Expiry);

        var command = new GetPresignedLinkPhotoCommand(fileData);

        return command;
    }
}