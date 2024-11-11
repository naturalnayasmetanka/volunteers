using Volunteers.Domain.Pet.Enums;

namespace Volunteers.Domain.Pet.Models
{
    public class Pet
    {
        public Guid Id { get; private set; }
        public string Nickname { get; private set; } = default!;
        public string Type { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public string Gender { get; private set; } = default!;
        public string Breed { get; private set; } = default!;
        public string Color { get; private set; } = default!;
        public string HelthInfo { get; private set; } = default!;
        public string Location { get; private set; } = default!;
        public double Weight { get; private set; }
        public double Height { get; private set; }
        public int PhoneNumber { get; private set; }
        public bool IsSterilized { get; private set; }
        public bool IsVaccinated { get; private set; }
        public PetStatus HelpStatus { get; private set; }
        public DateTime BirthDate { get; private set; }
        public DateTime CreationDate { get; private set; }

        private List<PetRequisite> _requisites = [];
        public IReadOnlyList<PetRequisite> Requisites => _requisites;

        private List<PetPhoto> _photo = [];
        public IReadOnlyList<PetPhoto> Photo=> _photo;
    }
}