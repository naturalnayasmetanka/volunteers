using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteers.Application.Volunteers.Commands.UpdatePet.Commands;

public record UpdatePetCommand(
    Guid PetId,
    Guid VolunteerId,
    string Nickname,
    string CommonDescription,
    string HelthDescription,
    int PetPhoneNumber,
    PetStatus PetStatus,
    DateTime BirthDate,
    DateTime CreationDate,
    Guid SpeciesId,
    Guid BreedId) : ICommand;
