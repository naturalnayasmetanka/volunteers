namespace Volunteers.Domain.Pet.Models
{
    public class PetPhoto
    {
        public string Path { get; set; } = default!;
        public bool IsMain { get; set; }
    }
}
