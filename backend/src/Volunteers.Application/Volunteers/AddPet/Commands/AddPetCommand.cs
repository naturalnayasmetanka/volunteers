﻿using Volunteers.Domain.PetManagment.Pet.Enums;

namespace Volunteers.Application.Volunteers.AddPet.Commands;

public record AddPetCommand(
    Guid VolunteerId,
    string Nickname,
    string CommonDescription,
    string HelthDescription,
    int PetPhoneNumber,
    PetStatus PetStatus,
    DateTime BirthDate,
    DateTime CreationDate,
    List<FileSignature> Files);

public record FileSignature(Stream FileStream, string ContentType, string FileName);