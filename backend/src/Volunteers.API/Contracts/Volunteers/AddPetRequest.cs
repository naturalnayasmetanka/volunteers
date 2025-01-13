using Volunteers.Domain.PetManagment.Pet.Enums;

namespace Volunteers.API.Contracts.Volunteers;

public record AddPetRequest(
    string Nickname,
    string CommonDescription,
    string HelthDescription,
    int PetPhoneNumber,
    PetStatus PetStatus,
    DateTime BirthDate,
    DateTime CreationDate,
    IFormFileCollection Photo);