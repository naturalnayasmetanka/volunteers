using Volunteers.Application.Abstractions;
using Volunteers.Application.DTO;

namespace Volunteers.Application.Volunteers.Commands.GetPresignedLinkPhoto.Commands;

public record GetPresignedLinkPhotoCommand(FileDTO FileData) : ICommand;