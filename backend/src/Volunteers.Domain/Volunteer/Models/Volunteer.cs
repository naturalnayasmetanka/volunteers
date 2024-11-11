using Volunteers.Domain.Pet.Enums;
using PetModel = Volunteers.Domain.Pet.Models.Pet;

namespace Volunteers.Domain.Volunteer.Models
{
    public class Volunteer
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public double ExperienceInYears { get; set; }
        public int PhoneNumber { get; set; }

        public List<SocialNetwork> SocialNetworks { get; set; } = [];
        public List<VolunteerRequisite> Requisites { get; set; } = [];
        public List<PetModel> Pets { get; set; } = [];

        public int NumberOfAttachedAnimals => Pets.Where(x => x.HelpStatus == PetStatus.FoundHome).Count();
        public int NumberOfLookingHomeAnimals => Pets.Where(x => x.HelpStatus == PetStatus.LookingHome).Count();
        public int NumberOfNeedsHelpAnimals => Pets.Where(x => x.HelpStatus == PetStatus.NeedsHelp).Count();
    }
}
