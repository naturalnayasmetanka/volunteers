﻿using Volunteers.Application.Abstractions;

namespace Volunteers.Application.Volunteers.Commands.Delete.Commands;

public record DeleteCommand(Guid Id) : ICommand;