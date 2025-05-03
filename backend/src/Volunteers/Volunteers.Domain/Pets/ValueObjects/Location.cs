using CSharpFunctionalExtensions;

namespace Volunteers.Domain.Pets.ValueObjects;

public record Location
{
    private Location(
        string country,
        string city,
        string street,
        string houseNumber,
        string roomNumber,
        int floor)
    {
        Country = country;
        City = city;
        Street = street;
        HouseNumber = houseNumber;
        RoomNumber = roomNumber;
        Floor = floor;
    }

    public string Country { get; }
    public string City { get; }
    public string Street { get; }
    public string HouseNumber { get; }
    public string RoomNumber { get; }
    public int Floor { get; }

    public static Result<Location> Create(
        string country,
        string city,
        string street,
        string houseNumber,
        string roomNumber,
        int floor)
    {
        if (string.IsNullOrWhiteSpace(country))
            return Result.Failure<Location>("Country can not be empty");

        if (string.IsNullOrWhiteSpace(city))
            return Result.Failure<Location>("City can not be empty");

        if (string.IsNullOrWhiteSpace(street))
            return Result.Failure<Location>("Street can not be empty");

        if (string.IsNullOrWhiteSpace(houseNumber))
            return Result.Failure<Location>("HouseNumber can not be empty");

        if (floor <= 0)
            return Result.Failure<Location>("Floor cannot be less than or equal to zero");

        var newLocation = new Location(
            country: country,
            city: city,
            street: street,
            houseNumber: houseNumber,
            roomNumber: roomNumber,
            floor: floor);

        return Result.Success(newLocation);
    }
}