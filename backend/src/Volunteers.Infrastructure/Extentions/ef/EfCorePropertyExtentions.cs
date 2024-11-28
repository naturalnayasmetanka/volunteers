using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace Volunteers.Infrastructure.Extentions.EF;

public static class EfCorePropertyExtentions
{
    public static PropertyBuilder<TValueObject> JsonValueObjectСonversion<TValueObject>(
        this PropertyBuilder<TValueObject> builder)
    {
        return builder.HasConversion(
            v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
            json => JsonSerializer.Deserialize<TValueObject>(json, JsonSerializerOptions.Default)!
        );
    }

    public static PropertyBuilder<IReadOnlyList<TValueObject>> JsonValueObjectCollectionСonversion<TValueObject>(
        this PropertyBuilder<IReadOnlyList<TValueObject>> builder)
    {
        return builder.HasConversion<string>(
            v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
            json => JsonSerializer.Deserialize<IReadOnlyList<TValueObject>>(json, JsonSerializerOptions.Default)!,
            new ValueComparer<IReadOnlyList<TValueObject>>(
                (c1, c2) => c1!.SequenceEqual(c2!),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v!.GetHashCode())),
                c => c.ToList()));
    }
}