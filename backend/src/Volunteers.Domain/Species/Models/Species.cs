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
            string title,
            string description
            ) : base(id)
        {
            Title = title;
            Description = description;
        }

        public string Title { get; private set; } = default!;
        public string Description { get; private set; } = default!;

        private List<BreedModel> _breeds = [];
        public IReadOnlyList<BreedModel> Breeds => _breeds;

        public static Result<Species> Create(
            SpeciesId id,
            string title,
            string description)
        {
            if (string.IsNullOrWhiteSpace(title))
                return Result.Failure<Species>("Title can not be empty");

            if (string.IsNullOrWhiteSpace(description))
                return Result.Failure<Species>("Description can not be empty");

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