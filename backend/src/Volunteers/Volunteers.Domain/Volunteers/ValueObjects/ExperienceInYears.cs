﻿using CSharpFunctionalExtensions;
using Shared.Kernel.CustomErrors;

namespace Volunteers.Domain.Volunteers.ValueObjects;

public record ExperienceInYears
{
    private ExperienceInYears(double value)
    {
        Value = value;
    }

    public double Value { get; }

    public static Result<ExperienceInYears, Error> Create(double value)
    {
        if (value <= 0)
            return Errors.General.ValueIsInvalid("Volunteer Experience");

        var experienceInYears = new ExperienceInYears(value: value);

        return experienceInYears;
    }
}