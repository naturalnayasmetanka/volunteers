namespace Volunteers.Domain.PetManagment.Pet.ValueObjects;

public class PetFiles
{
    private PetFiles() { }

    public PetFiles(List<PetFile> files)
    {
        Files = files;
    }

    public IReadOnlyList<PetFile> Files { get; } = [];
}