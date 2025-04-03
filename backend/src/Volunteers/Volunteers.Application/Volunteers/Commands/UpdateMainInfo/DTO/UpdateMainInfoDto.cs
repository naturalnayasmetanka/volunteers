using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteers.Application.Volunteers.Commands.UpdateMainInfo.DTO;

public record UpdateMainInfoDto(
    string Name,
    string Email,
    double ExperienceInYears,
    int PhoneNumber);