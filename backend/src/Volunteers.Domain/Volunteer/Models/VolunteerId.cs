namespace Volunteers.Domain.Volunteer.Models;

public record VolunteerId
{
    private VolunteerId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; set; }

    public static VolunteerId NewVolunteerId() => new VolunteerId(Guid.NewGuid());
    public static VolunteerId EmptyVolunteerId() => new VolunteerId(Guid.Empty);
    public static VolunteerId Create(Guid id) => new VolunteerId(id);
}

