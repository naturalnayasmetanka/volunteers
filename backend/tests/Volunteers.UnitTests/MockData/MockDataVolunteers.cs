using Volunteers.Domain.PetManagment.Volunteer.AggregateRoot;
using Volunteers.Domain.PetManagment.Volunteer.ValueObjects;
using Volunteers.Domain.Shared.Ids;

namespace Volunteers.UnitTests.MockData;

public class MockDataVolunteers
{
    public Volunteer GetSingleVolunteer()
    {
        var volunteerId = VolunteerId.NewVolunteerId();
        var name = Name.Create("Abobov Aboba Abobovich").Value;
        var email = Email.Create("Aboba@mail.ru").Value;
        var experience = ExperienceInYears.Create(5).Value;
        var phoneNumber = PhoneNumber.Create(15897654).Value;

        var volunteer = Volunteer.Create(
            id: volunteerId,
            name: name,
            email: email,
            experienceInYears: experience,
            phoneNumber: phoneNumber);

        return volunteer.Value;
    }

    public List<Volunteer> GetListVolunteers()
    {
        var volunteerId = VolunteerId.NewVolunteerId();
        var name = Name.Create("Abobov2 Aboba2 Abobovich2").Value;
        var email = Email.Create("Aboba1@mail.ru").Value;
        var experience = ExperienceInYears.Create(2).Value;
        var phoneNumber = PhoneNumber.Create(222222).Value;

        var volunteer = Volunteer.Create(
            id: volunteerId,
            name: name,
            email: email,
            experienceInYears: experience,
            phoneNumber: phoneNumber);

        var volunteerId1 = VolunteerId.NewVolunteerId();
        var name1 = Name.Create("Abobov3 Aboba3 Abobovich3").Value;
        var email1 = Email.Create("Aboba@mail.ru").Value;
        var experience1 = ExperienceInYears.Create(3).Value;
        var phoneNumber1 = PhoneNumber.Create(333333).Value;

        var volunteer1 = Volunteer.Create(
            id: volunteerId,
            name: name,
            email: email,
            experienceInYears: experience,
            phoneNumber: phoneNumber);

        var volunteerId2 = VolunteerId.NewVolunteerId();
        var name2 = Name.Create("Abobov4 Aboba4 Abobovich4").Value;
        var email2 = Email.Create("Aboba4@mail.ru").Value;
        var experience2 = ExperienceInYears.Create(4).Value;
        var phoneNumber2 = PhoneNumber.Create(444444).Value;

        var volunteer2 = Volunteer.Create(
            id: volunteerId,
            name: name,
            email: email,
            experienceInYears: experience,
            phoneNumber: phoneNumber);

        var volunteerId3 = VolunteerId.NewVolunteerId();
        var name3 = Name.Create("Abobov5 Aboba5 Abobovich5").Value;
        var email3 = Email.Create("Aboba5@mail.ru").Value;
        var experience3 = ExperienceInYears.Create(5).Value;
        var phoneNumber3 = PhoneNumber.Create(555555).Value;

        var volunteer3 = Volunteer.Create(
            id: volunteerId,
            name: name,
            email: email,
            experienceInYears: experience,
            phoneNumber: phoneNumber);

        var volunteerId4 = VolunteerId.NewVolunteerId();
        var name4 = Name.Create("Abobov6 Aboba6 Abobovich6").Value;
        var email4 = Email.Create("Aboba6@mail.ru").Value;
        var experience4 = ExperienceInYears.Create(5).Value;
        var phoneNumber4 = PhoneNumber.Create(666666).Value;

        var volunteer4 = Volunteer.Create(
            id: volunteerId,
            name: name,
            email: email,
            experienceInYears: experience,
            phoneNumber: phoneNumber);

        var volunteers = new List<Volunteer>()
        {
            volunteer1.Value,
            volunteer2.Value,
            volunteer3.Value,
            volunteer4.Value
        };

        return volunteers;
    }
}
