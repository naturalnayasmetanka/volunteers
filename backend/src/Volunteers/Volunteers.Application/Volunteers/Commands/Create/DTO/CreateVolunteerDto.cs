using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteers.Application.Volunteers.Commands.Create.DTO;

public record CreateVolunteerDto(
    string Name,
    string Email,
    double ExperienceInYears,
    int PhoneNumber,
    List<SocialNetworkDto>? SocialNetworks,
    List<VolunteerRequisiteDto>? VolunteerRequisites);
