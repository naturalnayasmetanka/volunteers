using CSharpFunctionalExtensions;
using BreedModel = Volunteers.Domain.Breed.Models.Breed;
using CustomEntity = Volunteers.Domain.Shared;

namespace Volunteers.Domain.Species.Models
{
    public class Species : CustomEntity.Models.Entity<SpeciesId>
    {
        private Species(SpeciesId id)
            : base(id)
        {
        }

        public Species(
            SpeciesId id,
            Title title,
            Description description
            ) : base(id)
        {
            Title = title;
            Description = description;
        }

        public Title Title { get; private set; } = default!;
        public Description Description { get; private set; } = default!;

        private List<BreedModel> _breeds = [];
        public IReadOnlyList<BreedModel> Breeds => _breeds;

        public static Result<Species> Create(
            SpeciesId id,
            Title title,
            Description description)
        {
            var newSpecies = new Species(
               id: id,
               title: title,
               description: description);

            return Result.Success(newSpecies);
        }

        public void AddBreed(BreedModel newBreed)
        {
            _breeds.Add(newBreed);
        }
    }
}