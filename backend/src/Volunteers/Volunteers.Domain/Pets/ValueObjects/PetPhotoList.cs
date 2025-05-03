namespace Volunteers.Domain.Pets.ValueObjects;
public class PetPhotoList
{
    private PetPhotoList() { }

    public PetPhotoList(List<PetPhoto> photo)
    {
        Photo = photo;
    }

    public IReadOnlyList<PetPhoto> Photo { get; } = [];
}