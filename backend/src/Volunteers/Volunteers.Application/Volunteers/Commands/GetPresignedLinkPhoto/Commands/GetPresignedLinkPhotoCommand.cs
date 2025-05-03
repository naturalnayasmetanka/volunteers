using Shared.Core.Abstractions.Handlers;
using Shared.Core.DTO;

namespace Volunteers.Application.Volunteers.Commands.GetPresignedLinkPhoto.Commands;

public record GetPresignedLinkPhotoCommand(FileDTO FileData) : ICommand;
