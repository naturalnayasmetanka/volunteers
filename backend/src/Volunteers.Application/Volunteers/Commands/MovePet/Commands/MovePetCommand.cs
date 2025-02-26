﻿using Volunteers.Application.Abstractions;

namespace Volunteers.Application.Volunteers.Commands.MovePet.Commands;

public record MovePetCommand(
    Guid VolunteerId,
    Guid PetId,
    int NewPosition) : ICommand;
