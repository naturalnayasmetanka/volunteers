using Volunteers.Application.DTO;
using Volunteers.Application.Volunteers.Commands.AddPetPhoto.Commands;

namespace Volunteers.API.Contracts.Volunteers.AddPetPhoto
{
    public record AddPetPhotoRequest
    {

        public IFormFileCollection Photo { get; set; } = new FormFileCollection();

        public static AddPetPhotoCommand ToCommand(
            Guid volunteerId,
            Guid petId,
            AddPetPhotoRequest request,
            List<FileDTO> petPhoto)
        {
            var command = new AddPetPhotoCommand(
                VolunteerId: volunteerId,
                PetId: petId,
                petPhoto);

            return command;
        }
    };
}
