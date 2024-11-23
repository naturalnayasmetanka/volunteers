using CSharpFunctionalExtensions;

namespace Volunteers.Domain.PetManagment.Pet.ValueObjects;

public record PetPhoto
{
    private PetPhoto(
        string path,
        bool isMain)
    {
        Path = path;
        IsMain = isMain;
    }

    public string Path { get; }
    public bool IsMain { get; }

    public static Result<PetPhoto> Create(
        string path,
        bool isMain = false)
    {
        if (string.IsNullOrWhiteSpace(path))
            return Result.Failure<PetPhoto>("Path can not be empty");

        var newPhoto = new PetPhoto(
            path: path,
            isMain: isMain);

        return Result.Success(newPhoto);
    }
}