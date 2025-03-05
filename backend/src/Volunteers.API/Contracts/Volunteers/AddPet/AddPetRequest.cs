using Volunteers.Application.Handlers.Volunteers.Commands.AddPet.Commands;
using Volunteers.Domain.PetManagment.Pet.Enums;

namespace Volunteers.API.Contracts.Volunteers.AddPet;

public record AddPetRequest
{
    public string Nickname { get; set; } = string.Empty;
    public string CommonDescription { get; set; } = string.Empty;
    public string HelthDescription { get; set; } = string.Empty;
    public int PetPhoneNumber { get; set; }
    public PetStatus PetStatus { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime CreationDate { get; set; }

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
            CreationDate: request.CreationDate);

        return command;
    }
};