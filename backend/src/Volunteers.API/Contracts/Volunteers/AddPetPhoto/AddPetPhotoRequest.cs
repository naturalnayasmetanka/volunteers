using Volunteers.API.Processors;
using Volunteers.Application.Volunteers.AddPetPhoto.Commands;

namespace Volunteers.API.Contracts.Volunteers.AddPetPhoto
{
    public record AddPetPhotoRequest
    {
        public Guid PetId { get; set; }
        public IFormFileCollection Photo { get; set; } = new FormFileCollection();

        public static async Task<AddPetPhotoCommand> ToCommandAsync(
            Guid VolunteerId,
            AddPetPhotoRequest request)
        {
            await using var fileProcessor = new FormFileProcessor();
            var petPhoto = fileProcessor.Process(request.Photo);

            var command = new AddPetPhotoCommand(
                VolunteerId: VolunteerId,
                PetId: request.PetId,
                petPhoto);

            return command;
        }
    };
}
