using Volunteers.Application.Volunteers.Commands.UpdateMainInfo.Commands;
using Volunteers.Application.Volunteers.Commands.UpdateMainInfo.DTO;

namespace Volunteers.API.Contracts.Volunteers.UpdateMainInfo
{
    public record UpdateMainInfoRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public double ExperienceInYears { get; set; }
        public int PhoneNumber { get; set; }

        public static UpdateMainInfoCommand ToCommand(
            Guid volunteerId,
            UpdateMainInfoRequest request)
        {
            var mainInfoDto = new UpdateMainInfoDto(
                Name: request.Name,
                Email: request.Email,
                ExperienceInYears: request.ExperienceInYears,
                PhoneNumber: request.PhoneNumber);

            var command = new UpdateMainInfoCommand(
                VolunteerId: volunteerId,
                MainInfoDto: mainInfoDto);

            return command;
        }
    }
}
