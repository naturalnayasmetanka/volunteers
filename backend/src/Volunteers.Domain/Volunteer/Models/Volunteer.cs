using Volunteers.Domain.Pet.Enums;
using PetModel = Volunteers.Domain.Pet.Models.Pet;

namespace Volunteers.Domain.Volunteer.Models
{
    public class Volunteer
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = default!;
        public string Email { get; private set; } = default!;
        public double ExperienceInYears { get; private set; }
        public int PhoneNumber { get; private set; }

        private List<SocialNetwork> _socialNetworks = [];
        public IReadOnlyList<SocialNetwork> SocialNetworks => _socialNetworks;

        private List<VolunteerRequisite> _requisites = [];
        public IReadOnlyList<VolunteerRequisite> Requisites => _requisites;

        private List<PetModel> _pets = [];
        public IReadOnlyList<PetModel> Pets => _pets;

        public int NumberOfAttachedAnimals => Pets.Where(x => x.HelpStatus == PetStatus.FoundHome).Count();
        public int NumberOfLookingHomeAnimals => Pets.Where(x => x.HelpStatus == PetStatus.LookingHome).Count();
        public int NumberOfNeedsHelpAnimals => Pets.Where(x => x.HelpStatus == PetStatus.NeedsHelp).Count();
    }
}
