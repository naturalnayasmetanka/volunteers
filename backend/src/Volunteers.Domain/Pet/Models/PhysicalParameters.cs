using CSharpFunctionalExtensions;

namespace Volunteers.Domain.Pet.Models;

public record PhysicalParameters
{
    private PhysicalParameters(
        string type,
        string gender,
        string breed,
        string color,
        double weight,
        double height,
        bool isSterilized,
        bool isVaccinated)
    {
        Type = type;
        Gender = gender;
        Breed = breed;
        Color = color;
        Weight = weight;
        Height = height;
        IsSterilized = isSterilized;
        IsVaccinated = isVaccinated;
    }

    public string Type { get; }
    public string? Gender { get; }
    public string? Breed { get; }
    public string? Color { get; }
    public double Weight { get; }
    public double Height { get; }
    public bool IsSterilized { get; }
    public bool IsVaccinated { get; }

    public static Result<PhysicalParameters> Create(
        string type,
        string gender,
        string breed,
        string color,
        double weight,
        double height,
        bool isSterilized,
        bool isVaccinated)
    {
        if (string.IsNullOrWhiteSpace(type))
            return Result.Failure<PhysicalParameters>("Type can not be empty");

        var newParameters = new PhysicalParameters(
            type: type,
            gender: gender,
            breed: breed,
            color: color,
            weight: weight,
            height: height,
            isSterilized: isSterilized,
            isVaccinated: isVaccinated);

        return Result.Success(newParameters);
    }
}