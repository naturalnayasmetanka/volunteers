﻿using CSharpFunctionalExtensions;
using FluentValidation;
using Volunteers.Application.Volunteers.CreateVolunteer.RequestModels;
using Volunteers.Domain.PetManagment.Volunteer.ValueObjects;
using Volunteers.Domain.Shared.CustomErrors;
using Volunteers.Domain.Shared.Ids;
using VolunteerModel = Volunteers.Domain.PetManagment.Volunteer.AggregateRoot.Volunteer;

namespace Volunteers.Application.Volunteer.CreateVolunteer;

public class CreateVolunteerHandler
{
    private List<Error> _errors = [];
    private readonly IVolunteerRepository _repository;
    private readonly IValidator<CreateVolunteerRequest> _validator;

    public CreateVolunteerHandler(
        IVolunteerRepository repository,
        IValidator<CreateVolunteerRequest> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<Result<Guid, List<Error>>> Handle(
        CreateVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        var volunteerId = VolunteerId.NewVolunteerId();
        var name = Name.Create(request.VolunteerDto.Name).Value;
        var email = Email.Create(request.VolunteerDto.Email).Value;
        var experienceInYears = ExperienceInYears.Create(request.VolunteerDto.ExperienceInYears).Value;
        var phoneNumber = PhoneNumber.Create(request.VolunteerDto.PhoneNumber).Value;

        var socialNetworks = request.VolunteerDto.SocialNetworks;
        var requisites = request.VolunteerDto.VolunteerRequisites;

        var volunteerResult = VolunteerModel.Create(
            volunteerId,
            name,
            email,
            experienceInYears,
            phoneNumber).Value;

        if (socialNetworks is not null)
            socialNetworks
                .ForEach(x =>
                    volunteerResult.AddSocialNetwork(
                        SocialNetwork.Create(
                            x.Title,
                            x.Link
                        ).Value));

        if (requisites is not null)
            requisites
                .ForEach(x =>
                    volunteerResult.AddVolunteerRequisite(
                        VolunteerRequisite.Create(
                            x.Title,
                            x.Description
                        ).Value));

        var createResult = await _repository.CreateAsync(volunteerResult, cancellationToken);

        return (Guid)createResult.Id;
    }
}