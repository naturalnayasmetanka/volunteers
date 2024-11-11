using Volunteers.Domain.Pet.Enums;

namespace Volunteers.Domain.Pet.Models
{
    public class Pet
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; } = default!;
        public string Type { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Gender { get; set; } = default!;
        public string Breed { get; set; } = default!;
        public string Color { get; set; } = default!;
        public string HelthInfo { get; set; } = default!;
        public string Location { get; set; } = default!;
        public double Weight { get; set; }
        public double Height { get; set; }
        public int PhoneNumber { get; set; }
        public bool IsSterilized { get; set; }
        public bool IsVaccinated { get; set; }
        public PetStatus HelpStatus { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreationDate { get; set; }

        public List<PetRequisite> Requisites { get; set; } = [];
        public List<PetPhoto> Photo { get; set; } = [];
    }
}