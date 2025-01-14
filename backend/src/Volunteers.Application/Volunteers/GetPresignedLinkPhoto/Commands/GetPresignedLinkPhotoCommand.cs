using Volunteers.Application.DTO;

namespace Volunteers.Application.Volunteers.GetPresignedLinkPhoto.Commands;

public record GetPresignedLinkPhotoCommand(FileDTO FileData);