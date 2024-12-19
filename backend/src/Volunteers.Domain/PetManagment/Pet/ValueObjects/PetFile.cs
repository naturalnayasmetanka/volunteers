using CSharpFunctionalExtensions;
using Volunteers.Domain.Shared.CustomErrors;

namespace Volunteers.Domain.PetManagment.Pet.ValueObjects;

public class PetFile
{
    private PetFile(string path)
    {
        Path = path;
    }

    public string Path { get; }

    public static Result<PetFile, Error> Create(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            return Errors.General.ValueIsInvalid("Path");

        return new PetFile(path);
    }
}