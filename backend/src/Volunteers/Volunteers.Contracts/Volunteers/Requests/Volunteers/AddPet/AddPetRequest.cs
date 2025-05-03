

using Shared.Kernel.Enums;
using Volunteers.Application.Volunteers.Commands.AddPet.Commands;

namespace Volunteers.Contracts.Volunteers.Requests.Volunteers.AddPet;

public record AddPetRequest
{
    public string Nickname { get; set; } = string.Empty;
    public string CommonDescription { get; set; } = string.Empty;
    public string HelthDescription { get; set; } = string.Empty;
    public int PetPhoneNumber { get; set; }
    public PetStatus PetStatus { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime CreationDate { get; set; }
    public Guid SpeciesId { get; set; }
    public Guid BreedId { get; set; }

    public static AddPetCommand ToCommand(
        Guid volunteerId,
        AddPetRequest request)
    {
        var command = new AddPetCommand(
            VolunteerId: volunteerId,
            Nickname: request.Nickname,
            CommonDescription: request.CommonDescription,
            HelthDescription: request.HelthDescription,
            PetPhoneNumber: request.PetPhoneNumber,
            PetStatus: request.PetStatus,
            BirthDate: request.BirthDate,
            CreationDate: request.CreationDate,
            SpeciesId: request.SpeciesId,
            BreedId: request.BreedId);

        return command;
    }
};