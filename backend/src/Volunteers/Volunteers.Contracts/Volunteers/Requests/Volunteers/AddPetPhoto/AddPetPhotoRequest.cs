

using Microsoft.AspNetCore.Http;
using Shared.Core.DTO;
using Volunteers.Application.Volunteers.Commands.AddPetPhoto.Commands;

namespace Volunteers.Contracts.Volunteers.Requests.Volunteers.AddPetPhoto
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
